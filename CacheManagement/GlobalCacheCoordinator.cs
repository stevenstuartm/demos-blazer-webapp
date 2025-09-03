using demos.blazer.webappPizzaShop.Client.Services;

namespace demos.blazer.webapp.CacheManagement
{
    public class GlobalCacheCoordinator : IGlobalCacheCoordinator, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly SemaphoreSlim _initSemaphore = new(1, 1);
        private volatile bool _isInitialized = false;
        private volatile bool _isDisposed = false;

        private System.Timers.Timer? _heartbeatTimer;
        private readonly TimeSpan _heartbeatInterval = TimeSpan.FromMinutes(30);

        public bool IsInitialized => _isInitialized;
        public event EventHandler<CacheRefreshedEventArgs>? CacheRefreshed;

        public GlobalCacheCoordinator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InitializeAsync()
        {
            if (_isInitialized || _isDisposed)
                return;

            await _initSemaphore.WaitAsync();
            try
            {
                if (_isInitialized || _isDisposed)
                    return;

                await InitializeCachesAsync();
                _isInitialized = true;

                // Auto-start heartbeat after successful initialization
                StartHeartbeat();
            }
            finally
            {
                _initSemaphore.Release();
            }
        }

        public void StartHeartbeat()
        {
            if (!_isInitialized || _isDisposed || _heartbeatTimer != null)
                return;

            _heartbeatTimer = new System.Timers.Timer(_heartbeatInterval.TotalMilliseconds);
            _heartbeatTimer.Elapsed += async (sender, e) => await RefreshCachesAsync();
            _heartbeatTimer.AutoReset = true;
            _heartbeatTimer.Enabled = true;
        }

        public async Task StopHeartbeatAsync()
        {
            if (_heartbeatTimer != null)
            {
                _heartbeatTimer.Stop();
                _heartbeatTimer.Dispose();
                _heartbeatTimer = null;
            }
            await Task.CompletedTask;
        }

        private async Task InitializeCachesAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            await InitializePizzaSpecialsAsync(scope.ServiceProvider);
        }

        private async Task RefreshCachesAsync()
        {
            if (_isDisposed)
                return;

            try
            {
                using var scope = _serviceProvider.CreateScope();
                await RefreshPizzaSpecialsAsync(scope.ServiceProvider);
                CacheRefreshed?.Invoke(this, new CacheRefreshedEventArgs("PizzaSpecials", DateTime.Now));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cache refresh failed: {ex.Message}");
            }
        }

        private async Task InitializePizzaSpecialsAsync(IServiceProvider serviceProvider)
        {
            var repository = serviceProvider.GetRequiredService<IPizzaStoreRepository>();
            await repository.Init();
        }

        private async Task RefreshPizzaSpecialsAsync(IServiceProvider serviceProvider)
        {
            PizzaSpecialsCache.ClearCache();
            await InitializePizzaSpecialsAsync(serviceProvider);
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;
            _heartbeatTimer?.Stop();
            _heartbeatTimer?.Dispose();
            _initSemaphore?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}