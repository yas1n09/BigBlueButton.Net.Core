using BigBlueButton.Net.Core.Responses;

namespace BigBlueButtonAPI.DTOs
{
    public class MeetingInfoResponseDto
    {
        public MeetingInfoResponseDto() { }
        public string Message { get; set; }
        public GetMeetingInfoResponse Result { get; set; }
    }
}
