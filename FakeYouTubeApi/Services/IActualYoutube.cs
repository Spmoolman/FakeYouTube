namespace FakeYouTubeApi.Services;

public interface IActualYoutube
{
    Task<List<string>> Search(string search);
}