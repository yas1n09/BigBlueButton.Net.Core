using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class DisableModuleRequest : BaseRequest
    {
        public string meetingID { get; set; }

        public string moduleName { get; set; }
        public bool redirect { get; set; }
    }
}
