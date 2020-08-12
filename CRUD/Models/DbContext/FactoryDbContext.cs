using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Models.FactoryDbContext
{
    public class FactoryDbContext : DbContext
    {
        public FactoryDbContext(DbContextOptions options) : base(options)
        {
        }

        public FactoryDbContext()
        {
        }

        public DbSet<Factory> Factory { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Tank> Tank { get; set; }
    }
}
