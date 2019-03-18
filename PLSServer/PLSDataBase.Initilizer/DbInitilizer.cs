using PLSDataBase.Initilizer.Generators;
using PLSDataBase.Models;
using System;

namespace PLSDataBase.Initilizer
{
    public class DbInitilizer
    {
        public static void ResetDataBase(PLSDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Console.WriteLine("BookShop database created successfully.");

            Seed(context);
        }

        private static void Seed(PLSDBContext context)
        {
            User[] users = UserGenerator.CreateUsers();
            Location[] locations = LocationGenerator.CreateLocations();

            context.AddRange(users);
            context.AddRange(locations);

            context.SaveChanges();
        }
    }
}
