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
using static System.Net.WebRequestMethods;

namespace Marathons_Platfom.UI2.Infrastructure.Services
{
    public class ProgressService : IProgressService<ProgressDto, ProgressCreateDto, ProgressAddDto>
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public List<ProgressDto> Progresses { get; set; } = new List<ProgressDto>();
        public ProgressService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task AddProgress(ProgressCreateDto progress)
        {
            await _http.PostAsJsonAsync("api/progress/add", progress);
        }

        public async Task DelProgress(int id)
        {
            await _http.DeleteAsync($"api/progress/delete/{id}");
        }

        public async Task<ProgressDto> GetProgressById(int id)
        {
            var result = await _http.GetAsync($"api/progress/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<ProgressDto>();
            }
            return null;
        }


        public async Task<List<ProgressDto>> GetProgresses()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<ProgressDto>>("api/progress/get-all");
                if (result != null)
                {
                    Progresses = result;
                }
                else
                {
                    Console.WriteLine("Response is null.");
                }
                return Progresses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching themes: {ex.Message}");
            }
            return null;
        }

        public async Task<ProgressDto> GetProgressByUserAndMarathonId(int userId, int marathonId)
        {
            var result = await _http.GetAsync($"api/progress/get-by-user-and-marathon/{userId}/{marathonId}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<ProgressDto>();
            }
            return null;
        }

        public async Task<bool> ExistsAsync(int userId, int marathonId)
        {
            using var httpResponse = await _http.GetAsync($"/api/progress/get-by-user-and-marathon/{userId}/{marathonId}");
            return httpResponse.IsSuccessStatusCode;
        }

        public async Task UpdateProgress(ProgressDto progress)
        {
            await _http.PutAsJsonAsync("api/progress/update", progress);

        }
    }
}
