using BigBlueButton.Net.Core.Responses;

namespace BigBlueButtonAPI.DTOs
{
    public class AllMeetingsResponseDto
    {
        public AllMeetingsResponseDto() { }
        public string Message { get; set; }
        public GetMeetingsResponse Result { get; set; }
    }
}
