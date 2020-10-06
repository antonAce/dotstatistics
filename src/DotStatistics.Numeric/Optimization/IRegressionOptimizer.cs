using System;
using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.Optimization
{
    public interface IRegressionOptimizer
    {
        IPolynomial Fit(IMatrix inputs, IMatrix outputs, double error = Double.Epsilon);
        IMatrix Predict(IMatrix inputs);
        double Score(IMatrix inputs);
    }
}