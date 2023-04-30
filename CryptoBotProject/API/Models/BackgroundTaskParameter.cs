namespace API.Models
{
    public class BackgroundTaskParameter
    {
        public int TaskId { get; set; }
        public string Time { get; set; }
        public TimeSpan Interval { get; set; }
        public string KlineStream { get; set; }
    }
}
