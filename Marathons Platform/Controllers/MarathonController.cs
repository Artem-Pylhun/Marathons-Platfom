using AutoMapper;
using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platform.Domain.Repositories;
using Marathons_Platfom.UI2.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Marathons_Platfom.UI2.Infrastructure.DTOs;

namespace Marathons_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarathonController : ControllerBase
    {
        private readonly IRepository<Marathon> _marathonRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Theme> _themeRepository;
        public MarathonController(IRepository<Marathon> marathonRepository, IMapper mapper, IRepository<Theme> themeRepository)
        {
            _marathonRepository = marathonRepository;
            _mapper = mapper;
            _themeRepository = themeRepository;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllMarathons()
        {
            var marathons = _mapper.Map<IEnumerable<Marathon>>(await _marathonRepository.GetAllAsync());
            return Ok(marathons);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Marathon>> AddMarathon(MarathonCreateDto marathon)
        {
            if (marathon == null)
            {
                return BadRequest(marathon);
            }
            var theme = await _themeRepository.GetByIdAsync(marathon.ThemeId);
            if (theme == null)
            {
                return NotFound("Theme not found");
            }
            await _marathonRepository.Add(_mapper.Map<Marathon>(marathon));
            return Ok(marathon);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Marathon>>> GetMarathon(int id)
        {
            var marathon = await _marathonRepository.GetByIdAsync(id);
            if (marathon is null)
                return NotFound("Marathon not found");
            return Ok(marathon);
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteMarathon(int id)
        {
            var marathon = await _marathonRepository.GetByIdAsync(id);
            if (marathon is null)
                return NotFound("Marathon not found");
            await _marathonRepository.Delete(marathon);
            return Ok($"Marathon {marathon.Title} deleted");
        }
        [HttpPut("update")]
        public async Task<ActionResult<Marathon>> UpdateMarathon(MarathonUpdateDto marathonDTO)
        {
            // Retrieve the existing marathon from the database
            var existingMarathon = await _marathonRepository.GetByIdAsync(marathonDTO.Id);
            if (existingMarathon == null)
            {
                return NotFound("Marathon not found");
            }

            // Update properties that are not null or empty in the DTO
            if (!string.IsNullOrEmpty(marathonDTO.Title))
                existingMarathon.Title = marathonDTO.Title;

            if (marathonDTO.ThemeId != 0)
            {
                var newTheme = await _themeRepository.GetByIdAsync(marathonDTO.ThemeId);
                if (newTheme == null)
                {
                    return NotFound("Theme not found");
                }
                existingMarathon.ThemeId = newTheme.Id;
            }
            else
                return NotFound("Theme not found");

            if (marathonDTO.DateOfStart != DateTime.MinValue)
                existingMarathon.DateOfStart = marathonDTO.DateOfStart;

            if (marathonDTO.Duration != 0)
                existingMarathon.Duration = marathonDTO.Duration;

            if (!string.IsNullOrEmpty(marathonDTO.Description))
                existingMarathon.Description = marathonDTO.Description;

            // Save the updated marathon to the database
            await _marathonRepository.Update(existingMarathon);

            return Ok(existingMarathon);
        }
    }
}
