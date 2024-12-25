using BigBlueButton.Net.Core.BaseClasses;
using BigBlueButton.Net.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class CreateMeetingRequest : BaseRequest
    {
        public string name { get; set; }  // Toplantı adı (isteğe bağlı)
        public string meetingID { get; set; }  // Toplantı ID'si (gerekli, her toplantının benzersiz olmalı)
        public string attendeePW { get; set; }  // Katılımcı şifresi (isteğe bağlı, varsayılan rastgele şifre oluşturulur)
        public string moderatorPW { get; set; }  // Moderatör şifresi (isteğe bağlı, varsayılan rastgele şifre oluşturulur)
        public string welcome { get; set; }  // Hoş geldin mesajı (isteğe bağlı)
        public string dialNumber { get; set; }  // Telefonla katılma numarası (isteğe bağlı)
        public int? voiceBridge { get; set; }  // Ses köprüsü numarası (isteğe bağlı)
        public int? maxParticipants { get; set; }  // Maksimum katılımcı sayısı (isteğe bağlı)
        public string logoutURL { get; set; }  // Çıkış URL'si (isteğe bağlı)
        public bool? record { get; set; }  // Kaydı başlatma (isteğe bağlı)
        public int? duration { get; set; }  // Toplantı süresi (isteğe bağlı)
        public bool? allowOverrideClientSettingsOnCreateCall { get; set; }  // İstemci ayarlarını geçersiz kılma izni (yeni)
        public bool? isBreakout { get; set; }  // Breakout odası (gereklidir)
        public string parentMeetingID { get; set; }  // Ana toplantı ID'si (gereklidir)
        public int? sequence { get; set; }  // Breakout odası sırası (gereklidir)
        public MetaData meta { get; set; }  // Meta veriler (isteğe bağlı)
        public bool? freeJoin { get; set; }  // Serbest katılım (isteğe bağlı)
        public bool? autoStartRecording { get; set; }  // Otomatik kayıt başlatma (isteğe bağlı)
        public bool? allowStartStopRecording { get; set; }  // Kayıt başlatma/durdurma izni (isteğe bağlı)
        public bool? webcamsOnlyForModerator { get; set; }  // Sadece moderatör için webcam (isteğe bağlı)
        public string logo { get; set; }  // Logo URL'si (isteğe bağlı)
        public string bannerText { get; set; }  // Banner metni (isteğe bağlı)
        public string bannerColor { get; set; }  // Banner rengi (isteğe bağlı)
        public string copyright { get; set; }  // Telif hakkı metni (isteğe bağlı)
        public bool? muteOnStart { get; set; }  // Başlangıçta tüm katılımcıları susturma (isteğe bağlı)
        public bool? allowModsToUnmuteUsers { get; set; }  // Moderatörlerin kullanıcıları açmasına izin verme (isteğe bağlı)
        public bool? lockSettingsDisableCam { get; set; }  // Kamerayı devre dışı bırakma (isteğe bağlı)
        public bool? lockSettingsDisableMic { get; set; }  // Mikrafonu devre dışı bırakma (isteğe bağlı)
        public bool? lockSettingsDisablePrivateChat { get; set; }  // Özel sohbeti devre dışı bırakma (isteğe bağlı)
        public bool? lockSettingsDisablePublicChat { get; set; }  // Genel sohbeti devre dışı bırakma (isteğe bağlı)
        public bool? lockSettingsDisableNote { get; set; }  // Notları devre dışı bırakma (isteğe bağlı)
        public bool? lockSettingsLockedLayout { get; set; }  // Düzeni kilitleme (isteğe bağlı)
        public bool? lockSettingsLockOnJoin { get; set; }  // Katılımcıların katılmasında kilit açma (isteğe bağlı)
        public bool? lockSettingsLockOnJoinConfigurable { get; set; }  // Katılımcı kilidi yapılandırılabilir (isteğe bağlı)
        public string guestPolicy { get; set; }  // Konuk politikası (isteğe bağlı)
    }

}
