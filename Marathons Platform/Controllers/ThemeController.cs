using AutoMapper;
using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Marathons_Platform.Repositories.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marathons_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IRepository<Theme> _themeRepository;
        private readonly IMapper _mapper;
        public ThemeController(IRepository<Theme> themeRepository, IMapper mapper)
        {
            _themeRepository = themeRepository;
            _mapper = mapper;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllThemes()
        {
            var themes = _mapper.Map<IEnumerable<Theme>>(await _themeRepository.GetAllAsync());
            return Ok(themes);
        }
        [HttpPost("add")]
        public async Task<ActionResult<Theme>> AddTheme(ThemeCreateDto theme)
        {
            if (theme == null)
            {
                return BadRequest("Theme wasn't found");
            }
            await _themeRepository.Add(_mapper.Map<Theme>(theme));
            return Ok(theme);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Theme>>> GetThemeById(int id)
        {
            var theme = await _themeRepository.GetByIdAsync(id);
            if (theme is null)
                return NotFound("Theme not found");
            return Ok(theme);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteTheme(int id)
        {
            var theme = await _themeRepository.GetByIdAsync(id);
            if (theme is null)
                return NotFound("Theme not found");
            await _themeRepository.Delete(theme);
            return Ok($"Theme {theme.Id} deleted");
        }
        [HttpPut("update")]
        public async Task<ActionResult<Theme>> UpdateTheme(ThemeDto themeDTO)
        {
            var theme = _mapper.Map<Theme>(themeDTO);
            if (theme.Id == null)
            {
                return BadRequest("Theme not found");
            }

            // Retrieve the existing theme from the database
            var existingTheme = await _themeRepository.GetByIdAsync(theme.Id);
            if (existingTheme == null)
            {
                return NotFound("Role not found");
            }

            // Update the title themeDTO
            existingTheme.Title = themeDTO.Title;

            // Save the updated role to the database
            await _themeRepository.Update(existingTheme);

            return Ok(existingTheme);
        }
    }
}
