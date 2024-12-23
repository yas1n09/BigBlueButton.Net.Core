using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
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
            var result = await client.GetMeetingStatsAsync(new GetMeetingStatsRequest
            {
                meetingID = meetingID
            });

            if (result.returncode == "FAILED")
            {
                return Content(XmlHelper.XmlErrorResponse("Failed to retrieve meeting stats.", result.message), "application/xml");
            }

            var xmlResult = XmlHelper.ToXml(result);
            return Content(xmlResult, "application/xml");
        }
        #endregion

        #region Export Meeting Data
        [HttpGet("export")]
        public async Task<IActionResult> ExportMeetingData(string meetingID)
        {
            var result = await client.ExportMeetingDataAsync(new ExportMeetingDataRequest
            {
                meetingID = meetingID
            });

            if (result.returncode == "FAILED")
            {
                return Content(XmlHelper.XmlErrorResponse("Failed to export meeting data.", result.message), "application/xml");
            }

            return File(result.Data, "application/json", "MeetingData.json");
        }
        #endregion
    }
}
