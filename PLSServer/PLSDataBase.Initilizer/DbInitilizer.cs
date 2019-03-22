using PLSDataBase.Initilizer.Generators;
using PLSDataBase.Models;
using System;

namespace PLSDataBase.Initilizer
{
    public static class DbInitilizer
    {
        public static string ResetDataBase(PLSDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Seed(context);

            return "PLS database was created sucessfuly";
        }

        private static void Seed(PLSDBContext context)
        {
            User[] users = UserGenerator.CreateUsers();
            Location[] locations = LocationGenerator.CreateLocations();

            context.AddRange(users);
            context.SaveChanges();
            context.AddRange(locations);
            context.SaveChanges();

        }
    }
}
