using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Regression.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly ILogger _logger;

        public FileUploadController(ILogger<FileUploadController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> StoreUploadedFile([FromForm] IFormFile file)
        {
            _logger.LogInformation($"[{DateTime.Now}] POST: Upload file");
            return Ok("Dataset stored successfully!");
        }
    }
}