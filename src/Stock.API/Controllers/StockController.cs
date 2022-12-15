using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuoteAPI.Domain.Interfaces.Services;
using Stock.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Stock.API.Controllers
{
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IStockService _stockService;

        public StockController(ILogger<StockController> logger, IStockService stockService)
        {
            _logger = logger;
            _stockService = stockService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StockInput model)
        {
            try
            {
                await _stockService.GetStockAsync(model);
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting stock", ex);
                return new StatusCodeResult(500);
            }
        }
    }
}
