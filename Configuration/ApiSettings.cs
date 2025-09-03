namespace demos.blazer.webapp.Configuration
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; }
        public int TimeoutSeconds { get; set; } = 30;
        public int RetryCount { get; set; } = 3;
    }
}
