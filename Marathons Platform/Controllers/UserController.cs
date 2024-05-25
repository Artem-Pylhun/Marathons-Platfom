using AutoMapper;
using Azure.Core;
using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platform.Domain.Repositories;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Marathons_Platform.Repositories.Repositories;
using Marathons_Platform.Repositories.Interfaces;

namespace Marathons_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Progress> _progressRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Marathon> _marathonRepository;
        private readonly IRepository<UserRoleInMarathon> _urinRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration, IJwtProvider jwtProvider, IRepository<User> userRepository, IRepository<UserRoleInMarathon> urinRepository, IRepository<Marathon> marathonRepository, IRepository<Progress> progressRepository, IMapper mapper)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _mapper = mapper;
            _progressRepository = progressRepository;
            _marathonRepository = marathonRepository;
            _urinRepository = urinRepository;
            _jwtProvider = jwtProvider;
        }
        [NonAction]
        public bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> users = _mapper.Map<List<User>>(await _userRepository.GetAllAsync());
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> AddUser(UserCreateDto user)
        {
            if (user == null)
            {
                return BadRequest("User can't be created");
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.BCrypt.GenerateSalt());
            if (!IsValidEmail(user.Email))
            {
                return BadRequest("Email is invalid");
            }
            await _userRepository.Add(_mapper.Map<User>(user));
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ShowUserDto>>> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return NotFound("User not found");
            return Ok(user);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return NotFound("User not found");
            
            var progressRecords = _progressRepository.GetQueryable().Where(p => p.UserId == id);
            foreach (var progress in progressRecords)
            {
                var marathon = await _marathonRepository.GetByIdAsync(progress.MarathonId);
                if (marathon != null)
                {
                    marathon.NumOfParticipants--;
                    await _marathonRepository.Update(marathon);
                }
                //Delete each progress record
                await _progressRepository.Delete(progress);
               
            }
            var urins = _urinRepository.GetQueryable().Where(ur=> ur.UserId == id);
            foreach(var urin in urins)
            {
                await _urinRepository.Delete(urin);
            }
            await _userRepository.Delete(user);

            return Ok($"User {user.Id} deleted");
        }
        [HttpPut("update")]
        public async Task<ActionResult<User>> UpdateUser(UserDto userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            if (user.Id == null)
            {
                return BadRequest("User not found");
            }

            // Retrieve the existing user from the database
            var existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            existingUser.Name = userDTO.Name;
            existingUser.Surname = userDTO.Surname;
             if (!IsValidEmail(userDTO.Email))
            {
                return Conflict("Email is invalid");
            }
            existingUser.Email = userDTO.Email;
            existingUser.Password = userDTO.Password;
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(existingUser.Password, BCrypt.Net.BCrypt.GenerateSalt());
            existingUser.Age = userDTO.Age;

            await _userRepository.Update(existingUser);

            return Ok(existingUser);
        }
        [HttpPost("login")]
        public ActionResult Login(UserLogDto request)
        {
            var exUser = _userRepository.GetQueryable().Where(u => u.Email == request.Email).FirstOrDefault();
            if(exUser.Email != request.Email)
            {
                if (!IsValidEmail(request.Email))
                {
                    return BadRequest("Email is incorrect.");
                }
                return BadRequest("Email not found");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password,exUser.Password))
            {
                return BadRequest("Wrong password.");
            }
            string token = _jwtProvider.Generate(exUser);
            return Ok(new { Token = token });
        }

        private string CreateToken(User user)
        {
            var urinOfUser = ((UserRoleInMarathonRepository)_urinRepository).GetByUserId(user.Id);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(urinOfUser))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(0.5),
                    signingCredentials:creds
                ) ;

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
