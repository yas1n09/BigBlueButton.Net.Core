using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class DeleteRecordingsRequest : BaseRequest
    {
        // Silinecek kayıtları belirtmek için bir kayıt ID'si. Birden fazla kayıt ID'si virgülle ayrılmış şekilde belirtilebilir.
        public string recordID { get; set; }
    }

}
