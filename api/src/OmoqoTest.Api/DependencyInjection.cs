using Microsoft.OpenApi.Models;
using OmoqoTest.Api.Common.Mapping;
using System.Reflection;

namespace OmoqoTest.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Omoqo API",
                    Description = "Omoqo interview test API",
                    Contact = new OpenApiContact()
                    {
                        Name = "Fellipe Lima",
                        Email = "fel.melo.lipe@gmail.com"
                    }
                });

                var Contact = new OpenApiContact()
                ;

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. <br/><br/>
                                Enter 'Bearer' [space] and then your token in the text input below.<br/><br/>
                                Example: 'Bearer 12345abcdef'<br/><br/>",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            {Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}},
                        new List<string>()
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddMappings();
            services.AddControllers();

            return services;
        }
    }
}