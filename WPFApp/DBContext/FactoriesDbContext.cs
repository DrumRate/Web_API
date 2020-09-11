using Microsoft.EntityFrameworkCore;
using WPFApp.Models;

namespace WPFApp.DBContext
{
    public class FactoriesDbContext : DbContext
    {
        public FactoriesDbContext(DbContextOptions<FactoriesDbContext> dbOptions) : base(dbOptions)
        {
        }
        public virtual DbSet<Event> Events { get; set; }
    }
}
