using BigBlueButton.Net.Core.BaseClasses;
using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Helpers
{
    public class UrlBuilder
    {
        private readonly BigBlueButtonAPISettings settings;

        // Yapıcı metod, BigBlueButton ayarlarını alır.
        public UrlBuilder(BigBlueButtonAPISettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// URL oluşturur.
        /// </summary>
        /// <param name="method">BigBlueButton API metod adı.</param>
        /// <param name="parameters">Sorgu parametreleri (checksum dahil edilmemiş haliyle)</param>
        /// <param name="onlyQueryString">Eğer true ise sadece sorgu string'ini döndürür, tam URL döndürmez.</param>
        /// <returns>Oluşturulmuş URL</returns>
        private string Build(string method, string parameters, bool onlyQueryString = false)
        {
            if (parameters == null) parameters = string.Empty;
            var checksum = GetChecksum(method, parameters);

            // Eğer sadece sorgu string'ini istiyorsa, checksum ile birlikte döndürür.
            if (onlyQueryString)
            {
                if (string.IsNullOrEmpty(parameters))
                {
                    return string.Format("checksum={0}", checksum);
                }
                else
                {
                    return string.Format("{0}&checksum={1}", parameters, checksum);
                }
            }
            else
            {
                // Tam URL döndürülür
                if (string.IsNullOrEmpty(parameters))
                {
                    return string.Format("{0}{1}?checksum={2}", settings.ServerAPIUrl, method, checksum);
                }
                else
                {
                    return string.Format("{0}{1}?{2}&checksum={3}", settings.ServerAPIUrl, method, parameters, checksum);
                }
            }

        }

        /// <summary>
        /// URL oluşturur.
        /// </summary>
        /// <param name="method">BigBlueButton API metod adı.</param>
        /// <param name="request">İstek verisi, sorgu parametrelerine dönüştürülür.</param>
        /// <param name="onlyQueryString">Sadece sorgu string'i döndürülüp döndürülmeyeceğini belirler.</param>
        /// <returns>Oluşturulmuş URL</returns>
        public string Build(string method, BaseRequest request, bool onlyQueryString = false)
        {
            string parameters = BuildParameters(method, request);
            return Build(method, parameters, onlyQueryString);
        }

        /// <summary>
        /// Belirtilen metod için temel URL'yi döndürür.
        /// Örnek: http://yourserver.com/bigbluebutton/api/create
        /// </summary>
        /// <param name="method">BigBlueButton API metod adı.</param>
        /// <returns>Temel URL</returns>
        public string BuildMethodUrl(string method)
        {
            return settings.ServerAPIUrl + method;
        }

        /// <summary>
        /// Parametreleri oluşturur.
        /// </summary>
        /// <param name="method">API metodu</param>
        /// <param name="request">İstek verisi</param>
        /// <returns>Oluşturulmuş sorgu parametreleri</returns>
        private string BuildParameters(string method, BaseRequest request)
        {
            string parameters = string.Empty;
            if (request != null)
            {
                var items = new List<KeyValuePair<string, string>>();

                string sValue;
                // Request sınıfındaki tüm public özelliklere bakılır
                foreach (System.Reflection.PropertyInfo p in request.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
                {
                    var value = p.GetValue(request, null);
                    if (value != null)
                    {
                        if (value is FileContentData) continue;
                        if (value is MetaData)
                        {
                            var metaData = (MetaData)value;
                            if (metaData.Count > 0)
                            {
                                foreach (var key in metaData.Keys)
                                {
                                    items.Add(new KeyValuePair<string, string>("meta_" + key, metaData[key]));
                                }

                            }
                        }
                        else
                        {
                            if (value.Equals(true)) sValue = "true";
                            else if (value.Equals(false)) sValue = "false";
                            else sValue = value.ToString();

                            items.Add(new KeyValuePair<string, string>(p.Name, sValue));
                        }

                    }
                }
                // Parametreler sıralanır ve string formatında döndürülür
                if (items.Count > 0)
                {
                    items.Sort((x, y) => x.Key.CompareTo(y.Key));
                    var c = new FormUrlEncodedContent(items);
                    parameters = c.ReadAsStringAsync().Result;
                }
            }
            return parameters;
        }

        /// <summary>
        /// Checksum hesaplar.
        /// </summary>
        /// <param name="method">API metodu</param>
        /// <param name="parameters">Parametreler</param>
        /// <returns>Checksum</returns>
        private string GetChecksum(string method, string parameters)
        {
            if (parameters == null) parameters = string.Empty;
            return HashHelper.Sha1Hash(method + parameters, settings.SharedSecret);
        }
    }

}
