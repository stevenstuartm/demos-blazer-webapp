namespace demos.blazer.webapp.CacheManagement
{
    public interface IGlobalCacheCoordinator
    {
        Task InitializeAsync();
        Task StartHeartbeatAsync();
        Task StopHeartbeatAsync();
        bool IsInitialized { get; }
        event EventHandler<CacheRefreshedEventArgs>? CacheRefreshed;
    }
}
