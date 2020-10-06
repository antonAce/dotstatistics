using System;

namespace DotStatistics.Numeric.Primitives
{
    public class Matrix : IMatrix
    {
        private readonly double[,] _core;

        public int Width => _core.GetLength(0);

        public int Height => _core.GetLength(1);

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

        public IMatrix Copy() => new Matrix(_core);

        public static IMatrix Zeros(int height, int width)
        {
            var core = new double[height, width];

            for (int i = 0; i < core.GetLength(0); i++)
            {
                for (int j = 0; j < core.GetLength(1); j++)
                {
                    core[i, j] = 0;
                }
            }
            
            return new Matrix(core);
        }

        public static IMatrix Identity(int height, int width)
        {
            var core = new double[height, width];

            for (int i = 0; i < core.GetLength(0); i++)
            {
                for (int j = 0; j < core.GetLength(1); j++)
                {
                    core[i, j] = i == j ? 1 : 0;
                }
            }

            return new Matrix(core);
        }
    }
}
