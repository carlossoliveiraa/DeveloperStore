using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DeveloperStore.Sales.API.Extensions
{
    public static class JwtAuthenticationExtensions
    {
        public static void AddCustomJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration["Jwt:Key"] ?? "MySuperSecureKey123";
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
