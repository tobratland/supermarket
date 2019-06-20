using Microsoft.EntityFrameworkCore;
using SprayNpray.api.Entities.Models;

namespace SprayNpray.api.Entities
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Score> Scores { get; set; }

    }
}
