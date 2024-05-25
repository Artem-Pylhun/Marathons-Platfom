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
using static System.Net.WebRequestMethods;

namespace Marathons_Platfom.UI2.Infrastructure.Services
{
    public class UserRoleInMarathon : IUserRoleInMarathon<UserRoleInMarathonDto, UserRoleInMarathonCreateDto>
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public List<UserRoleInMarathonDto> UserRolesInMarathon { get; set; } = new List<UserRoleInMarathonDto>();
        public UserRoleInMarathon(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public async Task AddUserRolesInMarathon(UserRoleInMarathonCreateDto urin)
        {
            await _http.PostAsJsonAsync("api/userroleinmarathon/add", urin);
        }

        public async Task DelUserRolesInMarathon(int id)
        {
            await _http.DeleteAsync($"api/userroleinmarathon/delete/{id}");
        }

        public async Task<List<UserRoleInMarathonDto>> GetUserRolesInMarathon()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<UserRoleInMarathonDto>>("api/userroleinmarathon/get-all");
                if (result != null)
                {
                    UserRolesInMarathon = result;
                }
                else
                {
                    Console.WriteLine("Response is null.");
                }
                return UserRolesInMarathon;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user's role in marathon: {ex.Message}");
            }
            return null;
        }

        public async Task<UserRoleInMarathonDto> GetUserRolesInMarathonById(int id)
        {
            var result = await _http.GetAsync($"api/userroleinmarathon/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<UserRoleInMarathonDto>();
            }
            return null;
        }

        public async Task UpdateUserRolesInMarathon(UserRoleInMarathonDto urin)
        {
            await _http.PutAsJsonAsync("api/userroleinmarathon/update", urin);
            _navigationManager.NavigateTo("userroleinmarathon/get-all");

        }
    }
}
