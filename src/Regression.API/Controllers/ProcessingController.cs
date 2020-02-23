using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Regression.API.Models;
using Regression.BL.Contracts;
using Regression.BL.DTO;

namespace Regression.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessingController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRegressionService _regressionService;
        
        public ProcessingController(ILogger<ProcessingController> logger,
                                    IRegressionService regressionService)
        {
            _logger = logger;
            _regressionService = regressionService;
        }
        
        [HttpPost]
        public IActionResult ProcessData([FromBody] Statistic statistic)
        {
            _logger.LogInformation($"[{DateTime.Now}] Params: statistic size: {statistic.Rows.Length}");

            try
            {
                double[] poly = _regressionService.CalculateRegression(
                    statistic.Rows.Select(row => new Record
                    {
                        Args = row.Args,
                        Result = row.Result
                    }).ToArray());
                
                return Ok(new Polynomial { Constants = poly });
            }
            catch (ArgumentException e)
            {
                _logger.LogError($"[{DateTime.Now}] Wrong arguments! Message: {e.Message}; Stacktrace: {e.StackTrace}");
                return BadRequest($"{e.Message}");
            }
            catch (DivideByZeroException e)
            {
                _logger.LogError($"[{DateTime.Now}] Divide by zero exception! Message: {e.Message}; Stacktrace: {e.StackTrace}");
                return BadRequest();
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(
                    $"[{DateTime.Now}] Invalid operation! Message: {e.Message}; Stacktrace: {e.StackTrace}");
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError($"[{DateTime.Now}] General exception! Message: {e.Message}; Stacktrace: {e.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}