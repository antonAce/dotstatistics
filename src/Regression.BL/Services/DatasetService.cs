using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Regression.BL.DTO;
using Regression.BL.Interfaces;

using Regression.Domain.Interfaces;
using Regression.Domain.Models;

namespace Regression.BL.Services
{
    public class DatasetService : IDatasetService
    {
        private readonly IDatasetRepository _repository;
        
        public DatasetService(IDatasetRepository repository)
        {
            _repository = repository;
        }
        
        public async Task StoreDataset(DatasetToSaveDTO dataset)
        {
            await _repository.StoreDatasetAsync(new Dataset
            {
                Id = Guid.NewGuid(),
                Name = dataset.Name,
                Records = dataset.Records.Select(record => new Record
                {
                    Inputs = record.Inputs,
                    Output = record.Output
                }).ToArray()
            });
        }

        public async Task UpdateDataset(Guid id, DatasetToSaveDTO dataset)
        {
            await _repository.UpdateDatasetAsync(new Dataset
            {
                Id = id,
                Name = dataset.Name,
                Records = dataset.Records.Select(record => new Record
                {
                    Inputs = record.Inputs,
                    Output = record.Output
                }).ToArray()
            });
        }

        public async Task DropDatasetById(Guid id)
        {
            await _repository.DropDatasetAsync(new Dataset { Id = id });
        }

        public async Task<DatasetToReadDTO> GetDatasetById(Guid id)
        {
            var dataset = await _repository.GetDatasetByIdAsync(id);
            
            return new DatasetToReadDTO
            {
                Id = dataset.Id,
                Name = dataset.Name,
                Records = dataset.Records.Select(record => new RecordDTO
                {
                    Inputs = record.Inputs,
                    Output = record.Output
                }).ToArray() 
            };
        }

        public async Task<DatasetOutputsDTO> GetDatasetOutputsOnly(Guid id)
        {
            var dataset = await _repository.GetDatasetByIdAsync(id);
            
            return new DatasetOutputsDTO
            {
                Outputs = dataset.Records
                    .Select(record => record.Output)
                    .ToArray()
            };
        }

        public async Task<IEnumerable<DatasetToReadDTO>> ListDatasets(int limit, int offset)
        {
            var datasets = await _repository.FetchDatasetsAsync(limit, offset);
            
            return datasets.Select(dataset => new DatasetToReadDTO
            {
                Id = dataset.Id,
                Name = dataset.Name,
                Records = dataset.Records.Select(record => new RecordDTO
                {
                    Inputs = record.Inputs,
                    Output = record.Output
                }).ToArray() 
            });
        }

        public async Task<int> GetCountOfDatasets()
        {
            return await _repository.CountDatasets();
        }
    }
}