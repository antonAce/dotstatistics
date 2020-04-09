using Microsoft.Extensions.DependencyInjection;

using Regression.BL.Interfaces;
using Regression.BL.Services;

namespace Regression.BL.Providers
{
    public static class StorageServicesProviders
    {
        public static void AddStorageServicesForDatasets(this IServiceCollection services)
        {
            services.AddTransient<IDatasetParser, DatasetParser>();
            services.AddScoped<IDatasetService, DatasetService>();
        }
    }
}