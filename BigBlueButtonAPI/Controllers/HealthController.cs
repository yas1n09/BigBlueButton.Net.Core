using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.Enums;
using BigBlueButton.Net.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BigBlueButtonAPI.Controllers
{
    
    public class HealthController : Controller
    {
        private readonly BigBlueButtonAPIClient client;

        public HealthController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }

        private async Task<bool> IsBigBlueButtonAPISettingsOKAsync()
        {
            try
            {
                var res = await client.IsMeetingRunningAsync(new IsMeetingRunningRequest { meetingID = Guid.NewGuid().ToString() });
                if (res.returncode == Returncode.FAILED) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ActionResult> CheckAPIHealth()
        {
            var setupOk = await IsBigBlueButtonAPISettingsOKAsync();
            return Json(new { status = setupOk ? "OK" : "Error" });
        }

    }
}
