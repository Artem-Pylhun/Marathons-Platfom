using Blazored.LocalStorage;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Marathons_Platfom.UI2.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;

namespace Marathons_Platform.UI1.Auth
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IRoleService<RoleDto, RoleCreateDto> _roleService;
        private readonly IUserService<UserDto,UserCreateDto,UserUpdateDto> _userService;
        private readonly NavigationManager _nav;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, NavigationManager nav,IUserService<UserDto, UserCreateDto, UserUpdateDto> userService, IRoleService<RoleDto, RoleCreateDto> roleService, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _roleService = roleService;
            _userService = userService;
            _nav = nav;
        }

        public async Task<bool> Login(UserLogDto userLoginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/login", userLoginDto);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

            await _localStorage.SetItemAsync("jwt", tokenResponse.Token);

            return true;
        }
        public async Task CheckAndRemoveExpiredToken()
        {
            var token = await _localStorage.GetItemAsync<string>("jwt");
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    await _localStorage.RemoveItemAsync("jwt");
                }
            }
        }
        public async Task<bool> IsUserAuthenticated()
        {
            var token = await _localStorage.GetItemAsync<string>("jwt");
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                if (jwtToken.ValidTo > DateTime.UtcNow)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("jwt");
            _nav.NavigateTo("/user/login");
        }
        public async Task<int> GetUserId()
        {
            var token = await _localStorage.GetItemAsync<string>("jwt");
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var emailClaims = jwtToken.Claims.Where(c => c.Type == "email").Select(c => c.Value).ToList();

                foreach (var emailClaim in emailClaims)
                {
                    var users = await _userService.GetUsers();
                    var user = users.FirstOrDefault(u => u.Email == emailClaim);
                    if(user.Id != 0)
                    {
                        return user.Id;
                    }

                }
                
            }

            return 0;
        }
        public async Task<Dictionary<int,List<string>>> GetUserRoles()
        {
            var token = await _localStorage.GetItemAsync<string>("jwt");
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);


                var roleClaims = jwtToken.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();


                if (roleClaims.Count > 0)
                {
                    var urinOfUser = JsonConvert.DeserializeObject<List<UserRoleInMarathonDto>>(roleClaims[0]);

                    var roles = new Dictionary<int, List<string>>();

                    foreach (var urin in urinOfUser)
                    {
                        RoleDto r = await _roleService.GetRoleById(urin.RoleId);
                        var roleName = r.Title;

                        if (roles.ContainsKey(urin.MarathonId))
                        {
                            roles[urin.MarathonId].Add(roleName);
                        }
                        else
                        {
                            roles.Add(urin.MarathonId, new List<string> { roleName });
                        }
                    }

                    return roles;

                }
            }
            return null; 
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
