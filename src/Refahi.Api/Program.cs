using Refahi.Api.Services.Chaching;
using Refahi.Contract.Interfaces;
using StackExchange.Redis;
using Refahi.Modules.Hotels.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddMemoryCache();
    builder.Services.AddSingleton<ICacheService, InMemoryCacheService>();
}
else
{
    builder.Services.AddSingleton<IConnectionMultiplexer>(
        _ => ConnectionMultiplexer.Connect(builder.Configuration["Redis:Connection"])
    );

    builder.Services.AddSingleton<ICacheService, RedisCacheService>();
}

builder.Services.AddHotelsModules(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHotelModule(app.Environment.IsDevelopment());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
