using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.DTOs.BreakoutRoomDto;
using BigBlueButton.Net.Core.Enums;
using BigBlueButton.Net.Core.Helpers;
using BigBlueButton.Net.Core.Requests;
using BigBlueButtonAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BreakoutRoomErrorResponseDto = BigBlueButton.Net.Core.DTOs.BreakoutRoomDto.BreakoutRoomErrorResponseDto;

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
        public async Task<IActionResult> CreateBreakoutRoom(CreateBreakoutRoomRequest request, bool redirect = true, string userdata = "")
        {
            try
            {
                request.redirect = redirect;
                request.userdata = userdata;

                var result = await client.CreateMeetingAsync(request);

                if (result.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
                        "Failed to create breakout room.",
                        result.message), "application/xml");
                }

                return Content(XmlHelper.ToXml(new BreakoutRoomCreateDto
                {
                    MeetingID = request.meetingID,
                    Name = request.name,
                    AttendeePW = request.attendeePW,
                    ModeratorPW = request.moderatorPW,
                    Duration = (int)request.duration,
                    Redirect = redirect,
                    Message = "Breakout room created successfully."
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
                    "An error occurred while creating the breakout room.",
                    ex.Message));
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
                    return Content(XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
                        "Failed to end breakout room.",
                        result.message), "application/xml");
                }

                return Content(XmlHelper.ToXml(new BreakoutRoomEndDto
                {
                    MeetingID = request.meetingID,
                    Message = "Breakout room ended successfully.",
                    EndTime = DateTime.UtcNow  // Toplantı sonlanma zamanı ekleniyor
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
                    "An error occurred while ending the breakout room.",
                    ex.Message));
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
                    return Content(XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
                        "Failed to retrieve breakout room info.",
                        result.message), "application/xml");
                }

                return Content(XmlHelper.ToXml(new BreakoutRoomInfoDto
                {
                    MeetingID = result.meetingID,
                    Name = result.meetingName,
                    ParticipantCount = result.participantCount ?? 0,
                    IsRunning = result.running ?? false,
                    StartTime = result.startTime,
                    EndTime = result.endTime,
                    MaxUsers = result.maxUsers ?? 0,
                    ModeratorCount = result.moderatorCount ?? 0,
                    VoiceParticipantCount = result.voiceParticipantCount ?? 0,
                    VideoCount = result.videoCount ?? 0,
                    ListenerCount = result.listenerCount ?? 0,
                    BreakoutRooms = result.breakoutRooms
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
                    "An error occurred while retrieving breakout room info.",
                    ex.Message));
            }
        }

        #endregion





        #region List Breakout Rooms
        [HttpGet("list")]
        public async Task<IActionResult> ListBreakoutRooms()
        {
            try
            {
                var meetings = await client.GetMeetingsAsync();

                if (meetings == null || meetings.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
                        "No breakout rooms found.",
                        "Meetings list is empty or an error occurred."), "application/xml");
                }

                var breakoutRooms = new List<BreakoutRoomInfoDto>();

                foreach (var meeting in meetings.meetings)
                {
                    breakoutRooms.Add(new BreakoutRoomInfoDto
                    {
                        MeetingID = meeting.meetingID,
                        Name = meeting.meetingName,
                        ParticipantCount = meeting.participantCount ?? 0,
                        IsRunning = meeting.running ?? false,
                        StartTime = meeting.startTime,
                        EndTime = meeting.endTime,
                        MaxUsers = meeting.maxUsers ?? 0,
                        ModeratorCount = meeting.moderatorCount ?? 0,
                        VoiceParticipantCount = meeting.voiceParticipantCount ?? 0,
                        VideoCount = meeting.videoCount ?? 0,
                        ListenerCount = meeting.listenerCount ?? 0,
                        BreakoutRooms = meeting.breakoutRooms
                    });
                }

                return Content(XmlHelper.ToXml(new BreakoutRoomListDto
                {
                    Message = "Breakout rooms retrieved successfully.",
                    BreakoutRooms = breakoutRooms
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
                    "An error occurred while retrieving breakout rooms.",
                    ex.Message));
            }
        }

        #endregion





        #region Join Breakout Room
        [HttpPost("join")]
        public IActionResult JoinBreakoutRoom(JoinBreakoutRoomRequest request, bool redirect = true, string userdata = "")
        {
            try
            {
                request.redirect = redirect;
                request.userdata = userdata;

                var joinUrl = client.GetJoinMeetingUrl(request);

                return Content(XmlHelper.ToXml(new BreakoutRoomJoinDto
                {
                    JoinUrl = joinUrl,
                    Redirect = redirect,
                    Message = "Successfully retrieved breakout room join URL."
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
                    "Failed to join breakout room.",
                    ex.Message));
            }
        }


        #endregion





        #region Get Breakout Room Participants
        [HttpGet("participants")]
        public async Task<IActionResult> GetBreakoutRoomParticipants(string meetingID)
        {
            try
            {
                var result = await client.GetMeetingInfoAsync(new GetMeetingInfoRequest { meetingID = meetingID });

                if (result.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
                        "Failed to retrieve participants.",
                        result.message), "application/xml");
                }

                var participants = new List<BreakoutRoomParticipantDto>();

                foreach (var attendee in result.attendees)
                {
                    participants.Add(new BreakoutRoomParticipantDto
                    {
                        ParticipantID = attendee.userID,
                        FullName = attendee.fullName,
                        Role = attendee.role
                    });
                }

                return Content(XmlHelper.ToXml(participants), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
                    "An error occurred while retrieving participants.",
                    ex.Message));
            }
        }
        #endregion






























        //#region Create Breakout Room
        //[HttpPost("create")]
        //public async Task<IActionResult> CreateBreakoutRoom(CreateBreakoutRoomRequest request)
        //{
        //    try
        //    {
        //        var result = await client.CreateMeetingAsync(request);

        //        if (result.returncode == Returncode.FAILED)
        //        {
        //            return Content(XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
        //                "Failed to create breakout room.",
        //                result.message), "application/xml");
        //        }

        //        return Content(XmlHelper.ToXml(new BreakoutRoomCreateDto
        //        {
        //            MeetingID = request.meetingID,
        //            Name = request.name,
        //            AttendeePW = request.attendeePW,
        //            ModeratorPW = request.moderatorPW,
        //            Duration = (int)request.duration
        //        }), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
        //            "An error occurred while creating the breakout room.",
        //            ex.Message));
        //    }
        //}
        //#endregion




        //#region End Breakout Room
        //[HttpPost("end")]
        //public async Task<IActionResult> EndBreakoutRoom(EndBreakoutRoomRequest request)
        //{
        //    try
        //    {
        //        var result = await client.EndMeetingAsync(request);

        //        if (result.returncode == Returncode.FAILED)
        //        {
        //            return Content(XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
        //                "Failed to end breakout room.",
        //                result.message), "application/xml");
        //        }

        //        return Content(XmlHelper.ToXml(new BreakoutRoomEndDto
        //        {
        //            MeetingID = request.meetingID,
        //            Message = "Breakout room ended successfully."
        //        }), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
        //            "An error occurred while ending the breakout room.",
        //            ex.Message));
        //    }
        //}
        //#endregion




        //#region Get Breakout Room Info
        //[HttpGet("info")]
        //public async Task<IActionResult> GetBreakoutRoomInfo(string meetingID, string password)
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
        //            return Content(XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
        //                "Failed to retrieve breakout room info.",
        //                result.message), "application/xml");
        //        }

        //        return Content(XmlHelper.ToXml(new BreakoutRoomInfoDto
        //        {
        //            MeetingID = result.meetingID,
        //            Name = result.meetingName,
        //            ParticipantCount = result.participantCount ?? 0,
        //            IsRunning = result.running ?? false
        //        }), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
        //            "An error occurred while retrieving breakout room info.",
        //            ex.Message));
        //    }
        //}
        //#endregion




        //#region List Breakout Rooms
        //[HttpGet("list")]
        //public async Task<IActionResult> ListBreakoutRooms()
        //{
        //    try
        //    {
        //        var meetings = await client.GetMeetingsAsync();

        //        var breakoutRooms = new List<BreakoutRoomInfoDto>();

        //        foreach (var meeting in meetings.meetings)
        //        {
        //            breakoutRooms.Add(new BreakoutRoomInfoDto
        //            {
        //                MeetingID = meeting.meetingID,
        //                Name = meeting.meetingName,
        //                ParticipantCount = meeting.participantCount ?? 0,
        //                IsRunning = meeting.running ?? false
        //            });
        //        }

        //        return Content(XmlHelper.ToXml(new BreakoutRoomListDto
        //        {
        //            Message = "Breakout rooms retrieved successfully.",
        //            BreakoutRooms = breakoutRooms
        //        }), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.XmlErrorResponse<BreakoutRoomErrorResponseDto>(
        //            "An error occurred while retrieving breakout rooms.",
        //            ex.Message));
        //    }
        //}
        //#endregion




    }
}
