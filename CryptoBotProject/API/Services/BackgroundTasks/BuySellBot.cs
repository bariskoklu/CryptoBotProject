using API.Data;
using API.Models;
using Binance.Spot;
using Binance.Spot.Models;
using CryptoBotProject.Controllers;
using System.Reflection.Metadata;

namespace API.Services.BackgroundTasks
{
    public class BuySellBot : BackgroundService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly BackgroundTaskParameter _backgroundTaskParameter;

        private bool isConnected = false;

        public BuySellBot(ILogger logger, IServiceScopeFactory serviceScopeFactory, BackgroundTaskParameter backgroundTaskParameter)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _backgroundTaskParameter = backgroundTaskParameter;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Starting background task");

            while (!stoppingToken.IsCancellationRequested)
            {
                // Do some work here...
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                if (!isConnected)
                {
                    await StartWebSocket();
                }

            }
            _logger.LogInformation($"Background task with  is stopping.");
        }

        public async Task StartWebSocket()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<BuySellBot>();

            var websocket = new MarketDataWebSocket(_backgroundTaskParameter.KlineStream);

            websocket.OnMessageReceived(
                async (data) =>
                {
                    isConnected = true;
                    logger.LogInformation(data);
                    await Task.CompletedTask;
                }, CancellationToken.None);

            await websocket.ConnectAsync(CancellationToken.None);
        }
    }
}
