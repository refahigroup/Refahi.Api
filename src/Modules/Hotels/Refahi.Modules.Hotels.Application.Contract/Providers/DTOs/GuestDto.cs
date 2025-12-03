namespace Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;

public sealed class GuestDto
    {
        public string FullName { get; set; } = default!;
        public int Age { get; set; }
        public string Type { get; set; } = default!;
    }

