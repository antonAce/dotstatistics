using System;
using System.Linq;
using DotStatistics.Numeric.Extensions;
using DotStatistics.Numeric.LinearAlgebra;
using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.Optimization
{
    public class LeastSquaresRegressionOptimizer : IRegressionOptimizer
    {
        private IPolynomial _fittedPolynomial;
        private IRootEliminator _rootEliminator = new GaussJordanRootEliminator();
        
        public IPolynomial Fit(IMatrix features, IMatrix values, double error = Double.Epsilon)
        {
            if (values.Width != 1)
                throw new InvalidOperationException("The width of the values vector should equal to 1.");
            
            if (features.Height != values.Height)
                throw new InvalidOperationException("The height of the input features matrix and values vector should match.");

            if (features.Height <= features.Width)
                throw new InvalidOperationException("The height of the input features matrix should be greater than the width.");

            var x = Matrix.Zeros(features.Width + 1, features.Width + 1);
            var y = Matrix.Zeros(features.Width + 1, 1);
            
            x[0, 0] = features.Height;
            y[0, 0] = Enumerable.Range(0, values.Height).Sum(i => values[i, 0]);

            for (var i = 1; i < x.Height; i++)
            {
                y[i, 0] = Enumerable.Range(0, values.Height).Sum(j => features[i - 1, j] * values[j, 0]);

                var sideElement = Enumerable.Range(0, values.Height).Sum(j => features[i - 1, j]);

                x[i, 0] = sideElement;
                x[0, i] = sideElement;
                
                for (var j = 1; j < x.Width; j++)
                    x[i, j] = Enumerable.Range(0, values.Height).Sum(k => features[i - 1, k] * features[j - 1, k]);
            }

            var elimination = _rootEliminator.Eliminate(x, y);
            var fittedPolynomial = new Polynomial(
                Enumerable.Range(0, elimination.Height).ToDictionary(i => i, i => elimination[i, 0]));

            _fittedPolynomial = fittedPolynomial;
            return fittedPolynomial;
        }

        public IMatrix Predict(IMatrix inputs)
        {
            if (_fittedPolynomial is null)
                throw new InvalidOperationException("Fit polynomial first.");
            
            var predictions = Matrix.Zeros(inputs.Height, 1);

            for (var i = 0; i < inputs.Height; i++)
            {
                predictions[i, 0] = _fittedPolynomial[0] +
                              Enumerable.Range(1, inputs.Width).Sum(j => inputs[i, j] * _fittedPolynomial[j]);
            }

            return predictions;
        }

        public double Score(IMatrix inputs, IMatrix values)
        {
            var predictions = Predict(inputs);
            var valMean = Enumerable.Range(0, values.Height)
                .Sum(k => values[k, 0]) / values.Height;

            double squaredSumResidue = 0.0, squaredSumTotal = 0.0;
            
            for (var i = 0; i < inputs.Height; i++)
            {
                squaredSumResidue += Math.Pow(values[i, 0] - predictions[i, 0], 2);
                squaredSumTotal += Math.Pow(values[i, 0] - valMean, 2);
            }
            
            return 1.0 - squaredSumResidue / squaredSumTotal;
        }

        public IRootEliminator RootEliminator
        {
            set => _rootEliminator = value;
        }
    }
}
