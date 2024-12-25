using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class JoinMeetingRequest : BaseRequest
    {
        public string userdata;

        // Gereklidir.
        // Bu kullanıcıyı diğer katılımcılara tanıtacak tam ad.
        public string fullName { get; set; }

        // Gereklidir.
        // Katılmak istenen toplantıyı tanımlayan toplantı ID'si.
        public string meetingID { get; set; }

        // Gereklidir.
        // Katılımcının kullanacağı şifre. 
        // Moderatör şifresi sağlanırsa, kullanıcı moderatör statüsüne sahip olur.
        public string password { get; set; }

        // Opsiyonel.
        // Üçüncü parti uygulamalar, oluşturulma zamanını belirten createTime parametresini geçebilir. Bu parametre, toplantının başlatılma zamanı ile eşleşmelidir.
        public long? createTime { get; set; }

        // Opsiyonel.
        // Bu kullanıcıyı tanımlamak için bir kimlik numarası. Bu ID, getMeetingInfo API çağrısında döndürülecektir.
        public string userID { get; set; }

        // Opsiyonel.
        // VoIP ile sesli konferansa katılırken özel bir ses uzantısı geçmek için kullanılabilir.
        public string webVoiceConf { get; set; }

        // Opsiyonel.
        // BigBlueButton istemcisinin, belirtilen token ile ilişkili config.xml dosyasını yüklemesini sağlar.
        public string configToken { get; set; }

        // Opsiyonel.
        // Uygulama yüklendiğinde ilk olarak yüklenecek düzenin adı.
        public string defaultLayout { get; set; }

        // Opsiyonel.
        // Kullanıcı avatarını, config.xml dosyasında displayAvatar true olarak ayarlandığında göstermek için avatar URL'si.
        public string avatarURL { get; set; }

        // Opsiyonel (Deneysel).
        // Varsayılan JOIN API davranışı, başarılı bir katılım çağrısında Flash istemcisine yönlendirmedir. FALSE olarak ayarlandığında, yönlendirme yapılmaz ve XML döndürülür.
        public bool? redirect { get; set; }

        // Opsiyonel (Deneysel).
        // Özel istemci uygulamaları için, yönlendirme yapılmadığında özel istemci URL'si belirlenebilir.
        public string clientURL { get; set; }

        // Opsiyonel.
        // HTML5 istemcisinin kullanıcı için yüklenmesi gerektiğini belirtir.
        public bool? joinViaHtml5 { get; set; }

        // Opsiyonel.
        // Kullanıcının misafir olduğunu belirtir.
        public bool? guest { get; set; }
    }

}
