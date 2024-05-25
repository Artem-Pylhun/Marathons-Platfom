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
    public class RoleService : IRoleService<RoleDto, RoleCreateDto>
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public List<RoleDto> Roles { get; set; }

        public RoleService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public async Task AddRole(RoleCreateDto role)
        {
            await _http.PostAsJsonAsync("api/role/add", role);
            _navigationManager.NavigateTo("role/get-all");
        }

        public async Task DelRole(int id)
        {
            await _http.DeleteAsync($"api/role/delete/{id}");
        }

        public async Task<RoleDto> GetRoleById(int id)
        {
            var result = await _http.GetAsync($"api/role/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<RoleDto>();
            }
            return null;
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<RoleDto>>("api/role/get-all");
                if (result != null)
                {
                    Roles = result;
                }
                else
                {
                    Console.WriteLine("Response is null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching roles: {ex.Message}");
            }
            return Roles;
        }

        public async Task UpdateRole(RoleDto role)
        {
            await _http.PutAsJsonAsync("api/role/update", role);
            _navigationManager.NavigateTo("role/get-all");
        }
    }
}
