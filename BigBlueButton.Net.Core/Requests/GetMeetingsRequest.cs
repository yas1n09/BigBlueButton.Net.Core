using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class GetMeetingsRequest : BaseRequest
    {
        // Bu sınıf, toplantıları sorgulamak için kullanılan temel istek sınıfıdır.
        public bool includeMetadata { get; set; } = false;  // Varsayılan olarak kapalı
    }

}
