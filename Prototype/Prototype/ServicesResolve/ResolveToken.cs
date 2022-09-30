using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Api.ServicesResolve
{
    internal static class ResolveToken
    {
        internal static IServiceCollection AddTokenDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });


            var key = Encoding.ASCII.GetBytes(configuration.GetSection("TokenConfig:HashKey").Value);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "prototype",
                    ValidAudience = "prototype",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                options.Events = new JwtBearerEvents

                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Token Inválido...:" + context.Exception.Message.ToString());
                        return Task.CompletedTask;
                    },

                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token válido...:" + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

    }
}
