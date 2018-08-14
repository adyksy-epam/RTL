using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using RTL.TvMazeScraper.Data.Context;
using RTL.TvMazeScraper.Data.Repository;
using RTL.TvMazeScraper.Scraper.App;
using RTL.TvMazeScraper.Scraper.App.Converters;
using RTL.TvMazeScraper.Scraper.App.Formatters;
using RTL.TvMazeScraper.Scraper.App.Http;
using RTL.TvMazeScraper.Scraper.App.Providers;
using RTL.TvMazeScraper.Scraper.App.Settings;
using RTL.TvMazeScraper.Scraper.WebAPI.Services;

namespace RTL.TvMazeScraper.Scraper.WebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigureDependencies(services);

            ConfigureScraper(services);

            ConfigureDatabase(services);
        }

        private void ConfigureScraper(IServiceCollection services)
        {
            var apiSettings = _configuration.GetSection("Api").Get<ApiSettings>();
            var httpSettings = _configuration.GetSection("Http").Get<HttpSettings>();
            services.Add(ServiceDescriptor.Singleton(apiSettings));
            services.Add(ServiceDescriptor.Singleton(httpSettings));
        }

        private void ConfigureDependencies(IServiceCollection services)
        {
            services.AddSingleton<IDateTimeConverter, SafeDateTimeConverter>();
            services.AddSingleton<IBackgroundTaskQueue, ScraperTaskQueue>();
            services.AddHostedService<ScraperHostedService>();

            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IRestClient, RestClient>();
            services.AddTransient<IHttpClient, RateLimitedHttpClient>();
            services.AddTransient<IContentFormatter, TvMazeContentFormatter>();
            services.AddTransient<IApiProvider, TvMazeApiProvider>();
            services.AddTransient<IApiScraper, TvMazeApiScraper>();
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
            }, ServiceLifetime.Transient);
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
