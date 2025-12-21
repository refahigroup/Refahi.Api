using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Refahi.Contract.Presentation;
using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;
using Refahi.Modules.Hotels.Application.Contract.Providers.Queries;

namespace Refahi.Modules.Hotels.Presentation.EndPoints.Hotels;

public sealed class GetHotelDetailsEndpoint : IEndpoint
{
    public void Map(object app)
    {
        if (app is not IEndpointRouteBuilder routes)
            return;

        routes.MapGet("{hotelId:long}", async (
            long hotelId,
            [FromQuery] DateOnly? checkIn,
            [FromQuery] DateOnly? checkOut,
            ISender sender) =>
        {
            var query = new GetHotelDetailsQuery(hotelId, checkIn, checkOut);

            var details = await sender.Send(query);

            return Results.Ok(details);

        })
        .Produces<IEnumerable<HotelDetailsDto>>()
        .WithName("Hotels.Details")
        .WithTags("Hotels");
    }
}
