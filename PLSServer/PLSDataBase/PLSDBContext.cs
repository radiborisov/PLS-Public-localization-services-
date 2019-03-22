using Microsoft.EntityFrameworkCore;
using PLSDataBase.Configurations;
using PLSDataBase.Models;
using System;

namespace PLSDataBase
{
    public class PLSDBContext : DbContext
    {

        public PLSDBContext()
        {
        }

        public PLSDBContext(DbContextOptions<PLSDBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=pls-db.postgres.database.azure.com;Database=PLSDBServer;Port=5432;User Id=plsadmin@pls-db;Password=Pls@dmin32;Ssl Mode=Require;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}
