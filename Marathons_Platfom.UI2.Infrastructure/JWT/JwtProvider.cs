using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platform.Repositories.Interfaces;
using Marathons_Platform.Repositories.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platfom.UI2.Infrastructure.JWT
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IRepository<UserRoleInMarathon> _urinRepository;

        public JwtProvider(IOptions<JwtOptions> jwtOptions, IRepository<UserRoleInMarathon> urinRepository)
        {
            _urinRepository = urinRepository;

            _jwtOptions = jwtOptions.Value;
        }
        public string Generate(User user)
        {
            var urinOfUser = ((UserRoleInMarathonRepository)_urinRepository).GetByUserId(user.Id);

            var claims = new Claim[]
            {
               new("email", user.Email),
               new("role",JsonConvert.SerializeObject(urinOfUser))
            };
            var signingKey = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
               _jwtOptions.Issuer,
               _jwtOptions.Audience,
               claims,
               null,
               DateTime.UtcNow.AddHours(1),
               signingKey);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
