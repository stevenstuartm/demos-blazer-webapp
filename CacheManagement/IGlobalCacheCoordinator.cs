namespace demos.blazer.webapp.CacheManagement
{
    public interface IGlobalCacheCoordinator
    {
        Task InitializeAsync();
        void StartHeartbeat();
        Task StopHeartbeatAsync();
        bool IsInitialized { get; }
        event EventHandler<CacheRefreshedEventArgs>? CacheRefreshed;
    }
}
