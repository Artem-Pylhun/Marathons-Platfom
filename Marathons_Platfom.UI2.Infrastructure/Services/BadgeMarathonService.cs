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
    public class BadgeMarathonService: IBadgeMarathonService<BadgeMarathonDto, BadgeMarathonCreateDto>
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public BadgeMarathonService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<BadgeMarathonDto> BadgeMarathon { get; set; } = new List<BadgeMarathonDto>();

        public async Task AddBadgeMarathon(BadgeMarathonCreateDto bMarathon)
        {
            await _http.PostAsJsonAsync("api/badgemarathon/add", bMarathon);
        }

        public async Task DelBadgeMarathon(int id)
        {
            await _http.DeleteAsync($"api/badgemarathon/delete/{id}");
        }

        public async Task<BadgeMarathonDto> GetBadgeMarathonById(int id)
        {
            var result = await _http.GetAsync($"api/badgemarathon/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<BadgeMarathonDto>();
            }
            return null;
        }

        public async Task<List<BadgeMarathonDto>> GetBadgesMarathon()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<BadgeMarathonDto>>("api/badgemarathon/get-all");
                if (result != null)
                {
                    BadgeMarathon = result;
                }
                else
                {
                    Console.WriteLine("Response is null.");
                }
                return BadgeMarathon;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching marathon's badges: {ex.Message}");
            }
            return null;
        }

        public async Task UpdateBadgeMarathon(BadgeMarathonDto bMarathon)
        {
            await _http.PutAsJsonAsync("api/badgemarathon/update", bMarathon);
        }
    }
}
