using DomainEvents.Demo.Data;
using DomainEvents.Demo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DomainEvents.Demo
{
    public class Startup
    {
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
        {
        _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices( IServiceCollection services)
            {
            services.AddMvc();
            services.Scan(scan => scan
                    .FromAssemblyOf<Program>()
            .AddClasses(classes => classes.AssignableTo(typeof(IHandle<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            services.AddScoped<DomainEvents>();
            services.AddScoped<IEmpRepository, EmpRepository>();
            services.AddDbContext<MyDbContext>(cfg =>
                {
                cfg.UseSqlServer(_config.GetConnectionString("MyDbConnectionString"));
                });
            services.AddTransient<DataSeeder>();
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            {
            if (env.IsDevelopment())
                {
                app.UseDeveloperExceptionPage();
                }
            app.UseDomainEvents(); // should be registered as one of the first middlewears
            app.UseMvc(cfg => { cfg.MapRoute("Default","{controller}/{action}/{id?}",new{controller="Home", Action="Index"}); });
            if (env.IsDevelopment())
                {
                using (var scope = app.ApplicationServices.CreateScope())
                    {
                    var seeder = scope.ServiceProvider.GetService<DataSeeder>();
                    seeder.Seed().Wait();
                    }
                }
            }
    }
}
