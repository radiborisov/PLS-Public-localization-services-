using PLSDataBase;
using PLSDataBase.Initilizer;
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
            using (PLSDBContext context = new PLSDBContext())
            {
                //var user = context.Users.FirstOrDefault(i => i.Id == 17);

                //context.Users.Remove(user);
                //context.SaveChanges();
                //Console.WriteLine("Is removed");
                //Console.WriteLine(DbInitilizer.ResetDataBase(context));
                var date = context.Locations.ToList();

                foreach (var item in date)
                {
                    Console.WriteLine(item.Date);
                }
            }

        }

    }
}
