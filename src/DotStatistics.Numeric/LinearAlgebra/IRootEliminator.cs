using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.LinearAlgebra
{
    public interface IRootEliminator
    {
        IMatrix Eliminate(IMatrix core, IMatrix outputs);
    }
}