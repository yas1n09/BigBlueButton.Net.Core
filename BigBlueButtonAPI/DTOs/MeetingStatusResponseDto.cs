namespace BigBlueButtonAPI.DTOs
{
    public class MeetingStatusResponseDto
    {
        public MeetingStatusResponseDto() { }
        public string MeetingID { get; set; }
        public bool IsRunning { get; set; }
        public string Message { get; set; }
    }
}
