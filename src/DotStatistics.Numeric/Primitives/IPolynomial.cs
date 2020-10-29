namespace DotStatistics.Numeric.Primitives
{
    public interface IPolynomial
    {
        double this[int coefficient] { get; set; }
        int Min { get; }
        int Max { get; }
    }
}