namespace BigBlueButtonAPI.DTOs
{
    public class ApiHealthResponseDto
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Details { get; set; } = string.Empty; // Opsiyonel, hata detayları için
        public string? Uptime { get;  set; }
        public int? MeetingCount { get;  set; }
        public int? ActiveUsers { get;  set; }
    }
}
