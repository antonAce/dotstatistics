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

            var coefficientsCopy = coefficients.Copy();
            var result = outputs.Copy();
            
            // Swap non-zero axis
            for (var j = 0; j < coefficientsCopy.Height - 1; j++)
            {
                if (coefficientsCopy[j, j] != 0) continue;

                var pivotAxis = j + 1;

                for (int i = j + 1; i < coefficientsCopy.Height; i++)
                {
                    if (coefficientsCopy[i, j] == 0) continue;
                    pivotAxis = i;
                    break;
                }
                
                if (coefficientsCopy[pivotAxis, j] == 0)
                    throw new InvalidOperationException("System doesn't have unique solution.");
                
                var swapAxis = new double[coefficientsCopy.Height + 1];

                for (var i = 0; i < coefficientsCopy.Height; i++)
                {
                    swapAxis[i] = coefficientsCopy[pivotAxis, i];
                    coefficientsCopy[pivotAxis, i] = coefficientsCopy[j, i];
                    coefficientsCopy[j, i] = swapAxis[i];
                }
            }
        
            // Triangulate matrix
            for (var i = 0; i < coefficientsCopy.Height - 1; i++)
            {
                for (var j = i + 1; j < coefficientsCopy.Height; j++)
                {
                    var rowPivot = coefficientsCopy[i, i];
                    var columnPivot = coefficientsCopy[j, i];

                    for (var k = 0; k < coefficientsCopy.Height + 1; k++)
                    {
                        coefficientsCopy[j, i] = coefficientsCopy[j, i] * rowPivot - coefficientsCopy[i, i] * columnPivot;
                    }
                }
            }

            return result;
        }
    }
}