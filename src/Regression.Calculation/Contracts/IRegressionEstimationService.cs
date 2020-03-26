using System.Collections.Generic;
using Regression.Calculation.DTO;

namespace Regression.Calculation.Contracts
{
    public interface IRegressionEstimationService
    {
        AccuracyEstimates CalculateEstimates(ICollection<Record> records);
    }
}