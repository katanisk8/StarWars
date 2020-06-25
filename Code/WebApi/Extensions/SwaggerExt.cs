using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CodePepper.WebUi.Extensions
{
    public static class SwaggerExt
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Star Wars API",
                    Description = "ASP.NET Core Web API for Star Wars portal.",
                    Contact = new OpenApiContact
                    {
                        Name = "Michal Makowej",
                        Email = "michal@makowej.pl",
                        Url = new Uri("https://www.linkedin.com/in/micha%C5%82-makowej/"),
                    }
                });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            SwaggerBuilderExtensions.UseSwagger(app);
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Star Wars API v1");
            });

            return app;
        }
    }
}
