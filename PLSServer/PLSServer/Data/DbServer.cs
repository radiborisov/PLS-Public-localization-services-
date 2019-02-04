using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLSServer.Data
{
    public class DbServer : DbContext
    {
        public DbSet<Location> Location { get; set; }
        public DbSet<Users> Users { get; set; }

        public DbServer(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=PLSDbServer;Integrated Security=true");

            base.OnConfiguring(optionsBuilder);
        }

        //public static void SeedData(DbServer context)
        //{
        //    context.Locations.Add(new Users
        //    {
        //        UserName = "Pesho",
        //        Age = 12
        //    });
        //    context.Locations.Add(new Users
        //    {
        //        UserName = "Georgi",
        //        Age = 20
        //    });
        //    context.SaveChanges();
        //}
    }
}
