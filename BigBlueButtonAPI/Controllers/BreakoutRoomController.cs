using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.Enums;
using BigBlueButton.Net.Core.Helpers;
using BigBlueButton.Net.Core.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigBlueButtonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreakoutRoomController : ControllerBase
    {
        private readonly BigBlueButtonAPIClient client;

        public BreakoutRoomController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }

        #region Create Breakout Room
        [HttpPost("create")]
        public async Task<IActionResult> CreateBreakoutRoom(CreateBreakoutRoomRequest request)
        {
            try
            {
                var result = await client.CreateMeetingAsync(request);

                if (result.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse("Failed to create breakout room.", result.message), "application/xml");
                }

                var successResponse = new
                {
                    message = "Breakout room created successfully.",
                    result
                };

                return Content(XmlHelper.ToXml(successResponse), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new { message = "An error occurred while creating the breakout room.", details = ex.Message }));
            }
        }
        #endregion

        #region Join Breakout Room
        [HttpPost("join")]
        public IActionResult JoinBreakoutRoom(JoinBreakoutRoomRequest request)
        {
            try
            {
                var joinUrl = client.GetJoinMeetingUrl(request);

                var successResponse = new
                {
                    message = "Successfully retrieved breakout room join URL.",
                    joinUrl
                };

                return Content(XmlHelper.ToXml(successResponse), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new { message = "Failed to join breakout room.", details = ex.Message }));
            }
        }
        #endregion

        #region Get Breakout Room Info
        [HttpGet("info")]
        public async Task<IActionResult> GetBreakoutRoomInfo(string meetingID, string password)
        {
            try
            {
                var result = await client.GetMeetingInfoAsync(new GetMeetingInfoRequest
                {
                    meetingID = meetingID,
                    password = password
                });

                if (result.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse("Failed to retrieve breakout room info.", result.message), "application/xml");
                }

                return Content(XmlHelper.ToXml(result), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new { message = "An error occurred while retrieving breakout room info.", details = ex.Message }));
            }
        }
        #endregion

        #region End Breakout Room
        [HttpPost("end")]
        public async Task<IActionResult> EndBreakoutRoom(EndBreakoutRoomRequest request)
        {
            try
            {
                var result = await client.EndMeetingAsync(request);

                if (result.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse("Failed to end breakout room.", result.message), "application/xml");
                }

                var successResponse = new
                {
                    message = "Breakout room ended successfully."
                };

                return Content(XmlHelper.ToXml(successResponse), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new { message = "An error occurred while ending the breakout room.", details = ex.Message }));
            }
        }
        #endregion
    }
}
