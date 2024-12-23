namespace BigBlueButtonAPI.DTOs
{
    public class MeetingResponseDto
    {
        public MeetingResponseDto() { }
        public string Message { get; set; }
        public BigBlueButton.Net.Core.Responses.CreateMeetingResponse Result { get; set; }
    }
}
