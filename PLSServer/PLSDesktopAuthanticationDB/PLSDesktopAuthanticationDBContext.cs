using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using PLSDesktopAuthanticationDB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLSDesktopAuthanticationDB
{
    public class PLSDesktopAuthanticationDBContext : DbContext
    {
        public PLSDesktopAuthanticationDBContext(DbContextOptions options) : base(options)
        {
        }

        public PLSDesktopAuthanticationDBContext()
        {
        }

        public DbSet<PLSDesktopUser> PLSDesktopUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:plsdbserver.database.windows.net,1433;Initial Catalog=PLSDesktopAuthanticationDB" +
               ";Persist Security Info=False;User ID=sqladmin;Password=Pls@dmin32" +
               ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PLSDesktopUser>(x =>
                x.HasIndex(u => u.Username)
                .IsUnique());
        }
    }
}
