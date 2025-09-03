namespace demos.blazer.webapp.Global.CacheManagement
{
    public class CacheRefreshedEventArgs : EventArgs
    {
        public string CacheKey { get; }
        public DateTime RefreshedAt { get; }

        public CacheRefreshedEventArgs(string cacheKey, DateTime refreshedAt)
        {
            CacheKey = cacheKey;
            RefreshedAt = refreshedAt;
        }
    }
}
