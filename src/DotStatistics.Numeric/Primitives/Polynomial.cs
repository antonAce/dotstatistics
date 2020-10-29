using System.Collections.Generic;
using System.Linq;

namespace DotStatistics.Numeric.Primitives
{
    public class Polynomial : IPolynomial
    {
        private readonly IDictionary<int, double> _powerSets;

        public Polynomial(IDictionary<int, double> powerSets) =>
            _powerSets = powerSets;

        public double this[int coefficient]
        {
            get => _powerSets.TryGetValue(coefficient, out var val) ? val : 0.0;
            set => _powerSets[coefficient] = value;
        }

        public int Min => _powerSets.Keys.Min();
        public int Max => _powerSets.Keys.Max();
    }
}
