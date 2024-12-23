using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.Enums;
using BigBlueButton.Net.Core.Helpers;
using BigBlueButton.Net.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BigBlueButtonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModulesController : ControllerBase
    {
        private readonly BigBlueButtonAPIClient client;

        public ModulesController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }

        #region Enable Module
        [HttpPost("enable")]
        public async Task<IActionResult> EnableModule(string meetingID, string moduleName)
        {
            if (string.IsNullOrEmpty(meetingID) || string.IsNullOrEmpty(moduleName))
            {
                return Content(XmlHelper.XmlErrorResponse("Meeting ID and Module Name cannot be null or empty.", "Invalid input."), "application/xml");
            }

            try
            {
                var result = await client.EnableModuleAsync(new EnableModuleRequest
                {
                    meetingID = meetingID,
                    moduleName = moduleName
                });

                if (result.returncode == "FAILED")
                {
                    return Content(XmlHelper.XmlErrorResponse("Failed to enable module.", result.message), "application/xml");
                }

                var successResponse = new
                {
                    message = "Module enabled successfully.",
                    result
                };

                return Content(XmlHelper.ToXml(successResponse), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new
                {
                    message = "An error occurred while enabling the module.",
                    details = ex.Message
                }));
            }
        }
        #endregion

        #region Disable Module
        [HttpPost("disable")]
        public async Task<IActionResult> DisableModule(string meetingID, string moduleName)
        {
            if (string.IsNullOrEmpty(meetingID) || string.IsNullOrEmpty(moduleName))
            {
                return Content(XmlHelper.XmlErrorResponse("Meeting ID and Module Name cannot be null or empty.", "Invalid input."), "application/xml");
            }

            try
            {
                var result = await client.DisableModuleAsync(new DisableModuleRequest
                {
                    meetingID = meetingID,
                    moduleName = moduleName
                });

                if (result.returncode == "FAILED")
                {
                    return Content(XmlHelper.XmlErrorResponse("Failed to disable module.", result.message), "application/xml");
                }

                var successResponse = new
                {
                    message = "Module disabled successfully.",
                    result
                };

                return Content(XmlHelper.ToXml(successResponse), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.ToXml(new
                {
                    message = "An error occurred while disabling the module.",
                    details = ex.Message
                }));
            }
        }
        #endregion
    }
}
