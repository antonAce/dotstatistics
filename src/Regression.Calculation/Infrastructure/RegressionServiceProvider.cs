using Microsoft.Extensions.DependencyInjection;
using Regression.Calculation.Contracts;
using Regression.Calculation.Services;

namespace Regression.Calculation.Infrastructure
{
    public static class RegressionServiceProvider
    {
        public static void AddRegression(this IServiceCollection services)
        {
            services.AddSingleton<IRegressionService>(service => new RegressionService(new LuEquationEliminationService()));
        }
        
        public static void AddRegressionEstimation(this IServiceCollection services)
        {
            services.AddSingleton<IRegressionEstimationService, RegressionEstimationService>();
        }
    }
}