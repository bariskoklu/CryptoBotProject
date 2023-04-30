using Binance.Common;
using Binance.Spot.Models;
using Binance.Spot;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using API.Services.BackgroundTasks;
using API.Interfaces;
using API.Models;

namespace CryptoBotProject.Controllers
{
    public class KlineCandlestickData_Example : BaseApiController
    {

        private readonly ILogger<KlineCandlestickData_Example> _logger;
        private readonly IBackgroundTaskFactory _backgroundTaskFactory;

        public KlineCandlestickData_Example(ILogger<KlineCandlestickData_Example> logger, IBackgroundTaskFactory backgroundTaskFactory)
        {
            _logger = logger;
            _backgroundTaskFactory = backgroundTaskFactory;
        }

        [HttpGet]
        public async Task<string> GetServerTime()
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

            return result;
        }

        [HttpGet("Candle-Stick")]
        public async Task<string> GetExampleCandleStick()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<KlineCandlestickData_Example>();

            HttpMessageHandler loggingHandler = new BinanceLoggingHandler(logger: logger);
            HttpClient httpClient = new HttpClient(handler: loggingHandler);

            var market = new Market(httpClient);

            var result = await market.KlineCandlestickData("BNBUSDT", Interval.ONE_MINUTE);

            return result;
        }

        [HttpGet("WebSocket")]
        public async Task GetData()
        {
            var backgroundTaskParameter = new BackgroundTaskParameter
            {
                Interval = TimeSpan.FromSeconds(1),
                TaskId= 1,
                KlineStream= "btcusdt@kline_1s"
            };
            var backgroundTask = _backgroundTaskFactory.Create(backgroundTaskParameter);
            await backgroundTask.StartAsync(CancellationToken.None);
        }
    }
}