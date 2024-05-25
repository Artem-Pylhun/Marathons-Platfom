using AutoMapper;
using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platform.Domain.Repositories;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Marathons_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBadgeController : ControllerBase
    {
        private readonly IRepository<User_Badge> _userBadgeRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Badge> _badgeRepository;
        private readonly IMapper _mapper;
        public UserBadgeController(IRepository<User_Badge> userBadgeRepository, IRepository<User> userRepository, IRepository<Badge> badgeRepository, IMapper mapper)
        {
            _userBadgeRepository = userBadgeRepository;
            _userRepository = userRepository;
            _badgeRepository = badgeRepository;
            _mapper = mapper;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUserBadges()
        {
            var userBadges = _mapper.Map<IEnumerable<User_Badge>>(await _userBadgeRepository.GetAllAsync());
            return Ok(userBadges);
        }

        [HttpPost("add")]
        public async Task<ActionResult<User_Badge>> AddUserBadge( UserBadgeCreateDto userBadge)
        {
            var badge = await _badgeRepository.GetByIdAsync(userBadge.BadgeId);
            if (badge == null)
            {
                return NotFound("Badge not found");
            }
            var user = await _userRepository.GetByIdAsync(userBadge.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (userBadge == null)
            {
                return NotFound("User's badge wasn't found");
            }


            await _userBadgeRepository.Add(_mapper.Map<User_Badge>(userBadge));
            
            return Ok(userBadge);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<User_Badge>>> GetUserBadgeById(int id)
        {
            var uBadge = await _userBadgeRepository.GetByIdAsync(id);
            if (uBadge is null)
                return NotFound("User's badge not found");
            return Ok(uBadge);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteUserBadge(int id)
        {
            var uBadge = await _userBadgeRepository.GetByIdAsync(id);
            if (uBadge is null)
                return NotFound("User's badge not found");
            await _userBadgeRepository.Delete(uBadge);
            return Ok($"User's Badge {uBadge.Id} deleted");

        }
        [HttpPut("update")]
        public async Task<ActionResult<User_Badge>> UpdateUserBadge( UserBadgeDto userBadgeDTO)
        {
            var existingUserBadge = await _userBadgeRepository.GetByIdAsync(userBadgeDTO.Id);
            if (existingUserBadge == null)
            {
                return NotFound("User`s badge not found");
            }

            if (userBadgeDTO.UserId != 0)
            {
                var newUser = await _userRepository.GetByIdAsync(userBadgeDTO.UserId);
                if (newUser == null)
                {
                    return NotFound("User not found");
                }
                existingUserBadge.UserId = newUser.Id;
            }
            else
                return NotFound("User not found");

            if (userBadgeDTO.BadgeId != 0)
            {
                var newBadge = await _badgeRepository.GetByIdAsync(userBadgeDTO.BadgeId);
                if (newBadge == null)
                {
                    return NotFound("Badge not found");
                }
                existingUserBadge.BadgeId = newBadge.Id;
                existingUserBadge.WhenClaimed = DateTime.Now;
            }
            else
                return NotFound("Badge not found");
            // Save the updated user`s badge to the database
            await _userBadgeRepository.Update(existingUserBadge);

            return Ok(existingUserBadge);
        }
    }
}
