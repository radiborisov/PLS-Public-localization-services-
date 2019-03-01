using EFCodeFirst.Infrastructure.Data.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCodeFirst.Infrastructure.Data
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
                optionsBuilder.UseNpgsql("Server=localhost,5432;Database=PLSDbServer;User=postgres;Password=654321", s => s.MigrationsAssembly("EFCodeFirst.Infrastructure"));
            }
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}
