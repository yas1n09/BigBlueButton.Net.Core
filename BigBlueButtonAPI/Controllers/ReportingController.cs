using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.DTOs.ReportingDto;
using BigBlueButton.Net.Core.Helpers;
using BigBlueButton.Net.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BigBlueButtonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportingController : ControllerBase
    {
        private readonly BigBlueButtonAPIClient client;

        public ReportingController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }

        #region Get Meeting Stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetMeetingStats(string meetingID)
        {
            try
            {
                var request = new GetMeetingStatsRequest
                {
                    meetingID = meetingID
                };

                var result = await client.GetMeetingStatsAsync(request);

                if (result.returncode == "FAILED")
                {
                    return Content(XmlHelper.XmlErrorResponse<ReportingErrorResponseDto>(
                        "Failed to retrieve meeting stats.",
                        result.message), "application/xml");
                }

                var meetingStatsDto = new MeetingStatsDto
                {
                    MeetingID = meetingID,  // Request üzerinden meetingID atanıyor
                    ParticipantCount = result.participantCount,
                    ListenerCount = result.listenerCount,
                    VoiceParticipantCount = result.voiceParticipantCount,
                    VideoCount = result.videoCount,
                    ModeratorCount = result.moderatorCount,
                    StartTime = result.startTime,
                    EndTime = result.endTime,
                    IsRunning = result.running,
                    Message = "Meeting stats retrieved successfully."
                };

                return Content(XmlHelper.ToXml(meetingStatsDto), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<ReportingErrorResponseDto>(
                    "An error occurred while retrieving meeting stats.",
                    ex.Message));
            }
        }
        #endregion





        #region Export Meeting Data
        [HttpGet("export")]
        public async Task<IActionResult> ExportMeetingData(string meetingID)
        {
            try
            {
                var result = await client.ExportMeetingDataAsync(new ExportMeetingDataRequest
                {
                    meetingID = meetingID
                });

                if (result.returncode == "FAILED")
                {
                    return Content(XmlHelper.XmlErrorResponse<ReportingErrorResponseDto>(
                        "Failed to export meeting data.",
                        result.message), "application/xml");
                }

                // DTO oluştur ve döndür
                var exportDto = new ExportMeetingDataDto
                {
                    MeetingID = meetingID,
                    Data = result.Data,
                    FileName = "MeetingData.json",
                    Message = "Meeting data exported successfully."
                };

                // JSON dosya olarak döndürülür
                return File(exportDto.Data, "application/json", exportDto.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<ReportingErrorResponseDto>(
                    "An error occurred while exporting meeting data.",
                    ex.Message));
            }
        }
        #endregion


    }
}
