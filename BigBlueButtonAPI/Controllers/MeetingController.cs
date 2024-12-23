using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.DTO;
using BigBlueButton.Net.Core.Enums;
using BigBlueButton.Net.Core.Helpers;
using BigBlueButton.Net.Core.Requests;
using BigBlueButtonAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BigBlueButtonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly BigBlueButtonAPIClient client;

        public MeetingController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }

        #region Create Meeting
        [HttpPost("create")]
        public async Task<IActionResult> CreateMeeting(string name, string meetingID, bool record = false)
        {
            try
            {
                var result = await client.CreateMeetingAsync(new CreateMeetingRequest
                {
                    name = name,
                    meetingID = meetingID,
                    record = record
                });

                if (result.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse("Failed to create meeting.", result.message), "application/xml");
                }

                var response = new MeetingResponseDto
                {
                    Message = "Meeting created successfully.",
                    Result = result
                };

                return Content(XmlHelper.ToXml(response), "application/xml");
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponseDto
                {
                    Message = "An error occurred while creating the meeting.",
                    Details = ex.Message
                };
                return StatusCode(500, XmlHelper.ToXml(errorResponse));
            }
        }
        #endregion

        #region End Meeting
        [HttpPost("end")]
        public async Task<IActionResult> EndMeeting(string meetingID, string password)
        {
            try
            {
                var result = await client.EndMeetingAsync(new EndMeetingRequest
                {
                    meetingID = meetingID,
                    password = password
                });

                if (result.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse("Failed to end meeting.", result.message), "application/xml");
                }

                var response = new EndMeetingResponseDto
                {
                    Message = "Meeting ended successfully."
                };

                return Content(XmlHelper.ToXml(response), "application/xml");
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponseDto
                {
                    Message = "An error occurred while ending the meeting.",
                    Details = ex.Message
                };
                return StatusCode(500, XmlHelper.ToXml(errorResponse));
            }
        }
        #endregion

        #region Join Meeting
        [HttpGet("join")]
        public async Task<IActionResult> JoinMeeting(string meetingID, string fullName, string password)
        {
            try
            {
                var joinRequest = new JoinMeetingRequest
                {
                    meetingID = meetingID,
                    fullName = fullName,
                    password = password
                };

                var joinUrl = client.GetJoinMeetingUrl(joinRequest);

                var response = new JoinMeetingResponseDto
                {
                    JoinUrl = joinUrl
                };

                return Content(XmlHelper.ToXml(response), "application/xml");
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponseDto
                {
                    Message = "An error occurred while joining the meeting.",
                    Details = ex.Message
                };
                return StatusCode(500, XmlHelper.ToXml(errorResponse));
            }
        }
        #endregion

        #region Is Meeting Running
        [HttpGet("isRunning")]
        public async Task<IActionResult> IsMeetingRunning(string meetingID)
        {
            if (string.IsNullOrEmpty(meetingID))
            {
                return Content(XmlHelper.XmlErrorResponse("Meeting ID cannot be null or empty.", "Invalid input."), "application/xml");
            }

            try
            {
                var response = await client.IsMeetingRunningAsync(new IsMeetingRunningRequest
                {
                    meetingID = meetingID
                });

                if (response == null || response.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse("Meeting not found.", "Invalid meeting ID."), "application/xml");
                }

                var responseDto = new MeetingStatusResponseDto
                {
                    MeetingID = meetingID,
                    IsRunning = (bool)response.running,
                    Message = (bool)response.running ? "Meeting is running." : "Meeting is not running."
                };

                return Content(XmlHelper.ToXml(responseDto), "application/xml");
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponseDto
                {
                    Message = "An error occurred while checking meeting status.",
                    Details = ex.Message
                };
                return StatusCode(500, XmlHelper.ToXml(errorResponse));
            }
        }
        #endregion

        #region Get Meeting Info
        [HttpGet("info")]
        public async Task<IActionResult> GetMeetingInfo(string meetingID, string password)
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
                    return Content(XmlHelper.XmlErrorResponse("Failed to retrieve meeting info.", result.message), "application/xml");
                }

                var response = new MeetingInfoResponseDto
                {
                    Message = "Meeting info retrieved successfully.",
                    Result = result
                };

                return Content(XmlHelper.ToXml(response), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse("An error occurred while retrieving meeting info.", ex.Message));
            }
        }
        #endregion

        #region Get All Meetings
        [HttpGet("all")]
        public async Task<IActionResult> GetAllMeetings()
        {
            try
            {
                var meetings = await client.GetMeetingsAsync();

                if (meetings == null || meetings.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse("No meetings found.", "Meetings list is empty."), "application/xml");
                }

                var response = new AllMeetingsResponseDto
                {
                    Message = "Meetings retrieved successfully.",
                    Result = meetings
                };

                return Content(XmlHelper.ToXml(response), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse("An error occurred while retrieving meetings.", ex.Message));
            }
        }
        #endregion
    }
}
