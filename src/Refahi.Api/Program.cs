using Refahi.Api.Services.Chaching;
using Refahi.Contract.Interfaces;
using StackExchange.Redis;
using Refahi.Modules.Hotels.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});

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

app.UseHotelModule("/api/hotels");

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
