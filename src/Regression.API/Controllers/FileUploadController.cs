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

        public FileUploadController(ILogger<FileUploadController> logger,
                                    IDatasetParser datasetParser,
                                    IFileParser fileParser)
        {
            _logger = logger;
            _fileParser = fileParser;
            _datasetParser = datasetParser;
        }
        
        [HttpPost]
        public async Task<IActionResult> StoreUploadedFile([FromForm] string name, [FromForm] IFormFile file)
        {
            _logger.LogInformation($"[{DateTime.Now}] POST: Upload file");

            try
            {
                var result = await _fileParser.GetStringAsync(file);
                var dataset = _datasetParser.FromString(name, result);
                return Ok(dataset);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}