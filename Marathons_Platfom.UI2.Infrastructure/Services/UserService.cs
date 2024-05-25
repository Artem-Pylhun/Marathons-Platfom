using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Marathons_Platfom.UI2.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Marathons_Platfom.UI2.Infrastructure.Services
{
    public class UserService : IUserService<UserDto, UserCreateDto, UserUpdateDto>
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public List<UserDto> Users { get; set; } = new List<UserDto>();

        public UserService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public async Task DelUser(int id)
        {
            await _http.DeleteAsync($"api/user/delete/{id}");
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var result = await _http.GetAsync($"api/user/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<UserDto>();
            }
            return null;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<UserDto>>("api/user/get-all");
                if (result != null)
                {
                    Users = result;
                }
                else
                {
                    Console.WriteLine("Response is null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
            }
            return Users;


        }

        public async Task LoginUser(UserLogDto user)
        {
            await _http.PostAsJsonAsync("api/user/login", user);
        }

        public async Task RegisterUser(UserCreateDto user)
        {
            await _http.PostAsJsonAsync("api/user/register", user);

        }

        public async Task UpdateUser(UserDto user)
        {
            await _http.PutAsJsonAsync("api/user/update", user);
            _navigationManager.NavigateTo("");
        }
    }
}
