using demos.blazer.webapp;
using demos.blazer.webapp.Features.PizzaShop.Client.Services;
using demos.blazer.webapp.Features.PizzaShop.Server.Repositories;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

//server-side
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();
builder.Services.AddSqlite<PizzaStoreContext>("Data Source=pizza.db");

//client-side
builder.Services.AddScoped<SalesState>();
builder.Services.AddScoped<OrderState>();
builder.Services.AddScoped<IPizzaStoreRepository, PizzaStoreRepository>();
builder.Services.AddScoped<IPizzaStoreAPI, PizzaStoreAPI>();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.MapControllers();
app.Run();
