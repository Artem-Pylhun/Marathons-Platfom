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
    public class ThemeService : IThemeService<ThemeDto, ThemeCreateDto>
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public List<ThemeDto> Themes { get; set; } = new List<ThemeDto>();

        public ThemeService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task AddTheme(ThemeCreateDto theme)
        {

            await _http.PostAsJsonAsync("api/theme/add", theme);
            _navigationManager.NavigateTo("theme/get-all");
        }

        public async Task DelTheme(int id)
        {
            await _http.DeleteAsync($"api/theme/delete/{id}");
        }

        public async Task<ThemeDto> GetThemeById(int id)
        {
            var result = await _http.GetAsync($"api/theme/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<ThemeDto>();
            }
            return null;
        }

        public async Task<List<ThemeDto>> GetThemes()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<ThemeDto>>("api/theme/get-all");
                if (result != null)
                {
                    Themes = result;
                }
                else
                {
                    Console.WriteLine("Response is null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching themes: {ex.Message}");
            }
            return Themes;
        }

        public async Task UpdateTheme(ThemeDto theme)
        {
            await _http.PutAsJsonAsync("api/theme/update", theme);
            _navigationManager.NavigateTo("theme/get-all");
        }
    }
}
