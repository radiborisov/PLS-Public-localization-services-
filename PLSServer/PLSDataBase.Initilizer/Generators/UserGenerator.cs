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
                "0864524343",
                "0835325321",
                "0875312345"
            };

            User[] users = new User[stringUsers.Length];

            for (int i = 0; i < users.Length; i++)
            {
                User newUser = new User
                {
                    PhoneNumber = stringUsers[i],
                    IsSavior = true,
                    IsOnline = true
                };
                users[i] = newUser;
            }

            return users;
        }
    }
}
