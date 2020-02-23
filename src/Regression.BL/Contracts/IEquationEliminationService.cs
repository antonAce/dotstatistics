using Regression.BL.DTO;

namespace Regression.BL.Contracts
{
    public interface IEquationEliminationService
    {
        Matrix Eliminate(Matrix core, Matrix constants);
    }
}