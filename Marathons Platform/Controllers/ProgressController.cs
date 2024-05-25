using AutoMapper;
using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Marathons_Platform.Repositories.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Marathons_Platform.Domain.Repositories;

namespace Marathons_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly IRepository<Progress> _progressRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Marathon> _marathonRepository;
        private readonly IMapper _mapper;

        public ProgressController(IRepository<Progress> progressRepository, IRepository<User> userRepository, IRepository<Marathon> marathonRepository, IMapper mapper)
        {
            _progressRepository = progressRepository;
            _userRepository = userRepository;
            _marathonRepository = marathonRepository;
            _mapper = mapper;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllProgresses()
        {
            var progresses = _mapper.Map<IEnumerable<Progress>>(await _progressRepository.GetAllAsync());
            return Ok(progresses);
        }
        [HttpPost("add")]
        public async Task<ActionResult<Progress>> AddProgress(ProgressCreateDto progress)
        {
            var marathon = await _marathonRepository.GetByIdAsync(progress.MarathonId);
            if(marathon == null)
            {
                return NotFound("Marathon not found");
            }
            var user = await _userRepository.GetByIdAsync(progress.UserId);
            if(user == null)
            {
                return NotFound("User not found");
            }
            if (progress == null)
            {
                return NotFound("Progress wasn't found");
            }
            await _progressRepository.Add(_mapper.Map<Progress>(progress));

            marathon.NumOfParticipants++;
            await _marathonRepository.Update(marathon);
            return Ok(progress);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Progress>>> GetRoleById(int id)
        {
            var progress = await _progressRepository.GetByIdAsync(id);
            if (progress is null)
                return NotFound("Progress not found");
            return Ok(progress);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteProgress(int id)
        {
            var progress = await _progressRepository.GetByIdAsync(id);
            if (progress is null)
                return NotFound("Progress not found");
            var marathon = await _marathonRepository.GetByIdAsync(progress.MarathonId);
            if (marathon == null)
                return NotFound("Marathon not found");
            await _progressRepository.Delete(progress);
            marathon.NumOfParticipants--;
            await _marathonRepository.Update(marathon);
            return Ok($"Progress {progress.Id} deleted");

        }

        [HttpPut("update")]
        public async Task<ActionResult<Progress>> UpdateProgress(ProgressDto progressDTO)
        {
            var progress = _mapper.Map<Progress>(progressDTO);
            if (progress.Id == null)
            {
                return BadRequest("Progress not found");
            }

            var existingProgress = await _progressRepository.GetByIdAsync(progress.Id);
            if (existingProgress == null)
            {
                return NotFound("Progress not found");
            }
            var marathon = await _marathonRepository.GetByIdAsync(progressDTO.MarathonId);
            if (marathon == null)
            {
                return NotFound("Marathon not found");
            }
            var user = await _userRepository.GetByIdAsync(progressDTO.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            existingProgress.MarathonId = marathon.Id;
            existingProgress.UserId = user.Id;
            existingProgress.Percent = progressDTO.Percent;
            await _progressRepository.Update(existingProgress);

            return Ok(existingProgress);
        }
        [HttpGet("get-by-user-and-marathon/{userId}/{marathonId}")]
        public async Task<ActionResult<ProgressDto>> GetProgressByUserIdAndMarathonId(int userId, int marathonId)
        {
            var progress = await ((ProgressRepository)_progressRepository).GetByUserAndMarathonAsync(userId, marathonId);

            if (progress == null)
            {
                return NotFound("Progress not found for user " + userId + " and marathon " + marathonId);
            }

            return Ok(progress);
        }
        [HttpGet("exists/{userId}/{marathonId}")]
        public async Task<ActionResult<bool>> ProgressExistsForUserAndMarathon(int userId, int marathonId)
        {
            var exists = await ((ProgressRepository)_progressRepository).ExistsAsync(userId, marathonId);
            return Ok(exists);
        }
    }
}
