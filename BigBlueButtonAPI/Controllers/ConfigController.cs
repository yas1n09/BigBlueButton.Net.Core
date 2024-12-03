using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BigBlueButtonAPI.Controllers
{
    public class ConfigController : Controller
    {
        private readonly BigBlueButtonAPIClient client;

        public ConfigController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }

        public async Task<ActionResult> GetDefaultConfigXML()
        {
            var result = await client.GetDefaultConfigXMLAsync();
            return Content(result, "text/xml");
        }

        public async Task<ActionResult> SetConfigXML(string meetingID)
        {
            var setConfigRequest = new SetConfigXMLRequest
            {
                meetingID = meetingID,
                configXML = "<config><modules><localeversion supressWarning=\"false\">0.9.0</localeversion></modules></config>"
            };
            var result = await client.SetConfigXMLAsync(setConfigRequest);
            return Json(result);
        }

    }
}
