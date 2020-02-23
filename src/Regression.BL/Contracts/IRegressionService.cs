using Regression.BL.DTO;

namespace Regression.BL.Contracts
{
    public interface IRegressionService
    {
        double[] CalculateRegression(Record[] records);
    }
}