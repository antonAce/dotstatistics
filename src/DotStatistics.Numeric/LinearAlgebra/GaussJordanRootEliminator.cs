using System;
using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.LinearAlgebra
{
    public class GaussJordanRootEliminator : IRootEliminator
    {
        public IMatrix Eliminate(IMatrix coefficients, IMatrix outputs)
        {
            if (coefficients.Width != coefficients.Height)
                throw new InvalidOperationException("The coefficient matrix should be squared.");
            
            if (outputs.Width != 1)
                throw new InvalidOperationException("The width of the outputs vector should equal to 1.");
            
            if (coefficients.Height != outputs.Height)
                throw new InvalidOperationException("The height of the coefficient matrix and outputs vector should match.");

            var roots = Matrix.Zeros(coefficients.Height, 1);
            var mergedCoefficients = new double[coefficients.Height, coefficients.Width + 1];

            for (int i = 0; i < coefficients.Height; i++)
            {
                for (int j = 0; j < coefficients.Width + 1; j++)
                    mergedCoefficients[i, j] = j != coefficients.Width
                        ? coefficients[i, j]
                        : outputs[i, 0];
            }

            // Coefficients matrix triangulation
            for (var i = 0; i < coefficients.Height - 1; i++)
            {
                var pivot = mergedCoefficients[i, i];
                
                for (var j = i + 1; j < coefficients.Width; j++)
                {
                    var factor = mergedCoefficients[j, i] / pivot;
                    
                    for (var k = 0; k < coefficients.Width + 1; k++)
                        mergedCoefficients[j, k] -= factor * mergedCoefficients[i, k];

                    if (mergedCoefficients[j, j] == 0)
                        throw new InvalidOperationException("Coefficient matrix has linear dependent rows.");
                }
            }
            
            // Reverse triangulation
            for (var i = coefficients.Height - 1; i >= 0; i--)
            {
                var rowSum = 0.0;

                for (var j = i + 1; j < coefficients.Width; j++)
                    rowSum += mergedCoefficients[i, j] * roots[j, 0];

                roots[i, 0] = (mergedCoefficients[i, coefficients.Width] - rowSum) / mergedCoefficients[i, i];
            }

            return roots;
        }
    }
}