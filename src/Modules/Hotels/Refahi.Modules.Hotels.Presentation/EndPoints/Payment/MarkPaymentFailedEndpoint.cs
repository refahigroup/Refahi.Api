using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Refahi.Contract.Presentation;
using Refahi.Modules.Hotels.Application.Contract.Services.Payment.MarkPaymentFailed;

namespace Refahi.Modules.Hotels.Presentation.EndPoints.Payment;

public sealed class MarkPaymentFailedEndpoint : IEndpoint
{
    public void Map(object app)
    {
        if (app is not IEndpointRouteBuilder routes)
            return;

        routes.MapPost("bookings/{bookingId:guid}/payment-failed", async (
            Guid bookingId,
            ISender sender) =>
        {
            await sender.Send(new MarkPaymentFailedCommand(bookingId));

            return Results.Ok();
        })
        .WithName("Hotels.Bookings.PaymentFailed")
        .WithTags("Bookings");
    }
}
