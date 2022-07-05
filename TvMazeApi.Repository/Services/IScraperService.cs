namespace TvMazeApi.Repository.Services
{
    public interface IScraperService
    {
        Task Scrape(string search);
    }
}
