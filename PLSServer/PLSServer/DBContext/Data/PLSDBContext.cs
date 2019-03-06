using Microsoft.EntityFrameworkCore;
using PLSServer.DBContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLSServer.DBContext.Data
{
    public class PLSDBContext : DbContext
    {
        protected PLSDBContext()
        {
        }

        public PLSDBContext(DbContextOptions<PLSDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=PLSDbServer;User Id=postgres;Password=654321;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUserEntity(modelBuilder);
            ConfigureLocationEntity(modelBuilder);
        }

        private void ConfigureLocationEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Location>()
                .HasKey(k => k.Id);

            modelBuilder
                .Entity<Location>()
                .HasOne(u => u.User)
                .WithMany(l => l.Locations)
                .HasForeignKey(f => f.UserId);
        }

        private void ConfigureUserEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasKey(k => k.Id);

            modelBuilder
                .Entity<User>()
                .HasMany(l => l.Locations)
                .WithOne(u => u.User);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}
