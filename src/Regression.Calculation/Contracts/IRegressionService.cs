using System.Collections.Generic;
using Regression.Calculation.DTO;

namespace Regression.Calculation.Contracts
{
    public interface IRegressionService
    {
        ICollection<double> CalculateRegression(ICollection<Record> records);
    }
}