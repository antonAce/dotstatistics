using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Regression.BL.DTO;

namespace Regression.BL.Interfaces
{
    public interface IDatasetService
    {
        Task StoreDataset(DatasetToSaveDTO dataset);
        Task UpdateDataset(Guid id, DatasetToSaveDTO dataset);
        Task DropDatasetById(Guid id);
        Task<DatasetToReadDTO> GetDatasetById(Guid id);
        Task<DatasetOutputsDTO> GetDatasetOutputsOnly(Guid id);
        Task<IEnumerable<DatasetToReadDTO>> ListDatasets(int limit, int offset);
        Task<IEnumerable<DatasetHeader>> ListDatasetsHeadOnly(int limit, int offset);
        Task<int> GetCountOfDatasets();
    }
}