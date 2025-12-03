using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refahi.Modules.Hotels.Domain.Abstraction.Repositories;
using Refahi.Modules.Hotels.Infrastructure.Config;
using Refahi.Modules.Hotels.Infrastructure.Persistence;
using Refahi.Modules.Hotels.Infrastructure.Persistence.Repositories;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Config;
using Refahi.Api.Infrastructure.Extensions;

namespace Refahi.Modules.Hotels.Infrastructure;

public static class DI
{
    public static IServiceCollection AddHotelsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HotelsOptions>(configuration.GetSection("Refahi:Hotels"));
        services.Configure<SnappTripOptions>(configuration.GetSection("Refahi:Hotels:Provider:SnappTrip"));

        var hotelsConfig = configuration.GetSection("Refahi:Hotels");
        var connectionString = hotelsConfig.GetValue<string>("ConnectionString");

        // DbContext
        services.AddDbContext<HotelsDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Hotels");
            options.UseNpgsql(connectionString);
        });

        // Repositories
        services.AddScoped<IBookingRepository, BookingRepository>();

        services.UseSnappTripProvider(configuration);

        return services;
    }

    public static void UseHotelInfrastructure(this IServiceProvider provider, bool isDev)
    {
        if (isDev)
        {
            provider.ApplyPendingMigrations<HotelsDbContext>();
        }
    }
}
