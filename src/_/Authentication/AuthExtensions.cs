using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace $safeprojectname$._.Authentication
{
    public static class AuthExtensions
    {
        /// <summary>
        /// Adds JWT Bearer authentication. Verifies if a JWT token was passed to the header, whether it is issued by the right authority, and whether the audience is the right one
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            string authority = configuration["Auth0:Authority"];
            string apiIdentifier = configuration["Auth0:ApiIdentifier"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = apiIdentifier;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
        }
    }
}
