using Microsoft.Extensions.Options;

namespace demos.blazer.webapp.Configuration
{
    public class ApiConfiguration : IApiConfiguration
    {
        private readonly ApiSettings _settings;

        public ApiConfiguration(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GetApiBaseUrl()
        {
            return _settings.BaseUrl;
        }

        public int GetTimeoutSeconds()
        {
            return _settings.TimeoutSeconds;
        }

        public int GetRetryCount()
        {
            return _settings.RetryCount;
        }
    }
}
