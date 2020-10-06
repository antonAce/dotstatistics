namespace DotStatistics.Numeric.Primitives
{
    public interface IMatrix
    {
        int Width { get; }
        int Height { get; }
        double this[int x, int y] { get; set; }
    }
}