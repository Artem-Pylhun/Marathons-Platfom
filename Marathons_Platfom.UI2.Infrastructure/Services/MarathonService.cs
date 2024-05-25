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
    public class MarathonService : IMarathonService<MarathonDto, MarathonCreateDto, MarathonUpdateDto>
    {
        public List<MarathonDto> Marathons { get; set; } = new List<MarathonDto>();

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public MarathonService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task AddMarathon(MarathonCreateDto marathon)
        {
            await _http.PostAsJsonAsync("api/marathon/add", marathon);
            _navigationManager.NavigateTo("/");
        }

        public async Task DelMarathon(int id)
        {
            await _http.DeleteAsync($"api/marathon/delete/{id}");
        }

        public async Task<MarathonDto> GetMarathonById(int id)
        {
            var result = await _http.GetAsync($"api/marathon/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<MarathonDto>();
            }
            return null;
        }

        public async Task<List<MarathonDto>> GetMarathons()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<MarathonDto>>("api/marathon/get-all");
                if (result != null)
                {
                    Marathons = result;
                }
                else
                {
                    Console.WriteLine("Response is null.");
                }
                return Marathons;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching themes: {ex.Message}");
            }
            return null;
        }

        public async Task UpdateMarathon(MarathonDto marathon)
        {
            await _http.PutAsJsonAsync("api/marathon/update", marathon);
            _navigationManager.NavigateTo("/");
        }
    }
}
