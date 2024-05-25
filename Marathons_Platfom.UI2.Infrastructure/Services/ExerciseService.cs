using Azure;
using Marathons_Platfom.Core.Entities;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Marathons_Platfom.UI2.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platfom.UI2.Infrastructure.Services
{
    public class ExerciseService : IExerciseService<ExerciseDto, ExerciseCreateDto, ExerciseUpdateDto>
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public List<ExerciseDto> Exercises { get; set; } = new List<ExerciseDto>();
        public ExerciseService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task AddExercise(ExerciseCreateDto Exercise)
        {
            await _http.PostAsJsonAsync("api/exercise/add", Exercise);
            _navigationManager.NavigateTo("exercise/get-all");
        }

        public async Task DelExercise(int id)
        {
            await _http.DeleteAsync($"api/exercise/delete/{id}");

        }

        public async Task<ExerciseDto> GetExerciseById(int id)
        {
            var result = await _http.GetAsync($"api/exercise/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<ExerciseDto>();
            }
            return null;
        }

        public async Task<List<ExerciseDto>> GetExercises()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<ExerciseDto>>("api/exercise/get-all");
                if (result != null)
                {
                    Exercises = result;
                }
                else
                {
                    Console.WriteLine("Response is null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching exercises: {ex.Message}");
            }
            return Exercises;
        }

        public async Task<List<ExerciseDto>> GetExercisesByMarathonId(int id)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<ExerciseDto>>($"api/exercise/mid/{id}");
                if (result != null)
                {
                    Exercises = result;

                }
                else
                {
                    Console.WriteLine("Response is null.");
                }
            }
             catch (Exception ex)
            {
                Console.WriteLine($"Error fetching exercises: {ex.Message}");
            }
            return Exercises;

        }

        public async Task UpdateExercise(ExerciseUpdateDto Exercise)
        {
            await _http.PutAsJsonAsync("api/exercise/update", Exercise);
            _navigationManager.NavigateTo("exercise/get-all");
        }
    }
}
