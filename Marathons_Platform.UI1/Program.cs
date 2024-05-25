using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Marathons_Platfom.UI2.Infrastructure.Interfaces;
using Marathons_Platfom.UI2.Infrastructure.Services;
using Marathons_Platform.UI1.Auth;
using Blazored.LocalStorage;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Marathons_Platform.UI1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5204/") });
            builder.Services.AddScoped<IUserService<UserDto, UserCreateDto, UserUpdateDto>, UserService>();
            builder.Services.AddScoped<IBadgeService<BadgeDto, BadgeCreateDto, BadgeUpdateDto>, BadgeService>();
            builder.Services.AddScoped<IThemeService<ThemeDto, ThemeCreateDto>, ThemeService>();
            builder.Services.AddScoped<IRoleService<RoleDto, RoleCreateDto>, RoleService>();
            builder.Services.AddScoped<IMarathonService<MarathonDto, MarathonCreateDto, MarathonUpdateDto>, MarathonService>();
            builder.Services.AddScoped<IExerciseService<ExerciseDto, ExerciseCreateDto, ExerciseUpdateDto>, ExerciseService>();
            builder.Services.AddScoped<IProgressService<ProgressDto, ProgressCreateDto, ProgressAddDto>, ProgressService>();
            builder.Services.AddScoped<IUserRoleInMarathon<UserRoleInMarathonDto, UserRoleInMarathonCreateDto>, UserRoleInMarathon>();
            builder.Services.AddScoped<IUserExerciseStatusService<UserExerciseStatusesDto, UserExerciseStatusesCreateDto>, UserExerciseStatusService>();
            builder.Services.AddScoped<IUserBadgeService<UserBadgeDto, UserBadgeCreateDto>, UserBadgeService>();
            builder.Services.AddScoped<IBadgeMarathonService<BadgeMarathonDto, BadgeMarathonCreateDto>, BadgeMarathonService>();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddBlazoredLocalStorage();
            await builder.Build().RunAsync();

        }
    }
}
