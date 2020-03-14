using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Regression.API.Models;
using Regression.Calculation.Contracts;
using Regression.Calculation.DTO;

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
        public IActionResult ProcessData([FromBody] DatasetProcessingModel dataset, [FromQuery] int? digits)
        {
            _logger.LogInformation($"[{DateTime.Now}] Params: statistic size: {dataset.Records.Count()}");

            try
            {
                ICollection<double> poly = _regressionService.CalculateRegression(
                    dataset.Records.Select(row => new Record
                    {
                        Inputs = row.Inputs,
                        Output = row.Output
                    }).ToArray());

                if (digits.HasValue)
                    poly = poly.Select(number => Math.Round(number, digits.Value)).ToArray();
                
                return Ok(new PolynomialModel { Koeficients = poly });
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