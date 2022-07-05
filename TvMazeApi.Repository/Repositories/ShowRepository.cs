using Microsoft.EntityFrameworkCore;
using TvMazeApi.Repository.Data;
using TvMazeApi.Repository.Models;
using TvMazeApi.Repository.Services;

namespace TvMazeApi.Repository.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly ScraperDbContext _dbContext;

        public ShowRepository(ScraperDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        /// <summary>
        /// Add a show to the database.
        /// </summary>
        /// <param name="show"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddShow(Show show)
        {
            if(show == null)
            {
                throw new ArgumentNullException(nameof(show));
            }
            _dbContext.Add(show);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Get a show result after calling the API
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<Show>> GetShows(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                throw new ArgumentNullException(nameof(search));
            }
            return await _dbContext.Shows.Where(s => s.Name.Contains(search)).ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.Shows.FirstOrDefaultAsync(e => e.Id == id) != null;
        }
    }
}
