using AutoMapper;
using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platform.Domain.Repositories;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Marathons_Platform.Repositories.Repositories;

namespace Marathons_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserExerciseStatusController : ControllerBase
    {
        private readonly IRepository<UserExerciseStatus> _userExerciseStatusRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Exercise> _exerciseRepository;
        private readonly IMapper _mapper;
        public UserExerciseStatusController(IRepository<UserExerciseStatus> userExerciseStatusRepository, IRepository<User> userRepository, IRepository<Exercise> exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
            _userExerciseStatusRepository = userExerciseStatusRepository;
            _mapper = mapper;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsersExerciseStatuses()
        {
            var ues = _mapper.Map<IEnumerable<UserExerciseStatus>>(await _userExerciseStatusRepository.GetAllAsync());
            return Ok(ues);
        }

        [HttpPost("add")]
        public async Task<ActionResult<UserExerciseStatus>> AddUsersExerciseStatus(UserExerciseStatusesCreateDto ues)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(ues.ExerciseId);
            if (exercise == null)
            {
                return NotFound("Exercise not found");
            }
            var user = await _userRepository.GetByIdAsync(ues.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (ues == null)
            {
                return NotFound("User's exercise status wasn't found");
            }
            await _userExerciseStatusRepository.Add(_mapper.Map<UserExerciseStatus>(ues));

            return Ok(ues);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserExerciseStatus>>> GetUsersExerciseStatusById(int id)
        {
            var ues = await _userExerciseStatusRepository.GetByIdAsync(id);
            if (ues is null)
                return NotFound("User's exercise status not found");
            return Ok(ues);
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteUserExerciseStatus(int id)
        {
            var ues = await _userExerciseStatusRepository.GetByIdAsync(id);
            if (ues is null)
                return NotFound("User's exercise status not found");
            await _userExerciseStatusRepository.Delete(ues);
            return Ok($"User's exercise statuses {ues.Id} deleted");

        }
        [HttpPut("update")]
        public async Task<ActionResult<UserExerciseStatus>> UpdateUserExerciseStatus(UserExerciseStatusesDto uesDTO)
        {
            // Retrieve the existing userBadge from the database
            var existingUes = await _userExerciseStatusRepository.GetByIdAsync(uesDTO.Id);
            if (existingUes== null)
            {
                return NotFound("User`s exercise status not found");
            }

            if (uesDTO.UserId != 0)
            {
                var newUser = await _userRepository.GetByIdAsync(uesDTO.UserId);
                if (newUser == null)
                {
                    return NotFound("User not found");
                }
                existingUes.UserId = newUser.Id;
            }
            else
                return NotFound("User not found");

            if (uesDTO.ExerciseId!= 0)
            {
                var newEx = await _exerciseRepository.GetByIdAsync(uesDTO.ExerciseId);
                if (newEx == null)
                {
                    return NotFound("Exercise not found");
                }
                existingUes.ExerciseId = newEx.Id;
            }
            else
                return NotFound("Exercise not found");
            if(uesDTO.Status != null)
            {
                existingUes.Status = uesDTO.Status;
            }
            await _userExerciseStatusRepository.Update(existingUes);

            return Ok(existingUes);
        }
        [HttpGet("get-by-user-and-exercise/{userId}/{exerciseId}")]
        public async Task<ActionResult<UserExerciseStatusesDto>> GetProgressByUserIdAndExerciseId(int userId, int exerciseId)
        {
            var userExerciseStatus = await ((UserExerciseStatusRepository)_userExerciseStatusRepository).GetByUserAndExerciseAsync(userId, exerciseId);

            if (userExerciseStatus == null)
            {
                return NotFound("User's exercise statuses not found for user " + userId + " and exercise " + exerciseId);
            }

            return Ok(userExerciseStatus);
        }
    }
}
