using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Regression.API.Interfaces;
using Regression.BL.Interfaces;

namespace Regression.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IFileParser _fileParser;
        private readonly IDatasetParser _datasetParser;
        private readonly IDatasetService _datasetService;

        public FileUploadController(ILogger<FileUploadController> logger,
                                    IDatasetService datasetService,
                                    IDatasetParser datasetParser,
                                    IFileParser fileParser)
        {
            _logger = logger;
            _fileParser = fileParser;
            _datasetParser = datasetParser;
            _datasetService = datasetService;
        }
        
        [HttpPost]
        public async Task<IActionResult> StoreDatasetFromFile([FromForm] string name, [FromForm] IFormFile file)
        {
            _logger.LogInformation($"[{DateTime.Now}] POST: Upload file");

            try
            {
                var result = await _fileParser.GetStringAsync(file);
                await _datasetService.StoreDataset(_datasetParser.FromString(name, result));
                return Ok("Dataset imported successfully!");
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}