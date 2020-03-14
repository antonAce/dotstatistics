using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Regression.Domain.Models;

namespace Regression.Domain.Interfaces
{
    public interface IDatasetRepository
    {
        Task<int> CountDatasets();
        Task<IEnumerable<Dataset>> FetchDatasetsAsync(int limit, int offset);
        Task<Dataset> GetDatasetByIdAsync(Guid id);
        Task StoreDatasetAsync(Dataset dataset);
        Task UpdateDatasetAsync(Dataset dataset);
        Task DropDatasetAsync(Dataset dataset);
    }
}