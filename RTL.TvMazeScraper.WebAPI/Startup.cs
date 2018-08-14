using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RTL.TvMazeScraper.Data.Context;
using RTL.TvMazeScraper.Data.Repository;
using RTL.TvMazeScraper.Domain.Formatters;
using RTL.TvMazeScraper.Domain.Providers;
using RTL.TvMazeScraper.WebAPI.Filters;

namespace RTL.TvMazeScraper.WebAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigureDependencies(services);

            ConfigureDatabase(services);
        }

        private void ConfigureDependencies(IServiceCollection services)
        {
            services.AddSingleton<IDateTimeFormatter, BirthDayFormatter>();

            services.AddTransient<IReadOnlyRepository, ReadOnlyRepository>();
            services.AddTransient<IShowProvider, ShowProvider>();
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            if (string.IsNullOrWhiteSpace(_configuration.GetConnectionString("DefaultConnection")))
            {
                throw new InvalidOperationException("Connection string is not provided");
            }

            services.AddDbContext<TvMazeDbContext>(options =>
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
