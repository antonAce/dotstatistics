using System;
using System.Linq;
using Regression.Calculation.Contracts;
using Regression.Calculation.DTO;

namespace Regression.Calculation.Services
{
    public class RegressionService : IRegressionService
    {
        private readonly IEquationEliminationService _eliminationService;
        
        public RegressionService(IEquationEliminationService eliminationService)
        {
            _eliminationService = eliminationService;
        }
        
        public double[] CalculateRegression(Record[] records)
        {
            if (records.Length == 0)
                return Array.Empty<Double>();
            
            if (records.Any(record => record.Args.Length != records[0].Args.Length))
                throw new ArgumentException("Arguments of rows are not equal!");
            
            if (records.Length <= records[0].Args.Length)
                throw new ArgumentException("Count of rows should be greater than arguments!");
            
            var operandsSize = records.First().Args.Length + 1;
            
            Matrix aij = new Matrix(operandsSize, operandsSize);
            Matrix bi = new Matrix(operandsSize, 1);

            bi[0, 0] = records.Select(s => s.Result).Sum();
            aij[0, 0] = records.Length;
            
            for (int i = 0; i < aij.H - 1; i++)
            {
                aij[i + 1, 0] = records.Select(s => s.Args[i]).Sum();
                aij[0, i + 1] = records.Select(s => s.Args[i]).Sum();
            }

            for (var i = 1; i < aij.H; i++)
            {
                bi[i, 0] = records.Select(s => s.Result * s.Args[i - 1]).Sum();

                for (var j = 1; j < aij.W; j++)
                    aij[i, j] = records.Select(s => s.Args[j - 1] * s.Args[i - 1]).Sum();
            }

            Matrix elimination = _eliminationService.Eliminate(aij, bi);
            var result = new double[elimination.H];

            for (int i = 0; i < result.Length; i++)
                result[i] = elimination[i, 0];

            return result;
        }
    }
}