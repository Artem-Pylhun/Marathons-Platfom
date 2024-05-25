using AutoMapper;
using Marathons_Platfom.Core.Entities;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platform.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marathons_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgeMarathonController : ControllerBase
    {
        private readonly IRepository<BadgeMarathon> _badgeMarathonRepository;
        private readonly IRepository<Badge> _badgeRepository;
        private readonly IRepository<Marathon> _marathonRepository;
        private readonly IMapper _mapper;
        public BadgeMarathonController(IRepository<BadgeMarathon> badgeMarathonRepository, IRepository<Badge> badgeRepository, IRepository<Marathon> marathonRepository, IMapper mapper)
        {
            _badgeMarathonRepository = badgeMarathonRepository;
            _badgeRepository = badgeRepository;
            _marathonRepository = marathonRepository;
            _mapper = mapper;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetMarathonBadges()
        {
            var progresses = _mapper.Map<IEnumerable<BadgeMarathon>>(await _badgeMarathonRepository.GetAllAsync());
            return Ok(progresses);
        }

        [HttpPost("add")]
        public async Task<ActionResult<BadgeMarathon>> AddMarathonBadge(BadgeMarathonCreateDto bmDto)
        {
            var marathon = await _marathonRepository.GetByIdAsync(bmDto.MarathonId);
            if (marathon == null)
            {
                return NotFound("Marathon not found");
            }
            var badge = await _badgeRepository.GetByIdAsync(bmDto.BadgeId);
            if (badge == null)
            {
                return NotFound("Badge not found");
            }
            if (bmDto == null)
            {
                return NotFound("Marathon's badge not found");
            }
            await _badgeMarathonRepository.Add(_mapper.Map<BadgeMarathon>(bmDto));
            return Ok(bmDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<BadgeMarathon>>> GetMarathonBadgeById(int id)
        {
            var bm = await _badgeMarathonRepository.GetByIdAsync(id);
            if (bm is null)
                return NotFound("Marathon's badge not found");
            return Ok(bm);
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteMarathonBadge(int id)
        {
            var bm = await _badgeMarathonRepository.GetByIdAsync(id);
            if (bm is null)
                return NotFound("Marathon's badge not found");
            await _badgeMarathonRepository.Delete(bm);
            return Ok($"Marathon's badge {bm.Id} deleted");

        }
        [HttpPut("update")]
        public async Task<ActionResult<BadgeMarathon>> UpdateMarathonBadge(BadgeMarathon bmDTO)
        {
            var badgeMarathon = _mapper.Map<BadgeMarathon>(bmDTO);
            if (badgeMarathon.Id == null)
            {
                return BadRequest("Marathon's badge not found");
            }

            // Retrieve the existing progress from the database
            var existingBm = await _badgeMarathonRepository.GetByIdAsync(badgeMarathon.Id);
            if (existingBm== null)
            {
                return NotFound("Marathon badge not found");
            }
            var badge = await _badgeRepository.GetByIdAsync(bmDTO.BadgeId);
            if (badge == null)
            {
                return NotFound("Badge not found");
            }
            var marathon = await _marathonRepository.GetByIdAsync(bmDTO.MarathonId);
            if (marathon == null)
            {
                return NotFound("Marathon not found");
            }
            existingBm.MarathonId = marathon.Id;
            existingBm.BadgeId = badge.Id;
            // Save the updated progress to the database
            await _badgeMarathonRepository.Update(existingBm);

            return Ok(existingBm);
        }
    }
}
