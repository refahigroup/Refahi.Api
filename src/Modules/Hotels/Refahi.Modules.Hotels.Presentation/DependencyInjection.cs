using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refahi.Api.Infrastructure.Extensions;
using Refahi.Modules.Hotels.Application;
using Refahi.Modules.Hotels.Infrastructure;
using Refahi.Modules.Hotels.Infrastructure.Persistence;
using System.Runtime.InteropServices.JavaScript;

namespace Refahi.Modules.Hotels.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddHotelsModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHotelsInfrastructure(configuration);
        services.AddHotelsApplication(configuration);

        return services;
    }

    public static void UseHotelModule(this IHost app, bool isDev)
    {
        app.Services.UseHotelInfrastructure(isDev);
    }
}
