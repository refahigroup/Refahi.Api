using System;
using MediatR;

namespace Refahi.Modules.Hotels.Application.Contract.Services.Payment.MarkSucceeded;

public sealed record MarkPaymentSucceededCommand(Guid BookingId) : IRequest<Unit>;
