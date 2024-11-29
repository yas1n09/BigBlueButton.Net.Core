using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class PublishRecordingsRequest : BaseRequest
    {
        // Gereklidir.
        // Yayınlanacak kaydı belirtmek için bir kayıt ID'si. Birden fazla ID virgülle ayrılarak verilebilir.
        public string recordID { get; set; }

        // Gereklidir.
        // Kaydın yayımlanıp yayımlanmayacağını belirten değer.
        public bool publish { get; set; }
    }

}
