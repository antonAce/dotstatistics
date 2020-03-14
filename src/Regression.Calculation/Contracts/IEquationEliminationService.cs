using Regression.Calculation.DTO;

namespace Regression.Calculation.Contracts
{
    public interface IEquationEliminationService
    {
        Matrix Eliminate(Matrix core, Matrix constants);
    }
}