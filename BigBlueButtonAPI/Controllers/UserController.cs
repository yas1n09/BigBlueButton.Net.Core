using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.Enums;
using BigBlueButton.Net.Core.Helpers;
using BigBlueButton.Net.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BigBlueButtonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BigBlueButtonAPIClient client;

        public UserController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }

        #region Add User to Meeting
        [HttpPost("add")]
        public IActionResult AddUserToMeeting(string meetingID, string fullName, string password, string role)
        {
            var joinRequest = new JoinMeetingRequest
            {
                meetingID = meetingID,
                fullName = fullName,
                password = password
            };

            if (role.ToLower() == "moderator" || role.ToLower() == "attendee")
            {
                joinRequest.password = password;
            }
            else
            {
                return Content(XmlHelper.XmlErrorResponse("Invalid role.", "Role must be 'moderator' or 'attendee'."), "application/xml");
            }

            var url = client.GetJoinMeetingUrl(joinRequest);
            return Redirect(url);
        }
        #endregion

        #region Remove User from Meeting
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveUserFromMeeting(string meetingID, string userID)
        {
            var result = await client.EjectParticipantAsync(new EjectParticipantRequest
            {
                meetingID = meetingID,
                userID = userID
            });

            if (result.returncode == Returncode.FAILED.ToString())
            {
                return Content(XmlHelper.XmlErrorResponse("Failed to remove user from meeting.", result.message), "application/xml");
            }

            var successResponse = new
            {
                message = "User removed successfully."
            };

            return Content(XmlHelper.ToXml(successResponse), "application/xml");
        }
        #endregion

        #region Get User List
        [HttpGet("list")]
        public async Task<IActionResult> GetUserList(string meetingID, string password)
        {
            var result = await client.GetMeetingInfoAsync(new GetMeetingInfoRequest
            {
                meetingID = meetingID,
                password = password
            });

            if (result.returncode == Returncode.FAILED)
            {
                return Content(XmlHelper.XmlErrorResponse("Failed to retrieve user list.", result.message), "application/xml");
            }

            return Content(XmlHelper.ToXml(result.attendees), "application/xml");
        }
        #endregion
    }
}
