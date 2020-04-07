using System;
using System.Linq;
using System.Collections.Generic;

using Regression.Calculation.Contracts;
using Regression.Calculation.DTO;

namespace Regression.Calculation.Services
{
    public class RegressionEstimationService : IRegressionEstimationService
    {
        private readonly IRegressionService _regressionService;
        
        public RegressionEstimationService(IRegressionService regressionService)
        {
            _regressionService = regressionService;
        }
        
        public AccuracyEstimates CalculateEstimates(ICollection<Record> records)
        {
            var accuracyEstimates = new AccuracyEstimates();
            var polynomial = _regressionService.CalculateRegression(records);
            
            var approximatedPairs = records.Select((record) =>
            {
                double output = 0.0;
                
                for (int i = 1; i <= record.Inputs.Count; i++)
                    output += record.Inputs.ElementAt(i - 1) * polynomial.ElementAt(i);

                return new Record
                {
                    Inputs = record.Inputs,
                    Output = output + polynomial.ElementAt(0)
                };
            }).ToArray();

            accuracyEstimates.ApproximationOutputs = approximatedPairs.Select(pair => pair.Output).ToArray();

            accuracyEstimates.SquareSumMax = records.Join(approximatedPairs,
                r => r.Inputs,
                est => est.Inputs,
                (r, est) => new
                {
                    DiscreteOutput = r.Output,
                    EstimatedOutput = est.Output
                }).Max(pair => Math.Pow(pair.DiscreteOutput - pair.EstimatedOutput, 2));

            var outputsAvg = records.Select(record => record.Output).Average();
            var approximatedPairsAvg = approximatedPairs.Select(record => record.Output).Average();

            var outputCov = records
                .Zip(approximatedPairs, (o, a) => (o.Output - outputsAvg) * (a.Output - approximatedPairsAvg))
                .Sum();

            var outputDispX = records.Sum(x => Math.Pow(x.Output - outputsAvg, 2));
            var outputDispY = approximatedPairs.Sum(x => Math.Pow(x.Output - approximatedPairsAvg, 2));

            var outputDispMult = Math.Sqrt(outputDispX * outputDispY);
            
            accuracyEstimates.Correlation = outputCov / outputDispMult;

            return accuracyEstimates;
        }
    }
}