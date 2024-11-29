using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.BigBlueButtonAPIClient
{
    public class BigBlueButtonAPISettings
    {
        /// <summary>
        /// BigBlueButton sunucu API uç noktası (genellikle sunucunun ana bilgisayar adı ve ardından <b>/bigbluebutton/api/</b>, örneğin: http://sunucunuz.com/bigbluebutton/api/ ).
        /// </summary>
        public string ServerAPIUrl { get; set; }

        /// <summary>
        /// BigBlueButton sunucu API'si için gereken ortak gizli kod.
        /// Bunu BigBlueButton sunucunuzda şu komutla alabilirsiniz:
        ///     $ bbb-conf --secret
        /// </summary>
        public string SharedSecret { get; set; }
    }

}
