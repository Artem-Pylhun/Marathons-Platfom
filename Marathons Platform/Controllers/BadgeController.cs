using AutoMapper;
using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platform.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Marathons_Platform.API.Interfaces;

namespace Marathons_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        private readonly IRepository<Badge> _badgeRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public BadgeController(IRepository<Badge> badgeRepository, IMapper mapper, IFileService fs)
        {
            _badgeRepository = badgeRepository;
            _mapper = mapper;
            _fileService = fs;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBadges()
        {
            var badges = _mapper.Map<IEnumerable<Badge>>(await _badgeRepository.GetAllAsync());
            return Ok(badges);
        }
        [HttpPost("add")]
        public async Task<ActionResult<Badge>> AddBadge(BadgeCreateDto badge)
        {
            if (badge == null)
            {
                return BadRequest(badge);
            }
            if (badge.BadgeImage != null)
            {
                var fileResult = _fileService.SaveImage(badge.BadgeImage);
                if (fileResult.Contains("Only") || fileResult.Contains("Error"))
                {
                    return BadRequest(fileResult);
                }
                else
                {
                    badge.Photo = fileResult;
                }
                await _badgeRepository.Add(_mapper.Map<Badge>(badge));
                return Ok(badge);
            }
            await _badgeRepository.Add(_mapper.Map<Badge>(badge));
            return Ok(badge);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Badge>>> GetBadge(int id)
        {
            var badge = await _badgeRepository.GetByIdAsync(id);
            if (badge is null)
                return NotFound("Badge not found");
            return Ok(badge);
        }



        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteBadge(int id)
        {
            var badge = await _badgeRepository.GetByIdAsync(id);
            if (badge is null)
                return NotFound("Badge not found");
            _fileService.DeleteImage(badge.Photo);
            await _badgeRepository.Delete(badge);
            return Ok($"Badge {badge.Id} deleted");
        }


        [HttpPut("update")]
        public async Task<ActionResult<Badge>> UpdateBadge(BadgeUpdateDto badgeDTO)
        {
            var badge = _mapper.Map<Badge>(badgeDTO);
            if (badge.Id == null)
            {
                return BadRequest("Badge not found");
            }

            // Retrieve the existing badge from the database
            var existingBadge = await _badgeRepository.GetByIdAsync(badge.Id);
            if (existingBadge == null)
            {
                return NotFound("Badge not found");
            }

            // Update the title and description
            existingBadge.Title = badgeDTO.Title;
            existingBadge.Description = badgeDTO.Description;

            if (badgeDTO.BadgeImage != null)
            {
                _fileService.DeleteImage(existingBadge.Photo);
                var fileResult = _fileService.SaveImage(badgeDTO.BadgeImage);
                if (fileResult.Contains("Only") || fileResult.Contains("Error"))
                {
                    return BadRequest(fileResult);
                }
                else
                {
                    existingBadge.Photo = fileResult;
                }
            }

            await _badgeRepository.Update(existingBadge);

            return Ok(existingBadge);
        }
    }
}
