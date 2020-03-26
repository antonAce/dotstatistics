using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Regression.API.Filters;
using Regression.BL.DTO;
using Regression.BL.Interfaces;

namespace Regression.API.Controllers
{
    [ApiController]
    [DatasetExceptionFilter]
    [Route("api/[controller]")]
    public class DatasetController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IDatasetService _datasetService;

        public DatasetController(ILogger<DatasetController> logger,
                                 IDatasetService datasetService)
        {
            _logger = logger;
            _datasetService = datasetService;
        }
        
        [HttpGet]
        public async Task<IActionResult> ListDatasets([FromQuery] int? limit, [FromQuery] int? offset, [FromQuery] bool? headersOnly)
        {
            _logger.LogInformation($"[{DateTime.Now}] List Datasets with limit {limit} and offset {offset}");
            var inputLimit = limit ?? await _datasetService.GetCountOfDatasets();
            var inputOffset = offset ?? 0;

            if (headersOnly ?? false)
            {
                var datasetsHeaders = await _datasetService.ListDatasetsHeadOnly(inputLimit, inputOffset);
                return Ok(datasetsHeaders);
            }
            else
            {
                var datasets = await _datasetService.ListDatasets(inputLimit, inputOffset);
                return Ok(datasets);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDatasetById(Guid id, [FromQuery] bool? outputsOnly)
        {
            _logger.LogInformation($"[{DateTime.Now}] Get Dataset By Id #{id}");

            if (outputsOnly ?? false)
            {
                var dataset = await _datasetService.GetDatasetOutputsOnly(id);
                return Ok(dataset);
            }
            else
            {
                var dataset = await _datasetService.GetDatasetById(id);
                return Ok(dataset);
            }
        }

        [HttpPost]
        public async Task<IActionResult> StoreDataset(DatasetToSaveDTO dataset)
        {
            _logger.LogInformation($"[{DateTime.Now}] Store dataset");
            
            await _datasetService.StoreDataset(dataset);
            return Ok("Dataset stored successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDataset(Guid id, DatasetToSaveDTO dataset)
        {
            _logger.LogInformation($"[{DateTime.Now}] Update dataset #{id}");
            
            await _datasetService.UpdateDataset(id, dataset);
            return Ok("Dataset updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DropDataset(Guid id)
        {
            _logger.LogInformation($"[{DateTime.Now}] Drop dataset #{id}");
            
            await _datasetService.DropDatasetById(id);
            return Ok("Dataset dropped successfully!");
        }
    }
}