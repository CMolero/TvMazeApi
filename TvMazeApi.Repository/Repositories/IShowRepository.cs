using TvMazeApi.Repository.Models;

namespace TvMazeApi.Repository.Repositories
{
    public interface IShowRepository
    {
        Task<IEnumerable<Show>> GetShows(string search);
        Task AddShow(Show show);
        public Task<bool> Exists(int id);
    }
}
