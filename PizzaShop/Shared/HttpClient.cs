using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace demos.blazer.webappPizzaShop.Shared
{
    public class HttpClient
    {
        private readonly System.Net.Http.HttpClient _client;
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public System.Net.Http.HttpClient Client => _client;

        public HttpClient(string baseURI)
        {
            _client = new System.Net.Http.HttpClient(new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(15)
            });
            _client.BaseAddress = new Uri(baseURI);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.Timeout = TimeSpan.FromSeconds(10);

            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public virtual async Task<HttpResponseMessage> GetWithoutValidation(string path)
        {
            return await _client.GetAsync(path);
        }

        public virtual async Task<T> Get<T>(string path)
        {
            var response = await _client.GetAsync(path);

            await ThrowExceptionIfErrorUnlessNotFoundAsync(response);

            if (response.IsSuccessStatusCode)
            {
                return await ParseJson<T>(response);
            }

#pragma warning disable CS8603 // Possible null reference return.
            return default;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public virtual async Task<HttpResponseMessage> DeleteWithoutValidation(string path)
        {
            return await _client.DeleteAsync(path);
        }

        public virtual async Task Delete(string path)
        {
            var response = await _client.DeleteAsync(path);

            await ThrowExceptionIfNotOKAsync(response);
        }

        public virtual async Task<HttpResponseMessage> PostWithoutValidation<R>(string path, R request)
        {
            return await _client.PostAsJsonAsync(path, request);
        }

        public virtual async Task Post<R>(string path, R request)
        {
            var response = await _client.PostAsJsonAsync(path, request);

            await ThrowExceptionIfNotOKAsync(response);
        }

        public virtual async Task<T> PostWithoutValidation<T, R>(string path, R request)
        {
            var response = await _client.PostAsJsonAsync(path, request);

            return await ParseJson<T>(response);
        }

        public virtual async Task<HttpResponseMessage> SendWithoutValidation(HttpRequestMessage message)
        {
            return await _client.SendAsync(message);
        }

        public virtual async Task<T> Post<T, R>(string path, R request)
        {
            var response = await _client.PostAsJsonAsync(path, request);

            await ThrowExceptionIfNotOKAsync(response);

            return await ParseJson<T>(response);
        }

        public virtual async Task<T> ParseJson<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(json))
            {
#pragma warning disable CS8603 // Possible null reference return.
                return JsonSerializer.Deserialize<T>(json!, jsonSerializerOptions);
#pragma warning restore CS8603 // Possible null reference return.
            }

#pragma warning disable CS8603 // Possible null reference return.
            return default;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public static readonly IEnumerable<System.Net.HttpStatusCode>? DefaultGetIgnoredStatuses
            = new System.Net.HttpStatusCode[] { System.Net.HttpStatusCode.NotFound };

        public const string InternalExceptionMessage = "Internal Exception";

        public async Task ThrowExceptionIfErrorAsync(
            HttpResponseMessage response,
            IEnumerable<System.Net.HttpStatusCode>? ignoredStatuses)
        {
            if (!response.IsSuccessStatusCode && ((!ignoredStatuses?.Contains(response.StatusCode)) ?? true))
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                throw new Exception(errorContent ?? InternalExceptionMessage);
            }
        }

        public async Task ThrowExceptionIfNotOKAsync(HttpResponseMessage response)
        {
            await ThrowExceptionIfErrorAsync(response, null);
        }

        public async Task ThrowExceptionIfErrorUnlessNotFoundAsync(HttpResponseMessage response)
        {
            await ThrowExceptionIfErrorAsync(response, DefaultGetIgnoredStatuses);
        }
    }
}
