using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Marathons_Platfom.UI2.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Marathons_Platfom.UI2.Infrastructure.Services
{
    public class BadgeService : IBadgeService<BadgeDto, BadgeCreateDto, BadgeUpdateDto>
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public List<BadgeDto> Badges { get; set; } = new List<BadgeDto>();

        public BadgeService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task AddBadge(BadgeCreateDto badge)
        {
            await _http.PostAsJsonAsync("api/badge/add", badge);
            _navigationManager.NavigateTo("badge/get-all");
        }

        public async Task DelBadge(int id)
        {
            await _http.DeleteAsync($"api/badge/delete/{id}");
        }

        public async Task<BadgeDto> GetBadgeById(int id)
        {
            var result = await _http.GetAsync($"api/badge/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<BadgeDto>();
            }
            return null;
        }

        public async Task<List<BadgeDto>> GetBadges()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<BadgeDto>>("api/badge/get-all");
                if (result != null)
                {
                    Badges = result;
                }
                else
                {
                    Console.WriteLine("Response is null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching badges: {ex.Message}");
            }
            return Badges;
        }

        public async Task UpdateBadge(BadgeUpdateDto badge)
        {
            await _http.PutAsJsonAsync("api/badge/update", badge);
            _navigationManager.NavigateTo("badge/get-all");
        }
    }
}
