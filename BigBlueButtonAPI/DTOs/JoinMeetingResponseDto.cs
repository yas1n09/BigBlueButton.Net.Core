namespace BigBlueButtonAPI.DTOs
{
    public class JoinMeetingResponseDto
    {
        public JoinMeetingResponseDto() { }
        public string JoinUrl { get; set; }
        public bool Redirect { get; set; }
        public string Message { get; set; }
    }
}
