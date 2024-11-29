using BigBlueButton.Net.Core.BaseClasses;
using BigBlueButton.Net.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.BigBlueButtonAPIClient
{
    public class BigBlueButtonAPIClient
    {
        #region Common
        private readonly HttpClient httpClient;
        private readonly UrlBuilder urlBuilder;

        /// <summary>
        /// Sınıfın yapıcı metodu.
        /// </summary>
        /// <param name="settings">BigBlueButtonAPI.Core.BigBlueButtonAPISettings sınıfı, BigBlueButton API için yapılandırma verilerini içerir.</param>
        /// <param name="httpClient">HttpClient örneği</param>
        public BigBlueButtonAPIClient(BigBlueButtonAPISettings settings, HttpClient httpClient)
        {
            this.urlBuilder = new UrlBuilder(settings);
            this.httpClient = httpClient;
        }

        private async Task<T> HttpGetAsync<T>(string method, BaseRequest request)
        {
            var url = urlBuilder.Build(method, request);
            var result = await HttpGetAsync<T>(url);
            return result;
        }
        private async Task<T> HttpGetAsync<T>(string url)
        {
            var response = await httpClient.GetAsync(url);
            var xmlOrJson = await response.Content.ReadAsStringAsync();
            if (typeof(T) == typeof(string)) return (T)(object)xmlOrJson;

            // Çoğu API XML dizesi döndürür.
            // getRecordingTextTracks API'si JSON dizesi döndürebilir.
            if (xmlOrJson.StartsWith("<"))
            {
                return XmlHelper.FromXml<T>(xmlOrJson);
            }
            else
            {
                var wrapper = JsonConvert.DeserializeObject<ResponseJsonWrapper<T>>(xmlOrJson);
                return wrapper.response;
            }
        }

        private async Task<T> HttpPostAsync<T>(string method, BaseRequest request)
        {
            var formData = urlBuilder.Build(method, request, true);
            var formDataBytes = System.Text.Encoding.UTF8.GetBytes(formData);

            var cts = new CancellationTokenSource();
            using (var content = new ByteArrayContent(formDataBytes))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var response = await httpClient.PostAsync(urlBuilder.BuildMethodUrl(method), content, cts.Token);
                var xmlOrJson = await response.Content.ReadAsStringAsync();
                if (typeof(T) == typeof(string)) return (T)(object)xmlOrJson;

                // Çoğu API XML dizesi döndürür.
                // getRecordingTextTracks API'si JSON dizesi döndürebilir.
                if (xmlOrJson.StartsWith("<"))
                {
                    return XmlHelper.FromXml<T>(xmlOrJson);
                }
                else
                {
                    var wrapper = JsonConvert.DeserializeObject<ResponseJsonWrapper<T>>(xmlOrJson);
                    return wrapper.response;
                }

            }

        }
        private async Task<T> HttpPostFileAsync<T>(string method, BasePostFileRequest request)
        {
            var url = urlBuilder.Build(method, request);
            var cts = new CancellationTokenSource();
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new ByteArrayContent(request.file.FileData), request.file.Name, request.file.FileName);

                var response = await httpClient.PostAsync(url, content, cts.Token);
                var xmlOrJson = await response.Content.ReadAsStringAsync();
                if (typeof(T) == typeof(string)) return (T)(object)xmlOrJson;

                // Çoğu API XML dizesi döndürür.
                // getRecordingTextTracks API'si JSON dizesi döndürebilir.
                if (xmlOrJson.StartsWith("<"))
                {
                    return XmlHelper.FromXml<T>(xmlOrJson);
                }
                else
                {
                    var wrapper = JsonConvert.DeserializeObject<ResponseJsonWrapper<T>>(xmlOrJson);
                    return wrapper.response;
                }

            }

        }
        #endregion


    }
}
