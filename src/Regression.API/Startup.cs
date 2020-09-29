using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Regression.API.Extensions;
using Regression.API.Filters;
using Regression.API.Helpers;
using Regression.API.Interfaces;
using Regression.Calculation.Infrastructure;
using Regression.BL.Providers;
using Regression.DB.FileSystem.Providers;

namespace Regression.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileParser, FileParser>();
            services.AddFileStorageForDatasets(Path.GetFullPath(Configuration["ModelStorage:Path"]));
            services.AddStorageServicesForDatasets();

            services.AddRegression();
            services.AddRegressionEstimation();

            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseRootSpa(Path.Combine(env.WebRootPath, "index.html"));
        }
    }
}