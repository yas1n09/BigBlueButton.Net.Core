using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.DTOs.UserDto;
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
            try
            {
                var joinRequest = new JoinMeetingRequest
                {
                    meetingID = meetingID,
                    fullName = fullName,
                    password = password
                };

                if (role.ToLower() != "moderator" && role.ToLower() != "attendee")
                {
                    return Content(XmlHelper.XmlErrorResponse<UserErrorResponseDto>(
                        "Invalid role.",
                        "Role must be 'moderator' or 'attendee'."), "application/xml");
                }

                var url = client.GetJoinMeetingUrl(joinRequest);

                var response = new AddUserDto
                {
                    MeetingID = meetingID,
                    FullName = fullName,
                    Role = role,
                    JoinUrl = url,
                    Message = "User added successfully."
                };

                return Content(XmlHelper.ToXml(response), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<UserErrorResponseDto>(
                    "An error occurred while adding the user.",
                    ex.Message));
            }
        }
        #endregion







        #region Remove User from Meeting
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveUserFromMeeting(string meetingID, string userID)
        {
            try
            {
                var result = await client.EjectParticipantAsync(new EjectParticipantRequest
                {
                    meetingID = meetingID,
                    userID = userID
                });

                if (result.returncode == Returncode.FAILED.ToString())
                {
                    return Content(XmlHelper.XmlErrorResponse<UserErrorResponseDto>(
                        "Failed to remove user from meeting.",
                        result.message), "application/xml");
                }

                var response = new RemoveUserDto
                {
                    MeetingID = meetingID,
                    UserID = userID,
                    IsRemoved = true,
                    Message = "User removed successfully."
                };

                return Content(XmlHelper.ToXml(response), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<UserErrorResponseDto>(
                    "An error occurred while removing the user.",
                    ex.Message));
            }
        }
        #endregion







        #region Get User List
        [HttpGet("list")]
        public async Task<IActionResult> GetUserList(string meetingID, string password)
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
                    return Content(XmlHelper.XmlErrorResponse<UserErrorResponseDto>(
                        "Failed to retrieve user list.",
                        result.message), "application/xml");
                }

                // Attendees doğrudan List<Attendee> tipinde geliyor
                var attendees = result.attendees
                    .Select(a => new UserDto
                    {
                        UserID = a.userID,
                        FullName = a.fullName,
                        Role = a.role
                    }).ToList();

                var response = new UserListDto
                {
                    MeetingID = meetingID,
                    Attendees = attendees,
                    Message = "User list retrieved successfully."
                };

                return Content(XmlHelper.ToXml(response), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<UserErrorResponseDto>(
                    "An error occurred while retrieving the user list.",
                    ex.Message));
            }
        }
        #endregion

    }
}
