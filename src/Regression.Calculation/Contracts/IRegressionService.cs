using Regression.Calculation.DTO;

namespace Regression.Calculation.Contracts
{
    public interface IRegressionService
    {
        double[] CalculateRegression(Record[] records);
    }
}