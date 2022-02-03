using FakeYouTubeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeYouTubeApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ActualYouTubeController : ControllerBase
{
    private IActualYoutube ActualYoutubeService;

    public ActualYouTubeController(IServiceProvider services) => ActualYoutubeService = services.GetService<IActualYoutube>();

    [HttpGet(Name = "Search")]
    public IEnumerable<string> Search(string search) => ActualYoutubeService.Search(search).Result;
}
