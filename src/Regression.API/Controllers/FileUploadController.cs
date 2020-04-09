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

namespace Regression.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IFileParser _fileParser;

        public FileUploadController(ILogger<FileUploadController> logger,
                                    IFileParser fileParser)
        {
            _logger = logger;
            _fileParser = fileParser;
        }
        
        [HttpPost]
        public async Task<IActionResult> StoreUploadedFile([FromForm] IFormFile file)
        {
            _logger.LogInformation($"[{DateTime.Now}] POST: Upload file");

            try
            {
                var bytes = await _fileParser.GetBytesAsync(file);
                return Ok(bytes);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}