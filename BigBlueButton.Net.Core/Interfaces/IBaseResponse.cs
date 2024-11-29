using BigBlueButton.Net.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Interfaces
{
    public interface IBaseResponse
    {
        /// <summary>
        /// İstenilen işlemin başarılı olup olmadığını gösterir.
        /// </summary>
        Returncode returncode { get; set; }

        /// <summary>
        /// Mesajla benzer işlevsellik sağlar ve aynı kurallara uyar. 
        /// Ancak, mesaj anahtarı çok daha kısa olacaktır ve genellikle API'nin ömrü boyunca aynı kalacaktır, oysa mesaj zamanla değişebilir. 
        /// Üçüncü taraf uygulamanız, standart mesajları uluslararasılaştırmak veya değiştirmek istiyorsa, bu messageKey'e dayalı özel mesajlarınızı arayabilirsiniz.
        /// </summary>
        string messageKey { get; set; }

        /// <summary>
        /// Çağrının durumu hakkında ek bilgi sağlayan bir mesaj. 
        /// returncode FAILED olduğunda her zaman bir mesaj parametresi döndürülecektir. 
        /// returncode SUCCESS olduğunda da bazı durumlarda, ek bilgi faydalı olursa bir mesaj döndürülebilir.
        /// </summary>
        string message { get; set; }
    }

}
