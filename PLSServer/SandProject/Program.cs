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
            using(PLSDBContext context = new PLSDBContext())
            Console.WriteLine(DbInitilizer.ResetDataBase(context));        
        }

    }
}
