using Microsoft.EntityFrameworkCore;

namespace ODataRepro
{
    public class AppDbContext : DbContext
    {
        public DbSet<Entity> Entities => Set<Entity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseInMemoryDatabase(nameof(ODataRepro));
        }
    }
}