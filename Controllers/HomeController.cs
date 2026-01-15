using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SSECheck.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [HttpGet("events")]
        public async Task Events(CancellationToken ct)
        {
            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("Connection", "keep-alive");
            Response.Headers.Add("X-Accel-Buffering", "no");

            while (!ct.IsCancellationRequested)
            {
                await Response.WriteAsync($"data: ping {DateTime.Now}\n\n", ct);
                await Response.Body.FlushAsync(ct);

                await Task.Delay(5000, ct);
            }
        }
        [HttpGet("test")]
        public async Task<string> Test(CancellationToken ct)
        {
            return await Task.FromResult("Worked");
        }
    }
}
