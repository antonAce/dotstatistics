using System.Collections.Generic;

namespace DotStatistics.Numeric.Primitives
{
    public class Polynomial : IPolynomial
    {
        private readonly IDictionary<double, double> _powerSets;

        public Polynomial(IDictionary<double, double> powerSets) =>
            _powerSets = powerSets;

        public double this[double power]
        {
            get => _powerSets[power];
            set => _powerSets[power] = value;
        }
    }
}
