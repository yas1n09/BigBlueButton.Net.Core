﻿using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class IsMeetingRunningRequest : BaseRequest
    {
        // Gereklidir.
        // Kontrol edilmek istenen toplantıyı tanımlayan toplantı ID'si.
        public string meetingID { get; set; }
    }

}
