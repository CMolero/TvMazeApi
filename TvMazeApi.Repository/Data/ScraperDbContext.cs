using Microsoft.EntityFrameworkCore;
using TvMazeApi.Repository.Models;

namespace TvMazeApi.Repository.Data
{
    public class ScraperDbContext : DbContext
    {
        public ScraperDbContext(DbContextOptions<ScraperDbContext> options) : base(options)
        {

        }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Episode> Episodes { get; set; }
    }
}
