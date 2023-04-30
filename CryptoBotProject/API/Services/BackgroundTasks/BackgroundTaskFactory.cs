using API.Interfaces;
using API.Models;
using CryptoBotProject.Controllers;

namespace API.Services.BackgroundTasks
{
    public class BackgroundTaskFactory : IBackgroundTaskFactory
    {
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BackgroundTaskFactory(ILogger<BackgroundTaskFactory> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public BuySellBot Create(BackgroundTaskParameter backgroundTaskParameter)
        {
            return new BuySellBot(_logger, _serviceScopeFactory, backgroundTaskParameter);
        }
    }
}
