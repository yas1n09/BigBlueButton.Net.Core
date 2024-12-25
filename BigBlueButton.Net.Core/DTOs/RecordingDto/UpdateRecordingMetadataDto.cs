using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.RecordingDto
{
    public class UpdateRecordingMetadataDto
    {
        public string RecordID { get; set; }
        public bool IsUpdated { get; set; }
        public string Message { get; set; }
    }
}
