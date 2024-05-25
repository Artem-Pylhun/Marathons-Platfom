using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Marathons_Platfom.UI2.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platfom.UI2.Infrastructure.Services
{
    public class UserBadgeService : IUserBadgeService<UserBadgeDto, UserBadgeCreateDto>

    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public UserBadgeService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<UserBadgeDto> UserBadges { get;set; } = new List<UserBadgeDto>();

        public async Task AddUserBadge(UserBadgeCreateDto uBadge)
        {
            await _http.PostAsJsonAsync("api/userbadge/add", uBadge);
        }

        public async Task DelUserBadge(int id)
        {
            await _http.DeleteAsync($"api/userbadge/delete/{id}");
        }

        public async Task<UserBadgeDto> GetUserBadgeById(int id)
        {
            var result = await _http.GetAsync($"api/userbadge/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<UserBadgeDto>();
            }
            return null;

        }

        public async Task<List<UserBadgeDto>> GetUserBadges()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<UserBadgeDto>>("api/userbadge/get-all");
                if (result != null)
                {
                    UserBadges = result;
                }
                else
                {
                    Console.WriteLine("Response is null.");
                }
                return UserBadges;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching exercise statuses: {ex.Message}");
            }
            return null;
        }

        public async Task UpdateUserBadge(UserBadgeDto uBadge)
        {
            await _http.PutAsJsonAsync("api/userbadge/update", uBadge);
        }
    }
}
