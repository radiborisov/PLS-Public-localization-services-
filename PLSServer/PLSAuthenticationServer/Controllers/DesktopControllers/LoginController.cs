using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLSDesktopAuthanticationDB;

namespace PLSAuthenticationServer.Controllers.DesktopControllers
{
    [Route("/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private PLSDesktopAuthanticationDBContext context;

        public LoginController(PLSDesktopAuthanticationDBContext context)
        {
            this.context = context;
        }
        // GET: api/Login
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        [HttpGet("{username}/{password}", Name = "Get")]
        public ActionResult<string> Get(string username, string password)
        {
            if (!this.context.PLSDesktopUsers.Any(u => u.Username == username))
            {
                return StatusCode(404);
            }
            else
            {
                
                var checkUser = this.context.PLSDesktopUsers.FirstOrDefault(u => u.Username == username);

                if (checkUser.LockoutEnabled)
                {
                    return "User is locked wait 5 minutes";
                }
                if (checkUser.AccessFailedCount == 5)
                {
                    checkUser.LockoutEnabled = true;
                    checkUser.LockoutEnd = DateTime.Now.AddMinutes(5);
                    this.context.PLSDesktopUsers.Update(checkUser);
                    this.context.SaveChanges();
                    return "User is locked wait 5 minutes";
                }

                if (this.context.PLSDesktopUsers.Any(u => u.Username == username && u.Password != password))
                {
                    this.context.PLSDesktopUsers.FirstOrDefault(u => u.Username == username).AccessFailedCount += 1;
                    this.context.SaveChanges();
                }   
            }

            var user = context.PLSDesktopUsers.FirstOrDefault(u => u.Username == username && u.Password == password);
            user.SecretKey = Guid.NewGuid();
            this.context.PLSDesktopUsers.Update(user);
            this.context.SaveChanges();

           return user.SecretKey.ToString();
        }
    }
}
