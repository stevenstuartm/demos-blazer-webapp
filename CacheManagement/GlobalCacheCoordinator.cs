using demos.blazer.webapp.CacheManagement;

namespace demos.blazer.webapp.Features.PizzaShop.Client.Services
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
                await StartHeartbeatAsync();
            }
            finally
            {
                _initSemaphore.Release();
            }
        }

        public async Task StartHeartbeatAsync()
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