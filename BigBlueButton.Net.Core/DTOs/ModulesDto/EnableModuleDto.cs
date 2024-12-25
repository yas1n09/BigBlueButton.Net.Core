using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.ModulesDto
{
    public class EnableModuleDto
    {
        public string MeetingID { get; set; }
        public string ModuleName { get; set; }
        public bool IsEnabled { get; set; }
        public string Message { get; set; }
    }
}
