using demos.blazer.webapp.PizzaShop.Client.Public.Models;

namespace demos.blazer.webapp.PizzaShop.Client.Public.Services
{
    public static class PizzaSpecialsCache
    {
        private static List<PizzaSpecial>? _specials;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        private static bool _isInitialized = false;
        private static DateTime _lastRefreshed = DateTime.MinValue;

        public static DateTime LastRefreshed => _lastRefreshed;
        public static bool IsInitialized => _isInitialized;

        public static async Task<List<PizzaSpecial>> GetSpecialsAsync(IPizzaStoreAPI api)
        {
            if (_isInitialized && _specials != null)
                return _specials;

            await _semaphore.WaitAsync();
            try
            {
                if (_isInitialized && _specials != null)
                    return _specials;

                await LoadSpecialsAsync(api);
                return _specials ?? new List<PizzaSpecial>();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public static async Task ForceRefreshAsync(IPizzaStoreAPI api)
        {
            await _semaphore.WaitAsync();
            try
            {
                await LoadSpecialsAsync(api);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private static async Task LoadSpecialsAsync(IPizzaStoreAPI api)
        {
            var response = await api.GetPizzaSpecials();
            _specials = response?.Select(ToModel).ToList() ?? new List<PizzaSpecial>();
            _isInitialized = true;
            _lastRefreshed = DateTime.Now;
        }

        public static void ClearCache()
        {
            _specials = null;
            _isInitialized = false;
            _lastRefreshed = DateTime.MinValue;
        }

        private static PizzaSpecial ToModel(Shared.DTOs.Outputs.PizzaSpecial special)
        {
            return new PizzaSpecial()
            {
                BasePrice = special.BasePrice,
                Description = special.Description,
                Id = special.Id,
                ImageUrl = special.ImageUrl,
                Name = special.Name
            };
        }
    }
}
