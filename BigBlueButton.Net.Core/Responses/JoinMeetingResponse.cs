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
    public class JoinMeetingResponse : BaseResponse
    {
        /// <summary>
        /// Sistem tarafından kullanılan dahili toplantı kimliği.
        /// </summary>
        [XmlElement("meeting_id")]
        public string internalMeetingID { get; set; }

        /// <summary>
        /// Bu kullanıcıyı tanımlamak için uygulamanızın kullanacağı bir kimlik.
        /// </summary>
        [XmlElement("user_id")]
        public string userID { get; set; }

        /// <summary>
        /// Kullanıcıyı arama URL'sine yönlendirmelisiniz, ardından toplantıya katılacaklardır.
        /// </summary>
        public string url { get; set; }
    }

}
