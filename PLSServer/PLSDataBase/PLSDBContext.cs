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
            optionsBuilder.UseSqlServer("Server=tcp:pls-databaseserver.database.windows.net,1433;Initial Catalog=PLSDatabase;Persist Security Info=False;User ID=sqladmin;Password=Pls@dmin32;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}
