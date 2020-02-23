using Microsoft.Extensions.DependencyInjection;
using Regression.BL.Contracts;
using Regression.BL.Services;

namespace Regression.BL.Infrastructure
{
    public static class RegressionServiceProvider
    {
        public static void AddRegression(this IServiceCollection services)
        {
            services.AddSingleton<IRegressionService>(service => new RegressionService(new LuEquationEliminationService()));
        }
    }
}