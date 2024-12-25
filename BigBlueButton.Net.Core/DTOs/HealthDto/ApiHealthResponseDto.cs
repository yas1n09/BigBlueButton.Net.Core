using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.HealthDto
{
    public class ApiHealthResponseDto
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
