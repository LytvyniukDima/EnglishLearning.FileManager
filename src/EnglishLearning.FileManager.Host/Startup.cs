using System;
using System.Text.Json.Serialization;
using EnglishLearning.FileManager.Application.Configuration;
using EnglishLearning.FileManager.Host.Infrastructure;
using EnglishLearning.FileManager.Persistence.Configuration;
using EnglishLearning.Utilities.General.Extensions;
using EnglishLearning.Utilities.Identity.Configuration;
using EnglishLearning.Utilities.Persistence.Redis.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EnglishLearning.FileManager.Host
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
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Authorization"));
            });

            services
                .AddControllers(options => options.AddEnglishLearningIdentityFilters())
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddSwaggerDocumentation();

            services
                .AddRedis(Configuration)
                .AddEnglishLearningIdentity();

            services
                .AddPersistenceSettings(Configuration)
                .AddApplicationSettings();
        }
        
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseEnglishLearningExceptionMiddleware();
                
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseSwaggerDocumentation();

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}