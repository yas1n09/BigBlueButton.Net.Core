using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.Responses
{
    [XmlRoot("response")]
    public class UpdateRecordingsResponse : BaseResponse
    {
        // Bu yanıt, kayıtların başarıyla güncellenip güncellenmediğini belirten bir değer içerir.
        public bool? updated { get; set; }
    }

}
