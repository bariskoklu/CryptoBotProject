using Binance.Common;
using Binance.Spot.Models;
using Binance.Spot;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace CryptoBotProject.Controllers
{
    public class KlineCandlestickData_Example : BaseApiController
    {

        private readonly ILogger<KlineCandlestickData_Example> _logger;

        public KlineCandlestickData_Example(ILogger<KlineCandlestickData_Example> logger)
        {
            _logger = logger;
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
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<KlineCandlestickData_Example>();

            var websocket = new MarketDataWebSocket("btcusdt@kline_1s");

            websocket.OnMessageReceived(
                async (data) =>
                {
                    logger.LogInformation(data);
                    await Task.CompletedTask;
                }, CancellationToken.None);

            await websocket.ConnectAsync(CancellationToken.None);

            // wait for 5s before disconnected
            await Task.Delay(5000);
            logger.LogInformation("Disconnect with WebSocket Server");
            await websocket.DisconnectAsync(CancellationToken.None);
        }
    }
}