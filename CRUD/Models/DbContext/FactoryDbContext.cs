using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Factory, Unit, Tank
            modelBuilder.Entity<Factory>().HasData(
                new Factory { Id = 1, Name = "МНПЗ", Description = "Московский нефтеперерабатывающий завод" },
                new Factory { Id = 2, Name = "ОНПЗ", Description = "Омский нефтеперерабатывающий завод" }
            );

            modelBuilder.Entity<Unit>().HasData(
                new Unit { Id = 1, Name = "ГФУ-1", FactoryId = 1 },
                new Unit { Id = 2, Name = "ГФУ-2", FactoryId = 1 },
                new Unit { Id = 3, Name = "АВТ-6", FactoryId = 2 }
            );

            modelBuilder.Entity<Tank>().HasData(
                new Tank { Id = 1, Name = "Резервуар 1", Volume = 1500, MaxVolume = 2000, UnitId = 1 },
                new Tank { Id = 2, Name = "Резервуар 2", Volume = 2500, MaxVolume = 3000, UnitId = 1 },
                new Tank { Id = 3, Name = "Дополнительный резервуар 24", Volume = 3000, MaxVolume = 3000, UnitId = 2 },
                new Tank { Id = 4, Name = "Резервуар 35", Volume = 3000, MaxVolume = 3000, UnitId = 2 },
                new Tank { Id = 5, Name = "Резервуар 47", Volume = 4000, MaxVolume = 5000, UnitId = 2 },
                new Tank { Id = 6, Name = "Резервуар 256", Volume = 500, MaxVolume = 500, UnitId = 3 }
            );

            byte[] pwdHash;
            using (var hasher = new System.Security.Cryptography.SHA512Managed())
            {
                pwdHash = hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Test"));
            }

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = pwdHash,
                    RoleId = 1
                });

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "admin" },
                new Role { Id = 2, Name = "user" }
            );

            modelBuilder.Entity<Event>().Property(i => i.Tags).HasConversion(
                i => string.Join(';', i),
                i => i.Split(';', StringSplitOptions.RemoveEmptyEntries)
            );

            modelBuilder.Entity<Event>().Property(i => i.ResponsibleOperators).HasConversion(
                i => JsonConvert.SerializeObject(i),
                i => JsonConvert.DeserializeObject<IEnumerable<UnitOperator>>(i)
            );

        }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Tank> Tanks { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
