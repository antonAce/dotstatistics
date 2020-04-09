using Regression.BL.DTO;

namespace Regression.BL.Interfaces
{
    public interface IDatasetParser
    {
        DatasetToSaveDTO FromString(string name, string source);
    }
}