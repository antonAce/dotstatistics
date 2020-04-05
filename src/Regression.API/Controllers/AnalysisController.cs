using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Regression.API.Filters;
using Regression.API.Models;
using Regression.Calculation.Contracts;
using Regression.Calculation.DTO;

using Regression.BL.DTO;
using Regression.BL.Interfaces;

namespace Regression.API.Controllers
{
    [ApiController]
    [ProcessingExceptionFilter]
    [Route("api/[controller]")]
    public class AnalysisController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IDatasetService _datasetService;
        private readonly IRegressionService _regressionService;
        private readonly IRegressionEstimationService _regressionEstimationService;

        public AnalysisController(ILogger<AnalysisController> logger,
                                  IDatasetService datasetService,
                                  IRegressionService regressionService,
                                  IRegressionEstimationService regressionEstimationService)
        {
            _logger = logger;
            _datasetService = datasetService;
            _regressionService = regressionService;
            _regressionEstimationService = regressionEstimationService;
        }
        
        [HttpGet("{id}/equation")]
        public async Task<IActionResult> CalculateEquation(Guid id, [FromQuery] int? digits)
        {
            _logger.LogInformation($"[{DateTime.Now}] Calculate equation for dataset #{id}");
            var dataset = await _datasetService.GetDatasetById(id);
            
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
        
        [HttpGet("{id}/estimations")]
        public async Task<IActionResult> CalculateEquationEstimations(Guid id, [FromQuery] int? digits)
        {
            _logger.LogInformation($"[{DateTime.Now}] Calculate estimations for equation of dataset #{id}");
            var dataset = await _datasetService.GetDatasetById(id);

            var estimations = _regressionEstimationService.CalculateEstimates(
                dataset.Records.Select(row => new Record
                {
                    Inputs = row.Inputs,
                    Output = row.Output
                }).ToArray());
            
            return Ok(new AccuracyEstimationsModel
            {
                DiscreteOutput = (digits.HasValue) ? 
                    dataset.Records.Select(record => Math.Round(record.Output, digits.Value)).ToArray() :
                    dataset.Records.Select(record => record.Output).ToArray(),
                ApproximationOutputs = (digits.HasValue) ?
                    estimations.ApproximationOutputs.Select(output => Math.Round(output, digits.Value)).ToArray() :
                    estimations.ApproximationOutputs,
                SquareSumMax = (digits.HasValue) ? 
                    Math.Round(estimations.SquareSumMax, digits.Value) :
                    estimations.SquareSumMax,
                Correlation = (digits.HasValue) ? 
                    Math.Round(estimations.Correlation, digits.Value) :
                    estimations.Correlation
            });
        }
    }
}