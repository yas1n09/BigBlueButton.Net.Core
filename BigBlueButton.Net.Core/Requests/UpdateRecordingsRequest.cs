using BigBlueButton.Net.Core.BaseClasses;
using BigBlueButton.Net.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class UpdateRecordingsRequest : BaseRequest
    {
        // Gerekli.
        // Yayınlama işlemi uygulanacak kayıtları belirtmek için bir kayıt ID'si. Virgülle ayrılmış bir dizi kayıt ID'si olabilir.
        public string recordID { get; set; }

        // Gerekli.
        // Güncellenmesi için bir veya daha fazla meta veri değeri geçebilirsiniz.
        public MetaData meta { get; set; }
    }

}
