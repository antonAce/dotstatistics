namespace DotStatistics.Numeric.Primitives
{
    public interface IPolynomial
    {
        double this[int coefficient] { get; set; }
        double Terms { get; }
    }
}