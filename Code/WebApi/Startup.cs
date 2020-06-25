using Autofac;
using CodePepper.DataAccess;
using CodePepper.Domain;
using CodePepper.Domain.Business.StarWars;
using CodePepper.WebUi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace CodePepper.WebUi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<AppDbContext>(x => x.UseSqlite("Data Source=StarWarsDatabase.db"));

            services.AddSwagger();
        }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            Assembly[] assemblies = {
                typeof(AppDbContext).Assembly,
                typeof(ValidationRules).Assembly,
                typeof(StarWarsService).Assembly
            };

            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces().AsSelf();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbContext.Database.Migrate();
        }
    }
}
