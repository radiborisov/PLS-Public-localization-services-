using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using PLSMobileAuthanticationDB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLSMobileAuthanticationDB
{
    public class PLSMobileAuthanticationDBContext : DbContext
    {
        public PLSMobileAuthanticationDBContext(DbContextOptions<PLSMobileAuthanticationDBContext> options) : base(options)
        {
        }

        protected PLSMobileAuthanticationDBContext()
        {
        }

        public DbSet<MobileAuthanticationQueue> MobileAuthanticationQueues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:plsdbserver.database.windows.net,1433;Initial Catalog=PLSMobileAuthanticationDB" +
               ";Persist Security Info=False;User ID=sqladmin;Password=Pls@dmin32" +
               ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
