using AutoMapper;
using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platform.Domain.Repositories;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Marathons_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleInMarathonController : ControllerBase
    {
        private readonly IRepository<UserRoleInMarathon> _userRoleInMarathonRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Marathon> _marathonRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;
        public UserRoleInMarathonController(IRepository<UserRoleInMarathon> userRoleInMarathonRepository, IRepository<User> _userRepository,
                                            IRepository<Marathon> marathonRepository,
                                            IRepository<User> userRepository,
                                            IRepository<Role> roleRepository,
                                            IMapper mapper)
        {
            _userRoleInMarathonRepository = userRoleInMarathonRepository;
            _marathonRepository = marathonRepository;
            this._userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsersRolesInMarathons()
        {
            var urim = _mapper.Map<IEnumerable<UserRoleInMarathon>>(await _userRoleInMarathonRepository.GetAllAsync());
            return Ok(urim);
        }

        [HttpPost("add")]
        public async Task<ActionResult<UserRoleInMarathon>> AddUsersRoleInMarathon(UserRoleInMarathonCreateDto urimDTO)
        {
            var marathon = await _marathonRepository.GetByIdAsync(urimDTO.MarathonId);
            if (marathon == null)
            {
                return NotFound("Marathon not found");
            }
            var user = await _userRepository.GetByIdAsync(urimDTO.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var role = await _roleRepository.GetByIdAsync(urimDTO.RoleId);
            if (role == null)
            {
                return NotFound("Role not found");
            }
            if (urimDTO == null)
            {
                return NotFound("User's role in marathon wasn't found");
            }
            await _userRoleInMarathonRepository.Add(_mapper.Map<UserRoleInMarathon>(urimDTO));

            return Ok(urimDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserRoleInMarathon>>> GetUsersRoleInMarathonById(int id)
        {
            var urim = await _userRoleInMarathonRepository.GetByIdAsync(id);
            if (urim is null)
                return NotFound("User's role in marathon not found");
            return Ok(urim);
        }
        [HttpGet("get-by-user-id/{id}")]
        public async Task<ActionResult<List<UserRoleInMarathonDto>>> GetRoleInMarathonByUserId(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return NotFound("User not found");
            return Ok(user.UsersRoleInMarathon);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteUsersRoleInMarathon(int id)
        {
            var urim = await _userRoleInMarathonRepository.GetByIdAsync(id);
            if (urim is null)
                return NotFound("User's role in marathon not found");
            await _userRoleInMarathonRepository.Delete(urim);
            return Ok($"User's role in marathon {urim.Id} deleted");

        }

        [HttpPut("update")]
        public async Task<ActionResult<UserRoleInMarathon>> UpdateUsersRoleInMarathon(UserRoleInMarathonDto urimDTO)
        {
            var existingUrim = await _userRoleInMarathonRepository.GetByIdAsync(urimDTO.Id);
            if (existingUrim == null)
            {
                return NotFound("User`s role in marathon not found");
            }

            if (urimDTO.UserId != 0)
            {
                var newUser = await _userRepository.GetByIdAsync(urimDTO.UserId);
                if (newUser == null)
                {
                    return NotFound("User not found");
                }
                existingUrim.UserId = newUser.Id;
            }
            else
                return NotFound("User not found");

            if (urimDTO.MarathonId != 0)
            {
                var newMarathon = await _marathonRepository.GetByIdAsync(urimDTO.MarathonId);
                if (newMarathon == null)
                {
                    return NotFound("Marathon not found");
                }
                existingUrim.MarathonId = newMarathon.Id;
            }
            else
                return NotFound("Marathon not found");

            if (urimDTO.RoleId != 0)
            {
                var newRole = await _roleRepository.GetByIdAsync(urimDTO.RoleId);
                if (newRole == null)
                {
                    return NotFound("Role not found");
                }
                existingUrim.RoleId = newRole.Id;
            }
            else
                return NotFound("Role not found");
            await _userRoleInMarathonRepository.Update(existingUrim);

            return Ok(existingUrim);
        }
    }
}
