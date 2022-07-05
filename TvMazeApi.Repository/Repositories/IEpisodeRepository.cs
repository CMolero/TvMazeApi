using TvMazeApi.Repository.Models;

namespace TvMazeApi.Repository.Repositories
{
    public interface IEpisodeRepository
    {
        Task<bool> Exists(int id);
        Task AddEpisodes(Episode episodes);
        Task<IEnumerable<Episode>> GetEpisodes(int showId);
    }
}
