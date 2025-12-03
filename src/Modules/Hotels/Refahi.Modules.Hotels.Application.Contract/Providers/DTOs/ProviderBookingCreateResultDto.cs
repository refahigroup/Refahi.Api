namespace Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;

public sealed class ProviderBookingCreateResultDto
{
    public string ProviderBookingCode { get; set; } = default!;
    public long ProviderPrice { get; set; }
    public string Currency { get; set; } = "IRT";
    public DateTime? LockedUntil { get; set; }
}

