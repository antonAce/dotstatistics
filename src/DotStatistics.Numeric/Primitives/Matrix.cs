using System;

namespace DotStatistics.Numeric.Primitives
{
    public class Matrix
    {
        private readonly double[,] _core;

        public int W => _core.Length / H;

        public int H => _core.GetUpperBound(0) + 1;

        public Matrix(double[,] core)
        {
            if (core.Rank != 2)
                throw new InvalidOperationException("Array rank doesn't match the matrix rank");

            _core = core.Clone() as double[,];
        }

        public double this[int x, int y]
        {
            get => _core[x, y];
            set => _core[x, y] = value;
        }

        public static Matrix Zeros(int height, int width)
        {
            var core = new double[height, width];

            for (uint i = 0; i < core.GetLength(0); i++)
            {
                for (uint j = 0; j < core.GetLength(1); j++)
                {
                    core[i, j] = 0;
                }
            }
            
            return new Matrix(core);
        }

        public static Matrix Identity(int height, int width)
        {
            var core = new double[height, width];

            for (uint i = 0; i < core.GetLength(0); i++)
            {
                for (uint j = 0; j < core.GetLength(1); j++)
                {
                    core[i, j] = i == j ? 1 : 0;
                }
            }
            
            return new Matrix(core);
        }
        
        public static Matrix operator +(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.H != rightMatrix.H || leftMatrix.W != rightMatrix.W)
                throw new InvalidOperationException("Operands sizes doesn't match!");

            Matrix result = Zeros(leftMatrix.H, leftMatrix.W);

            for (int i = 0; i < result.H; i++)
            {
                for (int j = 0; j < result.W; j++)
                    result[i, j] = leftMatrix[i, j] + rightMatrix[i, j];
            }

            return result;
        }
        
        public static Matrix operator -(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.H != rightMatrix.H || leftMatrix.W != rightMatrix.W)
                throw new InvalidOperationException("Operands sizes doesn't match!");

            Matrix result = Zeros(leftMatrix.H, leftMatrix.W);

            for (int i = 0; i < result.H; i++)
            {
                for (int j = 0; j < result.W; j++)
                    result[i, j] = leftMatrix[i, j] - rightMatrix[i, j];
            }

            return result;
        }
        
        public static Matrix operator *(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.W != rightMatrix.H)
                throw new InvalidOperationException(
                    "Rows amount of first operand must match columns amount of second!");

            Matrix result = Zeros(leftMatrix.H, rightMatrix.W);

            for (int i = 0; i < result.H; i++)
            {
                for (int j = 0; j < result.W; j++)
                {
                    double elementByproduct = 0;

                    for (int k = 0; k < leftMatrix.W; k++)
                        elementByproduct += leftMatrix[i, k] * rightMatrix[k, j];

                    result[i, j] = elementByproduct;
                }
            }

            return result;
        }
        
        public static Matrix operator *(double scalar, Matrix rightMatrix)
        {
            Matrix result = Zeros(rightMatrix.H, rightMatrix.W);

            for (int i = 0; i < result.H; i++)
            {
                for (int j = 0; j < result.W; j++)
                    result[i, j] = rightMatrix[i, j] * scalar;
            }

            return result;
        }
        
        public static Matrix operator ^(Matrix matrix, int power)
        {
            if (matrix.H != matrix.W)
                throw new ArgumentException("Matrix column size and row size doesn't match!");

            if (power <= 0)
                throw new ArgumentException("Wrong power argument value!");

            Matrix result = matrix * matrix;

            for (int i = 0; i < power - 2; i++)
                result = result * matrix;

            return result;
        }
    }
}
