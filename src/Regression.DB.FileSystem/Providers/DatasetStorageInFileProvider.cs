using Microsoft.Extensions.DependencyInjection;

using Regression.Domain.Interfaces;
using Regression.DB.FileSystem.Repositories;

namespace Regression.DB.FileSystem.Providers
{
    public static class DatasetStorageInFileProvider
    {
        public static void AddFileStorageForDatasets(this IServiceCollection services, string basePath)
        {
            services.AddScoped<IDatasetRepository>(service => new DatasetRepository()
                .UseStoragePath(basePath)
                .UseInputSeparationSymbol(',')
                .UseOutputSeparationSymbol(';'));
        }
    }
}