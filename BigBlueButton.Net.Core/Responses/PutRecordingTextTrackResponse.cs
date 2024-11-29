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
    public class PutRecordingTextTrackResponse : BaseResponse
    {
        // Kayıt ID'si, metin parçasının başarıyla güncellenip güncellenmediğini belirten yanıt.
        public string recordID { get; set; }
    }

}
