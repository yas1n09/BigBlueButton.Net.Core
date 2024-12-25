using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.RecordingDto
{
    public class DeleteRecordingDto
    {
        public string RecordID { get; set; }
        public bool IsDeleted { get; set; }
        public string Message { get; set; }
    }
}
