using TvMazeApi.Repository.Data;
using Microsoft.EntityFrameworkCore;
using TvMazeApi.Repository.Models;

namespace TvMazeApi.Repository.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly ScraperDbContext _dbContext;

        public EpisodeRepository(ScraperDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        /// <summary>
        /// Checks if an Episode exists in the Database before adding it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Exists(int id)
        {
            return await _dbContext.Episodes.FirstOrDefaultAsync(e => e.Id == id) != null;
        }
        /// <summary>
        /// Adds an Episode to the Database.
        /// </summary>
        /// <param name="episodes"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddEpisodes(Episode episodes)
        {
            if (episodes == null)
            {
                throw new ArgumentNullException(nameof(episodes));
            }
            _dbContext.Add(episodes);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Gets an Episode from the Database.
        /// </summary>
        /// <param name="showId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<Episode>> GetEpisodes(int showId)
        {
            if (showId == null)
            {
                throw new ArgumentNullException(nameof(showId));
            }
            return await _dbContext.Episodes.Where(s => s.ShowId == showId).ToListAsync();
        }
    }
}
