using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.Entities;
using BigBlueButton.Net.Core.Enums;
using BigBlueButton.Net.Core.Helpers;
using BigBlueButton.Net.Core.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BigBlueButtonAPI.Controllers
{
    public class RecordingController : Controller
    {
        private readonly BigBlueButtonAPIClient client;

        public RecordingController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }

        public async Task<ActionResult> Recordings()
        {
            var result = await client.GetRecordingsAsync();
            return View(result);
        }

        public async Task<ActionResult> PublishRecordings(string recordID, string type)
        {
            var request = new PublishRecordingsRequest
            {
                recordID = recordID,
                publish = type == "1"
            };
            var result = await client.PublishRecordingsAsync(request);

            if (result.returncode == Returncode.FAILED) return View("Error", result);
            return RedirectToAction("Recordings");
        }

        public async Task<ActionResult> DeleteRecordings(string recordID)
        {
            var request = new DeleteRecordingsRequest
            {
                recordID = recordID
            };
            var result = await client.DeleteRecordingsAsync(request);

            if (result.returncode == Returncode.FAILED) return View("Error", result);
            return RedirectToAction("Recordings");
        }

        public async Task<ActionResult> UpdateRecordings(string recordID)
        {
            var request = new UpdateRecordingsRequest
            {
                recordID = recordID,
                meta = new MetaData { { "customdata", DateTime.Now.Ticks.ToString() } }
            };
            var result = await client.UpdateRecordingsAsync(request);

            if (result.returncode == Returncode.FAILED) return View("Error", result);
            return RedirectToAction("Recordings");
        }

        public async Task<ActionResult> Tracks(string recordID)
        {
            var result = await client.GetRecordingTextTracksAsync(new GetRecordingTextTracksRequest { recordID = recordID });
            return Json(result);
        }

        public async Task<ActionResult> PutTrack(string recordID)
        {
            var request = new PutRecordingTextTrackRequest
            {
                recordID = recordID,
                kind = "subtitles",
                label = "English",
                lang = "en"
            };

            var webVTT = "WEBVTT - Some title\n\n00:00.000 --> 00:04.000\nHello\n\n00:04.000 --> 00:08.000\nWorld\n\n\n";
            var fileData = Encoding.UTF8.GetBytes(webVTT);

            request.file = new FileContentData
            {
                Name = "file",
                FileName = "a.vtt",
                FileData = fileData
            };
            var result = await client.PutRecordingTextTrackAsync(request);
            return Json(result);
        }

    }
}
