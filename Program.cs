using demos.blazer.webapp;
using demos.blazer.webapp.CacheManagement;
using demos.blazer.webapp.Configuration;
using demos.blazer.webapp.Features.PizzaShop.Client.Services;
using demos.blazer.webapp.Features.PizzaShop.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Server-side
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();
builder.Services.AddSqlite<PizzaStoreContext>("Data Source=pizza.db");

// Client-side
builder.Services.AddScoped<SalesState>();
builder.Services.AddScoped<OrderState>();
builder.Services.AddScoped<IPizzaStoreAPI, PizzaStoreAPI>();
builder.Services.AddScoped<IPizzaStoreRepository, PizzaStoreRepository>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IApiConfiguration, ApiConfiguration>();
builder.Services.AddSingleton<IGlobalCacheCoordinator, GlobalCacheCoordinator>();

//todo: review the GlobalCacheCoordinator. is the refresh hook in the api really needed?
//and is the endpoint startup timing just working our of luck or this sustainable?

builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<CacheSettings>(
    builder.Configuration.GetSection("CacheSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline
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

// Initialize database
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
 _ = cacheCoordinator.InitializeAsync();

// Graceful shutdown
app.Lifetime.ApplicationStopping.Register(async () =>
{
    await cacheCoordinator.StopHeartbeatAsync();
});

app.MapControllers();
app.Run();