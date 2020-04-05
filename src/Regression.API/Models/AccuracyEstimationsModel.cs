using System.Collections.Generic;

namespace Regression.API.Models
{
    public class AccuracyEstimationsModel
    {
        public ICollection<double> ApproximationOutputs { get; set; }
        public ICollection<double> DiscreteOutput { get; set; }
        public double SquareSumMax { get; set; }
        public double Correlation { get; set; }
    }
}