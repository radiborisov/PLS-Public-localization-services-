using PLSDataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLSDataBase.Initilizer.Generators
{
    public class UserGenerator
    {
        internal static User[] CreateUsers()
        {
            string[] stringUsers = new string[]
            {
                "0888014990",
                "0887512119",
                "0875312345"
            };

            User[] users = new User[stringUsers.Length];

            for (int i = 0; i < users.Length; i++)
            {
                users[i].PhoneNumber = stringUsers[i];
            }

            return users;
        }
    }
}
