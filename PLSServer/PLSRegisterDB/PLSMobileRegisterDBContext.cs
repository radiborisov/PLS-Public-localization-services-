using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using PLSMobileRegisterDB.Configurations;
using PLSRegisterDB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLSMobileRegisterDB
{
    public class PLSMobileRegisterDBContext : DbContext
    {
        public PLSMobileRegisterDBContext(DbContextOptions options) : base(options)
        {
        }

        protected PLSMobileRegisterDBContext()
        {
        }

        public DbSet<MobileUserRegisterQueue> MobileUserRegisterQueues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:plsdbserver.database.windows.net,1433;Initial Catalog=PLSMobileRegisterDB" +
                ";Persist Security Info=False;User ID=sqladmin;Password=Pls@dmin32" +
                ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegisterConfig());
        }
    }
}
