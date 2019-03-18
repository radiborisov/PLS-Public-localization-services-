using Microsoft.EntityFrameworkCore;
using PLSDataBase.Configurations;
using PLSDataBase.Models;
using System;

namespace PLSDataBase
{
    public class PLSDBContext : DbContext
    {

        protected PLSDBContext()
        {
        }

        public PLSDBContext(DbContextOptions<PLSDBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
        }



    }
}
