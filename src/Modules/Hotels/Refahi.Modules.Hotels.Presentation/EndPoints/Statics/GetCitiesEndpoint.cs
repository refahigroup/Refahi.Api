using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Refahi.Contract.Presentation;
using Refahi.Modules.Hotels.Application.Contract.Services.Statics.Cities;

namespace Refahi.Modules.Hotels.Presentation.EndPoints.ProvisionalBooking;

public sealed class GetCitiesEndpoint : IEndpoint
{
    public void Map(object app)
    {
        if (app is not IEndpointRouteBuilder routes)
            return;

        routes.MapGet("statics/cities", async ([FromQuery] string? name, ISender sender) =>
        {
            var request = new GetCitiesRequest(name);

            var result = await sender.Send(request);

            return Results.Ok(result);

        })
        .Produces<IEnumerable<GetCitiesResponse>>()
        .WithName("Hotels.Statics.Cities")
        .WithTags("Hotels");
    }
}