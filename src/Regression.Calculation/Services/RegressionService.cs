using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        public ICollection<double> CalculateRegression(ICollection<Record> records)
        {
            if (records.Count == 0)
                return new Collection<double>();
            
            if (records.Any(record => record.Inputs.Count != records.First().Inputs.Count))
                throw new ArgumentException("Arguments of rows are not equal!");
            
            if (records.Count <= records.First().Inputs.Count)
                throw new ArgumentException("Count of rows should be greater than arguments!");
            
            var operandsSize = records.First().Inputs.Count + 1;
            
            Matrix aij = new Matrix(operandsSize, operandsSize);
            Matrix bi = new Matrix(operandsSize, 1);

            bi[0, 0] = records.Select(s => s.Output).Sum();
            aij[0, 0] = records.Count;
            
            for (int i = 0; i < aij.H - 1; i++)
            {
                aij[i + 1, 0] = records.Select(s => s.Inputs.ElementAt(i)).Sum();
                aij[0, i + 1] = records.Select(s => s.Inputs.ElementAt(i)).Sum();
            }

            for (var i = 1; i < aij.H; i++)
            {
                bi[i, 0] = records.Select(s => s.Output * s.Inputs.ElementAt(i - 1)).Sum();

                for (var j = 1; j < aij.W; j++)
                    aij[i, j] = records.Select(s => s.Inputs.ElementAt(j - 1) * s.Inputs.ElementAt(i - 1)).Sum();
            }

            Matrix elimination = _eliminationService.Eliminate(aij, bi);
            var result = new double[elimination.H];

            for (int i = 0; i < result.Length; i++)
                result[i] = elimination[i, 0];

            return result;
        }
    }
}