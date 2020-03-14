using System;

namespace Regression.Calculation.DTO
{
    public class Matrix
    {
        /// <summary>
        /// Matrix values holder
        /// </summary>
        protected double[,] _core = {};

        /// <summary>
        /// Rows amount of matrix
        /// </summary>
        public int W { get => _core.Length / H; }

        /// <summary>
        /// Columns amount of matrix
        /// </summary>
        public int H { get => _core.GetUpperBound(0) + 1; }

        /// <summary>
        /// Matrix values holder
        /// </summary>
        public double[,] Core { get => _core; }

        /// <summary>
        /// Creates new matrix based on 2D array
        /// </summary>
        /// <param name="core">2D array</param>
        public Matrix(double[,] core)
        {
            if (core.Rank != 2)
                throw new InvalidOperationException("Array rank doesn't match the matrix rank");

            _core = core.Clone() as double[,];
        }

        /// <summary>
        /// Creates new null-matrix based on rows and columns amount
        /// </summary>
        /// <param name="height">Columns amount</param>
        /// <param name="width">Rows amount</param>
        public Matrix(int height, int width)
        {
            _core = new double[height, width];

            for (uint i = 0; i < _core.GetLength(0); i++)
            {
                for (uint j = 0; j < _core.GetLength(1); j++)
                    _core[i, j] = 0;
            }
        }

        /// <summary>
        /// Gives access to element of matrix
        /// </summary>
        /// <param name="x">Column index</param>
        /// <param name="y">Row index</param>
        /// <returns>Element in position, where x = column index and y = row index</returns>
        public double this[int x, int y]
        {
            get => _core[x, y];
            set => _core[x, y] = value;
        }

        /// <summary>
        /// Perform summing operation for 2 matrices. Column and row size of matrices must be equal.
        /// </summary>
        /// <param name="leftMatrix">First matrix operand</param>
        /// <param name="rightMatrix">Second matrix operand</param>
        /// <returns>Product of adding 2 matrices</returns>
        public static Matrix operator +(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.H != rightMatrix.H || leftMatrix.W != rightMatrix.W)
                throw new InvalidOperationException("Operands sizes doesn't match!");

            Matrix result = new Matrix(leftMatrix.H, leftMatrix.W);

            for (int i = 0; i < result.H; i++)
            {
                for (int j = 0; j < result.W; j++)
                    result[i, j] = leftMatrix[i, j] + rightMatrix[i, j];
            }

            return result;
        }

        /// <summary>
        /// Perform subtraction operation for 2 matrices. Column and row size of matrices must be equal.
        /// </summary>
        /// <param name="leftMatrix">First matrix operand</param>
        /// <param name="rightMatrix">Second matrix operand</param>
        /// <returns>Product of subtraction 2 matrices</returns>
        public static Matrix operator -(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.H != rightMatrix.H || leftMatrix.W != rightMatrix.W)
                throw new InvalidOperationException("Operands sizes doesn't match!");

            Matrix result = new Matrix(leftMatrix.H, leftMatrix.W);

            for (int i = 0; i < result.H; i++)
            {
                for (int j = 0; j < result.W; j++)
                    result[i, j] = leftMatrix[i, j] - rightMatrix[i, j];
            }

            return result;
        }

        /// <summary>
        /// Perform multiplication operation for 2 matrices. Columns amount must match the row amount.
        /// </summary>
        /// <param name="leftMatrix">First matrix operand</param>
        /// <param name="rightMatrix">Second matrix operand</param>
        /// <returns>Product of multiplication 2 matrices</returns>
        public static Matrix operator *(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.W != rightMatrix.H)
                throw new InvalidOperationException(
                    "Rows amount of first operand must match columns amount of second!");

            Matrix result = new Matrix(leftMatrix.H, rightMatrix.W);

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

        /// <summary>
        /// Perform multiplication operation for matrix and scalar value => R.
        /// </summary>
        /// <param name="scalar">Scalar value => R</param>
        /// <param name="rightMatrix">Matrix Operand</param>
        /// <returns></returns>
        public static Matrix operator *(double scalar, Matrix rightMatrix)
        {
            Matrix result = new Matrix(rightMatrix.H, rightMatrix.W);

            for (int i = 0; i < result.H; i++)
            {
                for (int j = 0; j < result.W; j++)
                    result[i, j] = rightMatrix[i, j] * scalar;
            }

            return result;
        }

        /// <summary>
        /// Perform power raising operation. Matrix operand must be square (N = M)
        /// </summary>
        /// <param name="matrix">Matrix operand. Square only!</param>
        /// <param name="power">Power raising operand</param>
        /// <returns>Result of power raising</returns>
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

        /// <summary>
        /// Returns [[ ... ], ... , [ ... ]] like version of matrix
        /// </summary>
        /// <returns>A string of matrix representation</returns>
        public override string ToString()
        {
            string result = "";
            result += "[";

            for (uint i = 0; i < this.H; i++)
            {
                result += "[";

                for (uint j = 0; j < this.W - 1; j++)
                    result += $" {_core[i, j]},";

                if (this.W >= 1)
                    result += $" {_core[i, this.W - 1]} ";

                if (i == (this.H - 1))
                    result += "]";
                else
                    result += "], ";
            }

            result += "]";
            return result;
        }
    }
}