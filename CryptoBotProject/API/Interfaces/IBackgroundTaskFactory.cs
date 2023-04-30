using API.Models;
using API.Services.BackgroundTasks;

namespace API.Interfaces
{
    public interface IBackgroundTaskFactory
    {
        public BuySellBot Create(BackgroundTaskParameter backgroundTaskParameter);
    }
}
