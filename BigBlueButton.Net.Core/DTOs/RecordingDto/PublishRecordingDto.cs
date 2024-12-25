using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.RecordingDto
{
    public class PublishRecordingDto
    {
        public string RecordID { get; set; }
        public bool IsPublished { get; set; }
        public string Message { get; set; }
    }
}
