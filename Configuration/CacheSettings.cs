namespace demos.blazer.webapp.Configuration
{
    public class CacheSettings
    {
        public int HeartbeatIntervalMinutes { get; set; } = 30;
        public bool EnableHeartbeat { get; set; } = true;
        public int CacheDurationMinutes { get; set; } = 60;
    }
}
