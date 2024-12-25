using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.DTOs.RecordingDto;
using BigBlueButton.Net.Core.Entities;
using BigBlueButton.Net.Core.Enums;
using BigBlueButton.Net.Core.Helpers;
using BigBlueButton.Net.Core.Requests;

using Microsoft.AspNetCore.Mvc;
using System.Text;
using RecordingErrorResponseDto = BigBlueButton.Net.Core.DTOs.RecordingDto.RecordingErrorResponseDto;

namespace BigBlueButtonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordingController : ControllerBase
    {
        private readonly BigBlueButtonAPIClient client;

        public RecordingController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }


        #region Get All Recordings
        [HttpGet("all")]
        public async Task<IActionResult> Index(bool includeMetadata = false)
        {
            try
            {
                var request = new GetMeetingsRequest
                {
                    includeMetadata = includeMetadata  // Metadata parametresi desteği eklendi
                };

                var meetings = await client.GetMeetingsAsync(request);

                if (meetings == null || meetings.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
                        "No meetings found.",
                        "Meetings list is empty."), "application/xml");
                }

                return Content(XmlHelper.ToXml(meetings), "application/xml");
            }
            catch (Exception ex)
            {
                return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
                    "An error occurred while retrieving meetings.",
                    ex.Message), "application/xml");
            }
        }
        #endregion

        #region Publish Recording
        [HttpPost("publish")]
        public async Task<IActionResult> PublishRecording(string recordID, bool publish)
        {
            var result = await client.PublishRecordingsAsync(new PublishRecordingsRequest
            {
                recordID = recordID,
                publish = publish
            });

            if (result.returncode == Returncode.FAILED)
            {
                return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
                    "Failed to publish recording.",
                    result.message), "application/xml");
            }

            return Content(XmlHelper.ToXml(new PublishRecordingDto
            {
                RecordID = recordID,
                IsPublished = publish,
                Message = "Recording published successfully."
            }), "application/xml");
        }
        #endregion


        #region Delete Recording
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRecordings(string recordID)
        {
            var result = await client.DeleteRecordingsAsync(new DeleteRecordingsRequest
            {
                recordID = recordID
            });

            if (result.returncode == Returncode.FAILED)
            {
                return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
                    "Failed to delete recording.",
                    result.message), "application/xml");
            }

            return Content(XmlHelper.ToXml(new DeleteRecordingDto
            {
                RecordID = recordID,
                IsDeleted = true,
                Message = "Recording deleted successfully."
            }), "application/xml");
        }
        #endregion

        #region Update Recording Metadata
        [HttpPut("updateMetadata")]
        public async Task<IActionResult> UpdateRecordingMetadata(string recordID)
        {
            try
            {
                var request = new UpdateRecordingsRequest
                {
                    recordID = recordID,
                    meta = new MetaData { { "customdata", DateTime.Now.Ticks.ToString() } }
                };

                var result = await client.UpdateRecordingsAsync(request);

                if (result.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
                        "Failed to update metadata.",
                        result.message), "application/xml");
                }

                return Content(XmlHelper.ToXml(new UpdateRecordingMetadataDto
                {
                    RecordID = recordID,
                    IsUpdated = true,
                    Message = "Recording metadata updated successfully."
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
                    "An error occurred while updating recording metadata.",
                    ex.Message));
            }
        }
        #endregion

        #region Pause Recording
        [HttpPost("pause")]
        public async Task<IActionResult> PauseRecording(string meetingID, string moderatorPW)
        {
            try
            {
                var result = await client.PauseRecordingAsync(new PauseRecordingRequest
                {
                    meetingID = meetingID,
                    moderatorPW = moderatorPW
                });

                if (result.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
                        "Failed to pause recording.",
                        result.message), "application/xml");
                }

                return Content(XmlHelper.ToXml(new PauseRecordingDto
                {
                    MeetingID = meetingID,
                    IsPaused = true,
                    Message = "Recording paused successfully."
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
                    "An error occurred while pausing the recording.",
                    ex.Message));
            }
        }
        #endregion

        #region Resume Recording
        [HttpPost("resume")]
        public async Task<IActionResult> ResumeRecording(string meetingID, string moderatorPW)
        {
            try
            {
                var result = await client.ResumeRecordingAsync(new ResumeRecordingRequest
                {
                    meetingID = meetingID,
                    moderatorPW = moderatorPW
                });

                if (result.returncode == Returncode.FAILED)
                {
                    return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
                        "Failed to resume recording.",
                        result.message), "application/xml");
                }

                return Content(XmlHelper.ToXml(new ResumeRecordingDto
                {
                    MeetingID = meetingID,
                    IsResumed = true,
                    Message = "Recording resumed successfully."
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
                    "An error occurred while resuming the recording.",
                    ex.Message));
            }
        }
        #endregion


































        #region Get All Recordings
        //[HttpGet("all")]
        //public async Task<IActionResult> Index()
        //{
        //    try
        //    {
        //        var meetings = await client.GetMeetingsAsync();

        //        if (meetings == null || meetings.returncode == Returncode.FAILED)
        //        {
        //            return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
        //                "No meetings found.",
        //                "Meetings list is empty."), "application/xml");
        //        }

        //        return Content(XmlHelper.ToXml(meetings), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
        //            "An error occurred while retrieving meetings.",
        //            ex.Message), "application/xml");
        //    }
        //}
        #endregion

        #region Publish Recording
        //[HttpPost("publish")]
        //public async Task<IActionResult> PublishRecording(string recordID, bool publish)
        //{
        //    var result = await client.PublishRecordingsAsync(new PublishRecordingsRequest
        //    {
        //        recordID = recordID,
        //        publish = publish
        //    });

        //    if (result.returncode == Returncode.FAILED)
        //    {
        //        return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
        //            "Failed to publish recording.",
        //            result.message), "application/xml");
        //    }

        //    return Content(XmlHelper.ToXml(new PublishRecordingDto
        //    {
        //        RecordID = recordID,
        //        IsPublished = publish,
        //        Message = "Recording published successfully."
        //    }), "application/xml");
        //}
        #endregion


        #region Delete Recording
        //[HttpDelete("delete")]
        //public async Task<IActionResult> DeleteRecordings(string recordID)
        //{
        //    var result = await client.DeleteRecordingsAsync(new DeleteRecordingsRequest
        //    {
        //        recordID = recordID
        //    });

        //    if (result.returncode == Returncode.FAILED)
        //    {
        //        return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
        //            "Failed to delete recording.",
        //            result.message), "application/xml");
        //    }

        //    return Content(XmlHelper.ToXml(new DeleteRecordingDto
        //    {
        //        RecordID = recordID,
        //        IsDeleted = true,
        //        Message = "Recording deleted successfully."
        //    }), "application/xml");
        //}
        #endregion




        #region Update Recording Metadata
        //[HttpPut("updateMetadata")]
        //public async Task<IActionResult> UpdateRecordingMetadata(string recordID)
        //{
        //    try
        //    {
        //        var request = new UpdateRecordingsRequest
        //        {
        //            recordID = recordID,
        //            meta = new MetaData { { "customdata", DateTime.Now.Ticks.ToString() } }
        //        };

        //        var result = await client.UpdateRecordingsAsync(request);

        //        if (result.returncode == Returncode.FAILED)
        //        {
        //            return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
        //                "Failed to update metadata.",
        //                result.message), "application/xml");
        //        }

        //        return Content(XmlHelper.ToXml(new UpdateRecordingMetadataDto
        //        {
        //            RecordID = recordID,
        //            IsUpdated = true,
        //            Message = "Recording metadata updated successfully."
        //        }), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
        //            "An error occurred while updating recording metadata.",
        //            ex.Message));
        //    }
        //}
        #endregion


        #region Get Recording Text Tracks
        //[HttpGet("textTracks")]
        //public async Task<IActionResult> GetRecordingTextTracks(string recordID)
        //{
        //    try
        //    {
        //        var result = await client.GetRecordingTextTracksAsync(new GetRecordingTextTracksRequest
        //        {
        //            recordID = recordID
        //        });

        //        if (result == null || result.returncode == Returncode.FAILED)
        //        {
        //            return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
        //                "Failed to retrieve text tracks.",
        //                result?.message ?? "Unknown error"), "application/xml");
        //        }

        //        return Content(XmlHelper.ToXml(new RecordingTextTracksDto
        //        {
        //            RecordID = recordID,
        //            TextTracks = result.tracks,
        //            Message = "Recording text tracks retrieved successfully."
        //        }), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
        //            "An error occurred while retrieving text tracks.",
        //            ex.Message));
        //    }
        //}
        #endregion


        #region Pause Recording
        //[HttpPost("pause")]
        //public async Task<IActionResult> PauseRecording(string meetingID, string moderatorPW)
        //{
        //    try
        //    {
        //        var result = await client.PauseRecordingAsync(new PauseRecordingRequest
        //        {
        //            meetingID = meetingID,
        //            moderatorPW = moderatorPW
        //        });

        //        if (result.returncode == Returncode.FAILED)
        //        {
        //            return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
        //                "Failed to pause recording.",
        //                result.message), "application/xml");
        //        }

        //        return Content(XmlHelper.ToXml(new PauseRecordingDto
        //        {
        //            MeetingID = meetingID,
        //            IsPaused = true,
        //            Message = "Recording paused successfully."
        //        }), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
        //            "An error occurred while pausing the recording.",
        //            ex.Message));
        //    }
        //}
        #endregion


        #region Resume Recording
        //[HttpPost("resume")]
        //public async Task<IActionResult> ResumeRecording(string meetingID, string moderatorPW)
        //{
        //    try
        //    {
        //        var result = await client.ResumeRecordingAsync(new ResumeRecordingRequest
        //        {
        //            meetingID = meetingID,
        //            moderatorPW = moderatorPW
        //        });

        //        if (result.returncode == Returncode.FAILED)
        //        {
        //            return Content(XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
        //                "Failed to resume recording.",
        //                result.message), "application/xml");
        //        }

        //        return Content(XmlHelper.ToXml(new ResumeRecordingDto
        //        {
        //            MeetingID = meetingID,
        //            IsResumed = true,
        //            Message = "Recording resumed successfully."
        //        }), "application/xml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, XmlHelper.XmlErrorResponse<RecordingErrorResponseDto>(
        //            "An error occurred while resuming the recording.",
        //            ex.Message));
        //    }
        //}
        #endregion

    }
}
