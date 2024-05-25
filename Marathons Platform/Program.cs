

using AutoMapper;
using Marathons_Platfom.Core.Context;
using Marathons_Platfom.Core.Entities;
using Marathons_Platfom.UI2.Infrastructure.DTOs;
using Marathons_Platfom.UI2.Infrastructure.JWT;
using Marathons_Platform.API.Implementation;
using Marathons_Platform.API.Interfaces;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platform.Domain.Repositories;
using Marathons_Platform.Helpers;
using Marathons_Platform.OptionsSetup;
using Marathons_Platform.Repositories.Interfaces;
using Marathons_Platform.Repositories.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace Marathons_Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Marathons_Platform", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "Bearer",
                                        Type = ReferenceType.SecurityScheme
                                    }
                                },
                                new List<string>()
                            }
                        });
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();
            builder.Services.ConfigureOptions<JwtOptionsSetup>();
            builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                                                             .AllowAnyHeader()
                                                             .AllowAnyMethod());
            });

            builder.Services.AddDbContext<MarathonPlatformContext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                    , options => options.EnableRetryOnFailure());
            });
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = null;

            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IRepository<Badge>,BadgeRepository>();
            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<IRepository<Theme>, ThemeRepository>();
            builder.Services.AddScoped<IRepository<User_Badge>, UserBadgeRepository>();
            builder.Services.AddScoped<IRepository<Progress>, ProgressRepository>();
            builder.Services.AddScoped<IRepository<Exercise>, ExerciseRepository>();
            builder.Services.AddScoped<IRepository<Marathon>, MarathonRepository>();
            builder.Services.AddScoped<IRepository<Role>, RoleRepository>();
            builder.Services.AddScoped<IRepository<UserRoleInMarathon>, UserRoleInMarathonRepository>();
            builder.Services.AddScoped<IRepository<UserExerciseStatus>, UserExerciseStatusRepository>();
            builder.Services.AddScoped<IRepository<BadgeMarathon>, BadgeMarathonRepository>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            var app = builder.Build();
           
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "BadgePhotos");

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(dir),
                RequestPath = "/images"
            });


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowOrigin");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
