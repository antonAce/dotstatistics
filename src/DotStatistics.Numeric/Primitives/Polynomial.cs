using System.Collections.Generic;

namespace DotStatistics.Numeric.Primitives
{
    public class Polynomial : IPolynomial
    {
        private readonly IDictionary<int, double> _powerSets;

        public Polynomial(IDictionary<int, double> powerSets) =>
            _powerSets = powerSets;

        public double this[int coefficient]
        {
            get => _powerSets[coefficient];
            set => _powerSets[coefficient] = value;
        }
    }
}
