using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.Helpers;
using BigBlueButton.Net.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BigBlueButtonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly BigBlueButtonAPIClient client;

        public ConfigController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }

        #region Get Default Config XML
        [HttpGet("getDefaultConfig")]
        public async Task<IActionResult> GetDefaultConfigXML()
        {
            try
            {
                var result = await client.GetDefaultConfigXMLAsync();

                if (string.IsNullOrEmpty(result))
                {
                    return Content(XmlHelper.XmlErrorResponse("Default configuration could not be retrieved.", "Config XML is empty."), "application/xml");
                }

                return Content(result, "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new
                {
                    message = "An error occurred while retrieving the default configuration.",
                    details = ex.Message
                }));
            }
        }
        #endregion

        #region Set Config XML
        [HttpPost("setConfig")]
        public async Task<IActionResult> SetConfigXML(string meetingID)
        {
            if (string.IsNullOrEmpty(meetingID))
            {
                return Content(XmlHelper.XmlErrorResponse("Meeting ID cannot be null or empty.", "Invalid request."), "application/xml");
            }

            try
            {
                var setConfigRequest = new SetConfigXMLRequest
                {
                    meetingID = meetingID,
                    configXML = "<config><modules><localeversion supressWarning=\"false\">0.9.0</localeversion></modules></config>"
                };

                var result = await client.SetConfigXMLAsync(setConfigRequest);

                if (result.returncode.ToString() == "FAILED")
                {
                    return Content(XmlHelper.XmlErrorResponse("Failed to set configuration.", result.message), "application/xml");
                }

                var successResponse = new
                {
                    message = "Configuration set successfully.",
                    result
                };

                return Content(XmlHelper.ToXml(successResponse), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new
                {
                    message = "An error occurred while setting the configuration.",
                    details = ex.Message
                }));
            }
        }
        #endregion
    }
}
