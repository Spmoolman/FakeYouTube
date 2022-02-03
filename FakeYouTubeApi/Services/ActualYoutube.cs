using Google.Apis.Services;
using Google.Apis.YouTube.v3;


namespace FakeYouTubeApi.Services;
public class ActualYoutube: IActualYoutube
{
    private readonly IConfiguration Configuration;
    public ActualYoutube(IServiceProvider services)
    {
        Configuration = services.GetService<IConfiguration>();
    }
    public async Task<List<string>> Search(string search)
    {
        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        {
            ApiKey = Configuration["ServiceDetails"],
            ApplicationName = this.GetType().ToString()
        });

        var searchListRequest = youtubeService.Search.List("snippet");
        searchListRequest.Q = search; 
        searchListRequest.MaxResults = 50;

        // Call the search.list method to retrieve results matching the specified query term.
        var searchListResponse = await searchListRequest.ExecuteAsync();

        List<string> videos = new List<string>();
        List<string> channels = new List<string>();
        List<string> playlists = new List<string>();

        // Add each result to the appropriate list, and then display the lists of
        // matching videos, channels, and playlists.
        foreach (var searchResult in searchListResponse.Items)
        {
            switch (searchResult.Id.Kind)
            {
                case "youtube#video":
                    videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                    break;

                case "youtube#channel":
                    channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                    break;

                case "youtube#playlist":
                    playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                    break;
            }
        }

        return videos;

        // Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
        // Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
        // Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));
    }
}

public class ApiConfig
{
    public string Key { get; set; }
}