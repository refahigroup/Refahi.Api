using MediatR;
using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;
using System;
using System.Collections.Generic;

namespace Refahi.Modules.Hotels.Application.Contract.Services.ProvisionalBooking;

public sealed record CreateProvisionalBookingCommand(
    long HotelId,
    long RoomId,
    DateOnly CheckIn,
    DateOnly CheckOut,
    int RoomsCount,
    IEnumerable<GuestDto> Guests,
    string BoardType
) : IRequest<ProvisionalBookingResponse>;
