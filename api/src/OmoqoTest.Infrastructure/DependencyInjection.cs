using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OmoqoTest.Application.Common.Interfaces.Authentication;
using OmoqoTest.Application.Repositories;
using OmoqoTest.Infrastructure.Authentication;
using OmoqoTest.Infrastructure.Persistence;
using OmoqoTest.Infrastructure.Persistence.Repositories;

namespace OmoqoTest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {

            services.AddAuth(configuration);
            services.AddPersistance();

            return services;
        }

        public static IServiceCollection AddAuth(
               this IServiceCollection services,
               ConfigurationManager configuration)
        {
            var JwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectioName, JwtSettings);

            services.AddSingleton(Options.Create(JwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtSettings.Issuer,
                    ValidAudience = JwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Secret))
                });

            return services;
        }

        public static IServiceCollection AddPersistance(
               this IServiceCollection services)
        {
            services.AddDbContext<OmoqoTestDbContext>(options => 
                options.UseInMemoryDatabase("OmoqoTest"));
                
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IShipRepository, ShipRepository>();

            return services;
        }


    }
}