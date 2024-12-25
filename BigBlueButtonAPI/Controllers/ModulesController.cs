using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.DTOs.ModulesDto;
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
        public async Task<IActionResult> EnableModule(string meetingID, string moduleName, bool redirect = true)
        {
            if (string.IsNullOrEmpty(meetingID) || string.IsNullOrEmpty(moduleName))
            {
                return Content(XmlHelper.XmlErrorResponse<ModuleErrorResponseDto>(
                    "Meeting ID and Module Name cannot be null or empty.",
                    "Invalid input."), "application/xml");
            }

            try
            {
                var result = await client.EnableModuleAsync(new EnableModuleRequest
                {
                    meetingID = meetingID,
                    moduleName = moduleName,
                    redirect = redirect
                });

                if (result.returncode == "FAILED")
                {
                    return Content(XmlHelper.XmlErrorResponse<ModuleErrorResponseDto>(
                        "Failed to enable module.",
                        result.message), "application/xml");
                }

                return Content(XmlHelper.ToXml(new EnableModuleDto
                {
                    MeetingID = meetingID,
                    ModuleName = moduleName,
                    IsEnabled = true,
                    Redirect = redirect,
                    Message = "Module enabled successfully."
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<ModuleErrorResponseDto>(
                    "An error occurred while enabling the module.",
                    ex.Message));
            }
        }

        #endregion





        #region Disable Module
        [HttpPost("disable")]
        public async Task<IActionResult> DisableModule(string meetingID, string moduleName, bool redirect = true)
        {
            if (string.IsNullOrEmpty(meetingID) || string.IsNullOrEmpty(moduleName))
            {
                return Content(XmlHelper.XmlErrorResponse<ModuleErrorResponseDto>(
                    "Meeting ID and Module Name cannot be null or empty.",
                    "Invalid input."), "application/xml");
            }

            try
            {
                var result = await client.DisableModuleAsync(new DisableModuleRequest
                {
                    meetingID = meetingID,
                    moduleName = moduleName,
                    redirect = redirect
                });

                if (result.returncode == "FAILED")
                {
                    return Content(XmlHelper.XmlErrorResponse<ModuleErrorResponseDto>(
                        "Failed to disable module.",
                        result.message), "application/xml");
                }

                return Content(XmlHelper.ToXml(new DisableModuleDto
                {
                    MeetingID = meetingID,
                    ModuleName = moduleName,
                    IsDisabled = true,
                    Redirect = redirect,
                    Message = "Module disabled successfully."
                }), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, XmlHelper.XmlErrorResponse<ModuleErrorResponseDto>(
                    "An error occurred while disabling the module.",
                    ex.Message));
            }
        }

        #endregion



    }
}
