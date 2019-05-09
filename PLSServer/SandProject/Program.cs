using Nexmo.Api;
using PLSDataBase;
using PLSDataBase.Initilizer;
using PLSDesktopAuthanticationDB;
using PLSDesktopAuthanticationDB.Models;
using PLSMobileAuthanticationDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SandProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (PLSDBContext context = new PLSDBContext())
            //{
            //    Console.WriteLine(DbInitilizer.ResetDataBase(context));

            //}

            //using (PLSMobileAuthanticationDBContext dBContext = new PLSMobileAuthanticationDBContext())
            //{
            //    dBContext.Database.EnsureDeleted();
            //    dBContext.Database.EnsureCreated();
            //    //string test = "09a7259f-31b7-492a-9146-7c36a53f8a2a";

            //    //if (dBContext.MobileUserRegisterQueues.FirstOrDefault(x => x.PhoneNumber == "0865422341").SecretKey.ToString() == test)
            //    //{
            //    //    Console.WriteLine("ok");
            //    //}
            //    //else
            //    //{
            //    //    Console.WriteLine("not ok");
            //    //}
            //}

            using (PLSDesktopAuthanticationDBContext authanticationDBContext = new PLSDesktopAuthanticationDBContext())
            {
                //authanticationDBContext.Database.EnsureDeleted();
                //authanticationDBContext.Database.EnsureCreated();
                var user = new PLSDesktopUser
                {
                    Username = "admin",
                    Password = "RedCross32"
                };

                authanticationDBContext.PLSDesktopUsers.Add(user);
                authanticationDBContext.SaveChanges();
            }

        }

    }
}
