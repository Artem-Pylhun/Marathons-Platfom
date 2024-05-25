using Marathons_Platfom.Core.Entities;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Marathons_Platfom.UI2.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platfom.UI2.Infrastructure.Services
{
    public class UserExerciseStatusService: IUserExerciseStatusService<UserExerciseStatusesDto, UserExerciseStatusesCreateDto>
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public UserExerciseStatusService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<UserExerciseStatusesDto> ExerciseStatuses { get; set; } = new List<UserExerciseStatusesDto>();

        public async Task AddExerciseStatus(UserExerciseStatusesCreateDto exStatus)
        {
            await _http.PostAsJsonAsync("api/userexercisestatus/add", exStatus);
        }

        public async Task DelExerciseStatus(int id)
        {
            await _http.DeleteAsync($"api/userexercisestatus/delete/{id}");
        }

        public async Task<UserExerciseStatusesDto> GetExerciseStatusById(int id)
        {
            var result = await _http.GetAsync($"api/userexercisestatus/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<UserExerciseStatusesDto>();
            }
            return null;

        }

        public async Task<List<UserExerciseStatusesDto>> GetExerciseStatuses()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<UserExerciseStatusesDto>>("api/userexercisestatus/get-all");
                if (result != null)
                {
                    ExerciseStatuses = result;
                }
                else
                {
                    Console.WriteLine("Response is null.");
                }
                return ExerciseStatuses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching exercise statuses: {ex.Message}");
            }
            return null;
        }

        public async Task<UserExerciseStatusesDto> GetStatusByUserAndExerciseId(int userId, int exId)
        {
            var result = await _http.GetAsync($"api/userexercisestatus/get-by-user-and-exercise/{userId}/{exId}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<UserExerciseStatusesDto>();
            }
            return null;
        }

        public async Task UpdateExerciseStatus(UserExerciseStatusesDto exStatus)
        {
            await _http.PutAsJsonAsync("api/userexercisestatus/update", exStatus);
        }
    }
}
