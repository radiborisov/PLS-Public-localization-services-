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

        public DbSet<User> Users { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}
