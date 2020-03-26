using System.Collections.Generic;

namespace Regression.Calculation.DTO
{
    public class AccuracyEstimates
    {
        public ICollection<double> ApproximationOutputs { get; set; }
        public double SquareSumMax { get; set; }
    }
}