using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Marathons_Platfom.UI2.Infrastructure.JWT;
namespace Marathons_Platform.OptionsSetup
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private const string SectionName = "JwtOptions";
        private readonly IConfiguration _configuration;
        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);

        }
    }
}
