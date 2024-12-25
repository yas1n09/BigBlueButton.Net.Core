using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.DTO;
using BigBlueButton.Net.Core.DTOs.MeetingDto;
using BigBlueButton.Net.Core.Enums;
using BigBlueButton.Net.Core.Helpers;
using BigBlueButton.Net.Core.Requests;
using BigBlueButtonAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using MeetingStatusDto = BigBlueButton.Net.Core.DTOs.MeetingDto.MeetingStatusDto;

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
                    return Content(XmlHelper.ToXml(new MeetingErrorResponseDto
                    {
                        Message = "Failed to create meeting.",
                        Details = result.message
                    }), "application/xml");
                }

                return Content(XmlHelper.ToXml(new MeetingCreateDto
                {
                    MeetingID = result.meetingID,
                    InternalMeetingID = result.internalMeetingID,
                    ParentMeetingID = result.parentMeetingID,
                    AttendeePW = result.attendeePW,
                    ModeratorPW = result.moderatorPW,
                    CreateTime = result.createTime,
                    VoiceBridge = result.voiceBridge,
                    DialNumber = result.dialNumber,
                    CreateDate = result.createDate,
                    HasUserJoined = result.hasUserJoined,
                    Duration = result.duration,
                    HasBeenForciblyEnded = result.hasBeenForciblyEnded,
                    Message = "Meeting created successfully."
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new MeetingErrorResponseDto
                {
                    Message = "An error occurred while creating the meeting.",
                    Details = ex.Message
                }));
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
                    return Content(XmlHelper.ToXml(new MeetingErrorResponseDto
                    {
                        Message = "Failed to end meeting.",
                        Details = result.message
                    }), "application/xml");
                }

                return Content(XmlHelper.ToXml(new MeetingEndDto
                {
                    MeetingID = meetingID,
                    Message = "Meeting ended successfully."
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new MeetingErrorResponseDto
                {
                    Message = "An error occurred while ending the meeting.",
                    Details = ex.Message
                }));
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

                return Content(XmlHelper.ToXml(new JoinMeetingResponseDto
                {
                    JoinUrl = joinUrl
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new MeetingErrorResponseDto
                {
                    Message = "An error occurred while joining the meeting.",
                    Details = ex.Message
                }));
            }
        }
        #endregion






        #region Is Meeting Running
        [HttpGet("isRunning")]
        public async Task<IActionResult> IsMeetingRunning(string meetingID)
        {
            if (string.IsNullOrEmpty(meetingID))
            {
                return Content(XmlHelper.ToXml(new MeetingErrorResponseDto
                {
                    Message = "Meeting ID cannot be null or empty.",
                    Details = "Invalid input."
                }), "application/xml");
            }

            try
            {
                var response = await client.IsMeetingRunningAsync(new IsMeetingRunningRequest
                {
                    meetingID = meetingID
                });

                return Content(XmlHelper.ToXml(new MeetingStatusDto
                {
                    MeetingID = meetingID,
                    IsRunning = response.running ?? false,
                    Message = response.running == true ? "Meeting is running." : "Meeting is not running."
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new MeetingErrorResponseDto
                {
                    Message = "An error occurred while checking meeting status.",
                    Details = ex.Message
                }));
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
                    return Content(XmlHelper.ToXml(new MeetingErrorResponseDto
                    {
                        Message = "Failed to retrieve meeting info.",
                        Details = result.message
                    }), "application/xml");
                }

                return Content(XmlHelper.ToXml(new MeetingInfoDto
                {
                    MeetingID = result.meetingID,
                    MeetingName = result.meetingName,
                    InternalMeetingID = result.internalMeetingID,
                    CreateTime = result.createTime,
                    CreateDate = result.createDate,
                    VoiceBridge = result.voiceBridge,
                    DialNumber = result.dialNumber,
                    AttendeePW = result.attendeePW,
                    ModeratorPW = result.moderatorPW
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new MeetingErrorResponseDto
                {
                    Message = "An error occurred while retrieving meeting info.",
                    Details = ex.Message
                }));
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
                    return Content(XmlHelper.ToXml(new MeetingErrorResponseDto
                    {
                        Message = "No meetings found.",
                        Details = "Meetings list is empty."
                    }), "application/xml");
                }

                var response = new AllMeetingsDto
                {
                    Message = "Meetings retrieved successfully.",
                    Meetings = meetings
                };

                return Content(XmlHelper.ToXml(response), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new MeetingErrorResponseDto
                {
                    Message = "An error occurred while retrieving meetings.",
                    Details = ex.Message
                }));
            }
        }
        #endregion



































        //#region Create Meeting
        //[HttpPost("create")]
        //public async Task<IActionResult> CreateMeeting(string name, string meetingID, bool record = false)
        //{
        //    try
        //    {
        //        var result = await client.CreateMeetingAsync(new CreateMeetingRequest
        //        {
        //            name = name,
        //            meetingID = meetingID,
        //            record = record
        //        });

        //        if (result.returncode == Returncode.FAILED)
        //        {
        //            return Content(XmlHelper.ToXml(new MeetingErrorResponseDto
        //            {
        //                Message = "Failed to create meeting.",
        //                Details = result.message
        //            }), "application/xml");
        //        }

        //        return Content(XmlHelper.ToXml(new MeetingCreateDto
        //        {
        //            MeetingID = result.meetingID,
        //            InternalMeetingID = result.internalMeetingID,
        //            ParentMeetingID = result.parentMeetingID,
        //            AttendeePW = result.attendeePW,
        //            ModeratorPW = result.moderatorPW,
        //            CreateTime = result.createTime,
        //            VoiceBridge = result.voiceBridge,
        //            DialNumber = result.dialNumber,
        //            CreateDate = result.createDate,
        //            HasUserJoined = result.hasUserJoined,
        //            Duration = result.duration,
        //            HasBeenForciblyEnded = result.hasBeenForciblyEnded,
        //            Message = "Meeting created successfully."
        //        }), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.ToXml(new MeetingErrorResponseDto
        //        {
        //            Message = "An error occurred while creating the meeting.",
        //            Details = ex.Message
        //        }));
        //    }
        //}
        //#endregion



        //#region End Meeting
        //[HttpPost("end")]
        //public async Task<IActionResult> EndMeeting(string meetingID, string password)
        //{
        //    try
        //    {
        //        var result = await client.EndMeetingAsync(new EndMeetingRequest
        //        {
        //            meetingID = meetingID,
        //            password = password
        //        });

        //        if (result.returncode == Returncode.FAILED)
        //        {
        //            return Content(XmlHelper.ToXml(new MeetingErrorResponseDto
        //            {
        //                Message = "Failed to end meeting.",
        //                Details = result.message
        //            }), "application/xml");
        //        }

        //        return Content(XmlHelper.ToXml(new MeetingEndDto
        //        {
        //            MeetingID = meetingID,
        //            Message = "Meeting ended successfully."
        //        }), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.ToXml(new MeetingErrorResponseDto
        //        {
        //            Message = "An error occurred while ending the meeting.",
        //            Details = ex.Message
        //        }));
        //    }
        //}
        //#endregion



        //#region Join Meeting
        //[HttpGet("join")]
        //public async Task<IActionResult> JoinMeeting(string meetingID, string fullName, string password)
        //{
        //    try
        //    {
        //        var joinRequest = new JoinMeetingRequest
        //        {
        //            meetingID = meetingID,
        //            fullName = fullName,
        //            password = password
        //        };

        //        var joinUrl = client.GetJoinMeetingUrl(joinRequest);

        //        return Content(XmlHelper.ToXml(new JoinMeetingResponseDto
        //        {
        //            JoinUrl = joinUrl
        //        }), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.ToXml(new MeetingErrorResponseDto
        //        {
        //            Message = "An error occurred while joining the meeting.",
        //            Details = ex.Message
        //        }));
        //    }
        //}
        //#endregion



        //#region Is Meeting Running
        //[HttpGet("isRunning")]
        //public async Task<IActionResult> IsMeetingRunning(string meetingID)
        //{
        //    if (string.IsNullOrEmpty(meetingID))
        //    {
        //        return Content(XmlHelper.ToXml(new MeetingErrorResponseDto
        //        {
        //            Message = "Meeting ID cannot be null or empty.",
        //            Details = "Invalid input."
        //        }), "application/xml");
        //    }

        //    try
        //    {
        //        var response = await client.IsMeetingRunningAsync(new IsMeetingRunningRequest
        //        {
        //            meetingID = meetingID
        //        });

        //        return Content(XmlHelper.ToXml(new MeetingStatusDto
        //        {
        //            MeetingID = meetingID,
        //            IsRunning = response.running ?? false,
        //            Message = response.running == true ? "Meeting is running." : "Meeting is not running."
        //        }), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.ToXml(new MeetingErrorResponseDto
        //        {
        //            Message = "An error occurred while checking meeting status.",
        //            Details = ex.Message
        //        }));
        //    }
        //}
        //#endregion




        //#region Get Meeting Info
        //[HttpGet("info")]
        //public async Task<IActionResult> GetMeetingInfo(string meetingID, string password)
        //{
        //    try
        //    {
        //        var result = await client.GetMeetingInfoAsync(new GetMeetingInfoRequest
        //        {
        //            meetingID = meetingID,
        //            password = password
        //        });

        //        if (result.returncode == Returncode.FAILED)
        //        {
        //            return Content(XmlHelper.ToXml(new MeetingErrorResponseDto
        //            {
        //                Message = "Failed to retrieve meeting info.",
        //                Details = result.message
        //            }), "application/xml");
        //        }

        //        var meetingInfo = new MeetingInfoDto
        //        {
        //            MeetingID = result.meetingID,
        //            MeetingName = result.meetingName,
        //            InternalMeetingID = result.internalMeetingID,
        //            CreateTime = result.createTime,
        //            CreateDate = result.createDate,
        //            VoiceBridge = result.voiceBridge,
        //            DialNumber = result.dialNumber,
        //            AttendeePW = result.attendeePW,
        //            ModeratorPW = result.moderatorPW,
        //            Running = result.running,
        //            Duration = result.duration,
        //            HasUserJoined = result.hasUserJoined,
        //            Recording = result.recording,
        //            HasBeenForciblyEnded = result.hasBeenForciblyEnded,
        //            StartTime = result.startTime,
        //            EndTime = result.endTime,
        //            ParticipantCount = result.participantCount,
        //            ListenerCount = result.listenerCount,
        //            VoiceParticipantCount = result.voiceParticipantCount,
        //            VideoCount = result.videoCount,
        //            MaxUsers = result.maxUsers,
        //            ModeratorCount = result.moderatorCount,
        //            IsBreakout = result.isBreakout,
        //            BreakoutRooms = result.breakoutRooms,
        //            ParentMeetingID = result.parentMeetingID,
        //            Sequence = result.sequence,
        //            FreeJoin = result.freeJoin
        //        };

        //        return Content(XmlHelper.ToXml(meetingInfo), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.ToXml(new MeetingErrorResponseDto
        //        {
        //            Message = "An error occurred while retrieving meeting info.",
        //            Details = ex.Message
        //        }));
        //    }
        //}
        //#endregion




        //#region Get All Meetings
        //[HttpGet("all")]
        //public async Task<IActionResult> GetAllMeetings()
        //{
        //    try
        //    {
        //        var meetings = await client.GetMeetingsAsync();

        //        if (meetings == null || meetings.returncode == Returncode.FAILED)
        //        {
        //            return Content(XmlHelper.ToXml(new MeetingErrorResponseDto
        //            {
        //                Message = "No meetings found.",
        //                Details = "Meetings list is empty."
        //            }), "application/xml");
        //        }

        //        var response = new AllMeetingsDto
        //        {
        //            Message = "Meetings retrieved successfully.",
        //            Meetings = meetings
        //        };

        //        return Content(XmlHelper.ToXml(response), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.ToXml(new MeetingErrorResponseDto
        //        {
        //            Message = "An error occurred while retrieving meetings.",
        //            Details = ex.Message
        //        }));
        //    }
        //}
        //#endregion








    }
}
