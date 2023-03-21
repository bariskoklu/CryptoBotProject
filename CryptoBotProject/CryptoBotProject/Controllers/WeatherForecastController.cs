using Binance.Common;
using Binance.Spot.Models;
using Binance.Spot;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBotProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KlineCandlestickData_Example : ControllerBase
    {

        private readonly ILogger<KlineCandlestickData_Example> _logger;

        public KlineCandlestickData_Example(ILogger<KlineCandlestickData_Example> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task Get()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<KlineCandlestickData_Example>();

            HttpMessageHandler loggingHandler = new BinanceLoggingHandler(logger: logger);
            HttpClient httpClient = new HttpClient(handler: loggingHandler);

            var market = new Market();

            var result = await market.CheckServerTime();
        }
    }
}