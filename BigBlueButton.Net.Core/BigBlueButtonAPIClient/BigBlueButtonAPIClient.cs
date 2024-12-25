using BigBlueButton.Net.Core.BaseClasses;
using BigBlueButton.Net.Core.Helpers;
using BigBlueButton.Net.Core.Requests;
using BigBlueButton.Net.Core.Responses;
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
            // HTTP isteği gönder
            var response = await httpClient.GetAsync(url);

            // Yanıtı metin olarak oku
            var xmlOrJson = await response.Content.ReadAsStringAsync();

            // Yanıtı logla
            Console.WriteLine("Response XML/JSON: " + xmlOrJson);

            // Eğer tip string ise yanıtı direkt döndür
            if (typeof(T) == typeof(string)) return (T)(object)xmlOrJson;

            // Eğer XML ise deserialize et
            if (xmlOrJson.StartsWith("<"))
            {
                return XmlHelper.FromXml<T>(xmlOrJson);
            }
            else
            {
                // JSON formatı varsa işle
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

                // Most APIs return XML string.
                // The getRecordingTextTracks API may return JSON string.
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

                // Most APIs return XML string.
                // The getRecordingTextTracks API may return JSON string.
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

        #region create
        
        public async Task<CreateMeetingResponse> CreateMeetingAsync(CreateMeetingRequest request)
        {
            //if (request == null) throw new ArgumentNullException("request");
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await HttpGetAsync<CreateMeetingResponse>("create", request);
        }
        #endregion

        #region getDefaultConfigXML
        /// <summary>
        /// Gets the default config.xml (these settings configure the BigBlueButton client for each user).
        /// Retrieve the default config.xml. This call enables a 3rd party application to get the current config.xml, modify it’s parameters, and use setConfigXML to store it on the BigBlueButton server (getting a reference token to the new config.xml), then using the token in as a parameter in the join URL to override the default config.xml.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetDefaultConfigXMLAsync(GetDefaultConfigXMLRequest request = null)
        {
            return await HttpGetAsync<string>("getDefaultConfigXML", request);
        }
        #endregion

        #region setConfigXML
        /// <summary>
        /// Add a custom config.xml to an existing meeting.
        /// Associate a custom config.xml file with the current session. This call returns a token that can later be passed as a parameter to a join URL. When passed as a parameter, the BigBlueButton client will use the associated config.xml for the user instead of using the default config.xml. This enables 3rd party applications to provide user-specific config.xml files.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SetConfigXMLResponse> SetConfigXMLAsync(SetConfigXMLRequest request)
        {
            return await HttpPostAsync<SetConfigXMLResponse>("setConfigXML", request);
        }
        #endregion

        #region join
        /// <summary>
        /// Join a new user to an existing meeting.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetJoinMeetingUrl(JoinMeetingRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

            // If the redirect is set as false, the reponse will be a XML, but there is 401 error when use the URL in the XML.
            // Maybe BigBlueButton API set some cookies at the same time. 
            // So I set redirect property is false.
            if (request.redirect == false) request.redirect = true;

            return urlBuilder.Build("join", request);
        }
        #endregion

        #region end
        /// <summary>
        /// Ends meeting.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EndMeetingResponse> EndMeetingAsync(EndMeetingRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpGetAsync<EndMeetingResponse>("end", request);
        }
        #endregion

        #region isMeetingRunning
        
        public async Task<IsMeetingRunningResponse> IsMeetingRunningAsync(IsMeetingRunningRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpGetAsync<IsMeetingRunningResponse>("isMeetingRunning", request);
        }
        #endregion

        #region getMeetings
        /// <summary>
        /// Get the list of Meetings.
        /// </summary>
        /// <returns></returns>
        public async Task<GetMeetingsResponse> GetMeetingsAsync(GetMeetingsRequest request = null)
        {
            return await HttpGetAsync<GetMeetingsResponse>("getMeetings", request);
        }
        #endregion

        #region getMeetingInfo
        /// <summary>
        /// Get the details of a Meeting.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetMeetingInfoResponse> GetMeetingInfoAsync(GetMeetingInfoRequest request)
        {
            return await HttpGetAsync<GetMeetingInfoResponse>("getMeetingInfo", request);
        }
        #endregion


        #region getRecordings
        /// <summary>
        /// Get a list of recordings.
        /// Retrieves the recordings that are available for playback for a given meetingID (or set of meeting IDs).
        /// </summary>
        /// <returns></returns>
        public async Task<GetRecordingsResponse> GetRecordingsAsync(GetRecordingsRequest request = null)
        {
            var result = await HttpGetAsync<GetRecordingsResponse>("getRecordings", request);

            //The url may contain leading and trailing white-space characters.
            if (result.recordings != null && result.recordings.Count > 0)
            {
                foreach (var recording in result.recordings)
                {
                    if (recording.playbacks != null && recording.playbacks.Count > 0)
                    {
                        foreach (var f in recording.playbacks)
                        {
                            if (f.url != null) f.url = f.url.Trim();
                            if (f.previewImages != null && f.previewImages.Count > 0)
                            {
                                foreach (var image in f.previewImages)
                                {
                                    if (image.url != null) image.url = image.url.Trim();
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #region publishRecordings
        /// <summary>
        /// Enables publishing or unpublishing of a recording.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PublishRecordingsResponse> PublishRecordingsAsync(PublishRecordingsRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpGetAsync<PublishRecordingsResponse>("publishRecordings", request);
        }
        #endregion

        #region deleteRecordings
        /// <summary>
        /// Deletes an existing recording.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DeleteRecordingsResponse> DeleteRecordingsAsync(DeleteRecordingsRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpGetAsync<DeleteRecordingsResponse>("deleteRecordings", request);
        }
        #endregion

        #region updateRecordings
        /// <summary>
        /// Updates metadata in a recording.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UpdateRecordingsResponse> UpdateRecordingsAsync(UpdateRecordingsRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpGetAsync<UpdateRecordingsResponse>("updateRecordings", request);
        }
        #endregion

        #region getRecordingTextTracks
        /// <summary>
        /// Get a list of the caption/subtitle.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetRecordingTextTracksResponse> GetRecordingTextTracksAsync(GetRecordingTextTracksRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpGetAsync<GetRecordingTextTracksResponse>("getRecordingTextTracks", request);
        }
        #endregion

        #region putRecordingTextTrack
        /// <summary>
        /// Upload a caption or subtitle file to add it to the recording.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fileContentData"></param>
        /// <returns></returns>
        public async Task<PutRecordingTextTrackResponse> PutRecordingTextTrackAsync(PutRecordingTextTrackRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpPostFileAsync<PutRecordingTextTrackResponse>("putRecordingTextTrack", request);
        }

        public async Task<bool> IsMeetingRunningAsync(string meetingID)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region EjectParticipant
        /// <summary>
        /// Ejects a participant from a meeting.
        /// </summary>
        /// <param name="request">The EjectParticipantRequest containing meetingID and userID</param>
        /// <returns>The response of the eject participant action</returns>
        public async Task<EjectParticipantResponse> EjectParticipantAsync(EjectParticipantRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.meetingID)) throw new ArgumentException("MeetingID is required", nameof(request));
            if (string.IsNullOrEmpty(request.userID)) throw new ArgumentException("UserID is required", nameof(request));

            return await HttpGetAsync<EjectParticipantResponse>("ejectParticipant", request);
        }
        #endregion

        #region Modules

        public async Task<EnableModuleResponse> EnableModuleAsync(EnableModuleRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return await HttpPostAsync<EnableModuleResponse>("enableModule", request);
        }

        public async Task<DisableModuleResponse> DisableModuleAsync(DisableModuleRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return await HttpPostAsync<DisableModuleResponse>("disableModule", request);
        }
        #endregion

        #region Reporting

        public async Task<GetMeetingStatsResponse> GetMeetingStatsAsync(GetMeetingStatsRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return await HttpGetAsync<GetMeetingStatsResponse>("getMeetingStats", request);
        }

        public async Task<ExportMeetingDataResponse> ExportMeetingDataAsync(ExportMeetingDataRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return await HttpGetAsync<ExportMeetingDataResponse>("exportMeetingData", request);
        }
        #endregion

        #region Recording

        // Kayıt Duraklatma
        public async Task<PauseRecordingResponse> PauseRecordingAsync(PauseRecordingRequest request)
        {
            return await HttpPostAsync<PauseRecordingResponse>("pauseRecording", request);
        }

        // Kayıt Devam Ettirme
        public async Task<ResumeRecordingResponse> ResumeRecordingAsync(ResumeRecordingRequest request)
        {
            return await HttpPostAsync<ResumeRecordingResponse>("resumeRecording", request);
        }

        #endregion

    }
}
