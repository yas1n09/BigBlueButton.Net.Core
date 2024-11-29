using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Enums
{
    public enum Returncode
    {
        SUCCESS = 0,
        // Çağrı başarılı oldu – Bu çağrı ile genellikle ilişkili olan diğer parametreler döndürülecektir.

        FAILED = 1
        // Bir hata oluştu – Daha fazla bilgi için mesaj ve messageKey'e bakın.

    }
}
