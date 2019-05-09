using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLSAuthenticationServer.Models.UserModels;
using PLSDataBase;
using PLSDataBase.Models;
using PLSMobileAuthanticationDB;

namespace PLSAuthenticationServer.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MobileLoginController : ControllerBase
    {
        private PLSMobileAuthanticationDBContext authanticationContext;
        private PLSDBContext plsDBContext;
        private readonly IMapper mapper;

        public MobileLoginController(PLSMobileAuthanticationDBContext registerContext, IMapper mapper, PLSDBContext plsDBContext)
        {
            this.authanticationContext = registerContext;
            this.plsDBContext = plsDBContext;
            this.mapper = mapper;
        }

        // GET: api/MobileLogin
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{phoneNumber}/{token}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> Get(string phoneNumber, string token)
        {
            bool IsUserValid = this.plsDBContext.Users.Any(x => x.PhoneNumber == phoneNumber && x.Token.ToString() == token);

            if (!IsUserValid)
            {
                return StatusCode(404);
            }

            return IsUserValid;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<string> Post(LoginUserDto userInfo)
        {
            bool IsUserValid = this.authanticationContext.MobileAuthanticationQueues.Any(x => x.PhoneNumber == userInfo.PhoneNumber &&
            x.IsRegistered == false &&
            x.VerificationCode == userInfo.VerificationCode);

            bool IsSecretkeyValid = this.authanticationContext.MobileAuthanticationQueues.Any(x => x.SecretKey.ToString() == userInfo.SecretKey);

            bool IsUserExisting = this.plsDBContext.Users.Any(x => x.PhoneNumber == userInfo.PhoneNumber);

            if (IsUserExisting)
            {
                var checkUser = this.plsDBContext.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber);
                if (checkUser.LockoutEnabled)
                {
                    DateTime dateTime = checkUser.LockoutEnd;
                    if (DateTime.Now < dateTime)
                    {
                        return "User is locked for 5 minutes";
                    }
                    else
                    {
                        var userDto = this.plsDBContext.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber);
                        userDto.AccessFailedCount = 0;
                        userDto.LockoutEnabled = false;
                        

                        this.plsDBContext.Users.Update(checkUser);
                        this.plsDBContext.SaveChanges();

                    }
                }

                if (checkUser.AccessFailedCount == 5)
                {
                    checkUser.LockoutEnabled = true;
                    checkUser.LockoutEnd = DateTime.Now.AddMinutes(2);
                    this.plsDBContext.Users.Update(checkUser);
                    this.plsDBContext.SaveChanges();

                    return "User is locked for 5 minutes";
                }
            
            }



            if (!IsSecretkeyValid)
            {
                if (IsUserExisting)
                {
                    this.plsDBContext.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber).LockoutEnabled = true;
                    this.plsDBContext.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber).LockoutEnd = DateTime.Now.AddMinutes(2);
                    this.plsDBContext.SaveChanges();
                }
                return StatusCode(404);
            }



            if (!ModelState.IsValid)
            {
                if (IsUserExisting)
                {
                    this.plsDBContext.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber).AccessFailedCount += 1;
                    this.plsDBContext.SaveChanges();
                }

                return this.CreatedAtAction(nameof(Get), new { id = "Invalid Input" });
            }
            else if (IsUserExisting && IsUserValid)
            {
                this.plsDBContext.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber).Token = Guid.NewGuid();
                this.plsDBContext.SaveChanges();
                this.authanticationContext.MobileAuthanticationQueues.FirstOrDefault(x => x.SecretKey.ToString() == userInfo.SecretKey);
                this.authanticationContext.SaveChanges();

                return this.plsDBContext.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber).Token.ToString();
            }
            else if (!IsUserValid)
            {
                if (IsUserExisting)
                {
                    this.plsDBContext.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber).AccessFailedCount += 1;
                    this.plsDBContext.SaveChanges();
                }

                return StatusCode(404);
            }

            var user = this.mapper.Map<User>(userInfo);

            this.plsDBContext.Users.Add(user);
            this.plsDBContext.SaveChanges();

            this.authanticationContext.MobileAuthanticationQueues.FirstOrDefault(x => x.SecretKey.ToString() == userInfo.SecretKey).IsRegistered = true;
            this.authanticationContext.SaveChanges();

            //TODO IMPROVE THE SECURITY            

            return this.plsDBContext.Users.FirstOrDefault(p => p.PhoneNumber == userInfo.PhoneNumber).Token.ToString();
        }

        // PUT: api/MobileLogin/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
