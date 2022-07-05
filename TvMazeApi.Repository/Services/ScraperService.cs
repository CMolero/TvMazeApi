using Newtonsoft.Json;
using TvMazeApi.Repository.Models;
using TvMazeApi.Repository.Repositories;

namespace TvMazeApi.Repository.Services
{
    public class ScraperService : IScraperService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IShowRepository _showRepository;
        private readonly IEpisodeRepository _episodeRepository;

        public ScraperService(IHttpClientFactory httpClientFactory, IShowRepository showRepository, IEpisodeRepository episodeRepository)
        {
            _httpClientFactory = httpClientFactory;
            _showRepository = showRepository;
            _episodeRepository = episodeRepository;
        }

        public async Task Scrape(string search)
        {
            var apiClient = _httpClientFactory.CreateClient("tvmaze");
            var showList = JsonConvert.DeserializeObject<IEnumerable<ShowSearchResult>>(await ApiCall(apiClient, $"search/shows?q={search}"));

            
            foreach (var show in showList)
            {
                var storedShow = await _showRepository.Exists(show.Show.Id);

                if (!storedShow)
                {
                    var episodes = JsonConvert.DeserializeObject<IEnumerable<Episode>>(await ApiCall(apiClient, $"/shows/{show.Show.Id}/episodes"));
                    var newShow = new Show()
                    {
                        Id = show.Show.Id,
                        Name = show.Show.Name,
                        Summary = show.Show.Summary
                    };
                    await _showRepository.AddShow(newShow);
                    foreach (var ep in episodes)
                    {
                        var episodeExists = await _episodeRepository.Exists(ep.Id);
                        var newShowEpisodes = new Episode()
                        {
                            ShowId = show.Show.Id,
                            Id = ep.Id,
                            Name = ep.Name,
                            Number = ep.Number,
                            Season = ep.Season,
                            Summary = ep.Summary
                        };
                        await _episodeRepository.AddEpisodes(newShowEpisodes);
                    }
                }
            }
        }
        private static async Task<string> ApiCall(HttpClient apiClient, string route)
        {
            var response = await apiClient.GetAsync(route);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
