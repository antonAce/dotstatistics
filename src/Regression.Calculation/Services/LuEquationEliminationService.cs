using System;
using Regression.Calculation.Contracts;
using Regression.Calculation.DTO;

namespace Regression.Calculation.Services
{
    public class LuEquationEliminationService : IEquationEliminationService
    {
        public Matrix Eliminate(Matrix core, Matrix constants)
        {
            int rowIndex = core.H;
            
            // L * result = byproduct;
            Matrix result = new Matrix(core.H, 1);
            
            // U * byproduct = constants;
            Matrix byproduct = new Matrix(core.H, 1);

            var convertedMatrix = LuDecompose(core);

            for (var i = 0; i < rowIndex; i++)
            {
                double constantSum = 0;
                
                for (var j = 0; j < i; j++)
                    constantSum += byproduct[j, 0] * convertedMatrix.LMatrix[i, j];

                CheckDivideByZeroForDouble(convertedMatrix.LMatrix[i, i]);
                byproduct[i, 0] = (constants[i, 0] - constantSum) / convertedMatrix.LMatrix[i, i];
            }
            
            for (int i = rowIndex - 1; i >= 0; i--)
            {
                double constantSum = 0;
                
                for (int j = i; j < rowIndex; j++)
                    constantSum += result[j, 0] * convertedMatrix.UMatrix[i, j];
                
                CheckDivideByZeroForDouble(convertedMatrix.UMatrix[i, i]);
                result[i, 0] = (byproduct[i, 0] - constantSum) / convertedMatrix.UMatrix[i, i];
            }

            return result;
        }

        private (Matrix LMatrix, Matrix UMatrix) LuDecompose(Matrix source)
        {
            int rowIndex = source.H;
            Matrix lower = new Matrix(rowIndex, source.W);
            Matrix upper = new Matrix(source.H, source.W);
            
            for (var i = 0; i < rowIndex; i++) 
            {
                for (var k = i; k < rowIndex; k++) 
                {
                    double sum = 0;
                    
                    for (var j = 0; j < i; j++) 
                        sum += (lower[i,j] * upper[j,k]);
                    
                    upper[i, k] = source[i, k] - sum; 
                } 
                
                for (var k = i; k < rowIndex; k++)  
                { 
                    if (i == k) 
                        lower[i, i] = 1;
                    else
                    {
                        double sum = 0; 
                        
                        for (var j = 0; j < i; j++) 
                            sum += (lower[k, j] * upper[j, i]);
                        

                        CheckDivideByZeroForDouble(upper[i, i]);
                        lower[k, i] = (source[k, i] - sum) / upper[i, i]; 
                    } 
                } 
            } 
            
            return (LMatrix: lower, UMatrix: upper);
        }

        private void CheckDivideByZeroForDouble(double number)
        {
            if (Math.Abs(number) < Double.Epsilon)
                throw new DivideByZeroException();
        }
    }
}