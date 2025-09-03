using demos.blazer.webapp;
using demos.blazer.webapp.CacheManagement;
using demos.blazer.webapp.Global.Configuration;
using demos.blazer.webappPizzaShop.Client.Services;
using demos.blazer.webappPizzaShop.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Server-side
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();
builder.Services.AddSqlite<PizzaStoreContext>("Data Source=pizza.db");

// Client-side
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IPizzaStoreAPI, PizzaStoreAPI>();
builder.Services.AddSingleton<IPizzaStoreRepository, PizzaStoreRepository>();
builder.Services.AddSingleton<IApiConfiguration, ApiConfiguration>();
builder.Services.AddSingleton<IGlobalCacheCoordinator, GlobalCacheCoordinator>();
builder.Services.AddScoped<SalesState>();
builder.Services.AddScoped<OrderState>();

builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<CacheSettings>(
    builder.Configuration.GetSection("CacheSettings"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}

var cacheCoordinator = app.Services.GetRequiredService<IGlobalCacheCoordinator>();

// Ensure cache initializes only after the server has started listening
app.Lifetime.ApplicationStarted.Register(() =>
{
    _ = cacheCoordinator.InitializeAsync();
});

// Fallback: kick off initialization on first request if it hasn't happened yet
app.Use(async (context, next) =>
{
    if (!cacheCoordinator.IsInitialized)
    {
        _ = cacheCoordinator.InitializeAsync();
    }
    await next();
});

app.Lifetime.ApplicationStopping.Register(async () =>
{
    await cacheCoordinator.StopHeartbeatAsync();
});

app.MapControllers();
app.Run();