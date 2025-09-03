namespace demos.blazer.webapp.Global.CacheManagement
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
