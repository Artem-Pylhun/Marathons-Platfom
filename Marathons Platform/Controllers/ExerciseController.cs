using AutoMapper;
using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platform.Domain.Repositories;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marathons_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IRepository<Marathon> _marathonRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Exercise> _exerciseRepository;
        public ExerciseController(IRepository<Marathon> marathonRepository, IMapper mapper, IRepository<Exercise> exerciseRepository)
        {
            _marathonRepository = marathonRepository;
            _mapper = mapper;
            _exerciseRepository = exerciseRepository;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllExercises()
        {
            var exercises = _mapper.Map<IEnumerable<Exercise>>(await _exerciseRepository.GetAllAsync());
            return Ok(exercises);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Exercise>> AddExercise(ExerciseCreateDto exercise)
        {
            if (exercise == null)
            {
                return BadRequest("Exercise data is missing.");
            }

            var marathon = await _marathonRepository.GetByIdAsync(exercise.MarathonId);
            var marExercises = _exerciseRepository.GetQueryable().Where(ex => ex.MarathonId == marathon.Id).ToList();
            if (marathon == null)
            {
                return NotFound("Marathon not found.");
            }
            if (marExercises.Count >= marathon.Duration)
            {
                return Conflict($"The number of exercises cannot exceed the duration ({marathon.Duration}) of the marathon.");
            }
            if (marExercises.Any(e => e.DayNum == exercise.DayNum))
            {
                return Conflict($"An exercise for day {exercise.DayNum} already exists for this marathon.");
            }
            if (exercise.DayNum < 1 || exercise.DayNum > marathon.Duration)
            {
                return BadRequest($"DayNum must be between 1 and the marathon duration ({marathon.Duration}).");
            }
            // Check if an exercise for the current day already exists
            var existingExerciseForDay = marExercises.FirstOrDefault(e => e.DayNum == exercise.DayNum);
            if (existingExerciseForDay != null)
            {
                return Conflict($"An exercise for day {exercise.DayNum} already exists for this marathon.");
            }

            // Map the DTO to an Exercise entity and add it to the marathon's collection
            var newExercise = _mapper.Map<Exercise>(exercise);
            await _exerciseRepository.Add(newExercise);

            return Ok(newExercise);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Exercise>>> GetExercise(int id)
        {
            var ex = await _exerciseRepository.GetByIdAsync(id);
            if (ex is null)
                return NotFound("Exercise not found");
            return Ok(ex);
        }
        [HttpGet("mid/{id}")]
        public async Task<ActionResult<List<Exercise>>> GetExercisesByMarathon(int id)
        {
            var exercise = _mapper.Map<List<Exercise>>(await ((ExerciseRepository)_exerciseRepository).GetByMarathonIdAsync(id));
            if (exercise is null)
                return NotFound("No exercises found");
            return Ok(exercise);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteExercise(int id)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(id);
            if (exercise is null)
                return NotFound("Exercise not found");
            await _exerciseRepository.Delete(exercise);
            return Ok($"Exercise {exercise.Title} deleted");
        }
        [HttpPut("update")]
        public async Task<ActionResult<Exercise>> UpdateExercise(ExerciseUpdateDto exerciseDTO)
        {
            // Retrieve the existing exercise from the database
            var existingExercise = await _exerciseRepository.GetByIdAsync(exerciseDTO.Id);
            if (existingExercise == null)
            {
                return NotFound("Exercise not found");
            }

            // Update properties that are not null or empty in the DTO
            if (!string.IsNullOrEmpty(exerciseDTO.Title))
                existingExercise.Title = exerciseDTO.Title;
            if (exerciseDTO.MarathonId != 0)
            {
                var newMarathon = await _marathonRepository.GetByIdAsync(exerciseDTO.MarathonId);
                var marExercises = _exerciseRepository.GetQueryable().Where(ex => ex.MarathonId == newMarathon.Id).ToList();
                if (newMarathon == null)
                {
                    return NotFound("Marathon not found");
                }
                if (existingExercise.DayNum < 1 || existingExercise.DayNum > newMarathon.Duration)
                {
                    return BadRequest($"DayNum must be between 1 and the marathon duration ({newMarathon.Duration}).");
                }
                // Check for DayNum uniqueness in the new marathon (excluding the current exercise)
                if (marExercises.Any(e => e.Id != existingExercise.Id && e.DayNum == exerciseDTO.DayNum))
                {
                    return Conflict($"An exercise for day {exerciseDTO.DayNum} already exists for this marathon.");
                }
                existingExercise.DayNum = exerciseDTO.DayNum;
                existingExercise.MarathonId = newMarathon.Id;
            }
            var existingMarathon = await _marathonRepository.GetByIdAsync(existingExercise.MarathonId);
            if (exerciseDTO.DayNum < 1 || exerciseDTO.DayNum > existingMarathon.Duration)
            {
                return BadRequest($"DayNum must be between 1 and the marathon duration ({existingExercise.Marathon.Duration}).");
            }
            existingExercise.DayNum = exerciseDTO.DayNum;
            if (!string.IsNullOrEmpty(exerciseDTO.Description))
                existingExercise.Description = exerciseDTO.Description;
            if (!string.IsNullOrEmpty(exerciseDTO.URL))
                existingExercise.URL = exerciseDTO.URL;

            // Save the updated exercise to the database
            await _exerciseRepository.Update(existingExercise);

            return Ok(existingExercise);
        }
    }
}
