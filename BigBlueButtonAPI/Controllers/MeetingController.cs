using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.Enums;
using BigBlueButton.Net.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BigBlueButtonAPI.Controllers
{
    public class MeetingController : Controller
    {
        private readonly BigBlueButtonAPIClient client;

        public MeetingController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }

        public async Task<ActionResult> Create()
        {
            var result = await client.CreateMeetingAsync(new CreateMeetingRequest
            {
                name = "Test Meeting",
                meetingID = "TestMeeting001",
                record = true
            });

            if (result.returncode == Returncode.FAILED)
                return View("Error", result);

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> End(string meetingID, string pass)
        {
            var result = await client.EndMeetingAsync(new EndMeetingRequest
            {
                meetingID = meetingID,
                password = pass
            });
            if (result.returncode == Returncode.FAILED) return View("Error", result);
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Join(string meetingID, string role, string pass, string token)
        {
            var requestJoin = new JoinMeetingRequest { meetingID = meetingID };
            if (role == "1")
            {
                requestJoin.password = pass;
                requestJoin.userID = "10000";
                requestJoin.fullName = "Admin";
            }
            else
            {
                requestJoin.password = pass;
                requestJoin.userID = "20000";
                requestJoin.fullName = "User";
            }
            if (token == "1")
            {
                var setConfigRequest = new SetConfigXMLRequest
                {
                    meetingID = meetingID,
                    configXML = "<config><modules><localeversion supressWarning=\"false\">0.9.0</localeversion></modules></config>"
                };
                var setConfigResult = await client.SetConfigXMLAsync(setConfigRequest);
                if (setConfigResult.returncode == Returncode.FAILED) return View("Error", setConfigResult);
                requestJoin.configToken = setConfigResult.configToken;
            }

            var url = client.GetJoinMeetingUrl(requestJoin);
            return Redirect(url);
        }
    }
}
