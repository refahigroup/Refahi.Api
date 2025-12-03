namespace Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;

public sealed class ProviderBookingStatusDto
{
    public string Status { get; set; } = default!;
    public string? VoucherUrl { get; set; }
    public string? VoucherNumber { get; set; }
    public string? ProviderMessage { get; set; }
}

