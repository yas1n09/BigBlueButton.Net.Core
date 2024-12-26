using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Responses
{
    public class ApiHealthResponse : BaseResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Details { get; set; } = string.Empty; // Opsiyonel, hata detayları için
        public string? Uptime { get; set; }
        public int? MeetingCount { get; set; }
        public int? ActiveUsers { get; set; }
    }
}
