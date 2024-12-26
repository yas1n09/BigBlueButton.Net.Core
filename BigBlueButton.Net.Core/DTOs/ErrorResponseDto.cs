 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs
{
    public class ErrorResponseDto
    {
        public ErrorResponseDto() { }

        public string Message { get; set; }
        public string Details { get; set; }
    }
}
