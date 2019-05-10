using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PLSDataBase;
using PLSDataBase.Models;
using PLSDesktopAuthanticationDB;
using PLSServerForDesktop.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PLSServerForDesktop.Controllers
{
    [Route("return/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private PLSDBContext context;
        private readonly IMapper mapper;
        private readonly PLSDesktopAuthanticationDBContext dBContext;

        public UserController(PLSDBContext context, IMapper mapper, PLSDesktopAuthanticationDBContext dBContext)
        {
            this.context = context;
            this.mapper = mapper;
            this.dBContext = dBContext;
        }

        // GET api/values
        [HttpGet("{secretKey}")]
        public ActionResult<List<CreateUserAllView>> Get(string secretKey)
        {
            if (!this.dBContext.PLSDesktopUsers.Any(x => x.SecretKey.ToString() == secretKey))
            {
                return StatusCode(404);
            }

            var userLocations = this.context.Users
                .Select(x => new
                {
                    x.PhoneNumber,
                    x.IsSavior,
                    x.IsOnline,
                    x.IsInDanger,
                    Messages = x.UserEmergencyMessages.Where(d => d.Created.Date.Day == DateTime.Now.Day).Select(m => m.Message).ToList(),
                    Locations = x.Locations.Where(l => l.Date.Day == DateTime.Now.Day).ToList()
                })
                .ProjectTo<CreateUserAllView>(this.mapper.ConfigurationProvider)
                .ToList();

            return userLocations;
        }

        // POST api/values                                      
        [HttpPost]
        // PUT api/values/5                                     
        [HttpPut("{secretKey}")]                                       
        public ActionResult<User> Put(string secretKey,[FromBody] PutUserView userInfo)
        {
            if (!this.dBContext.PLSDesktopUsers.Any(x => x.SecretKey.ToString() == secretKey))
            {
                return StatusCode(404);
            }

            if (!this.context.Users.Any(x => x.PhoneNumber == userInfo.PhoneNumber))
            {
                return StatusCode(404);
            }
            var user = this.context.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber);

            user.IsSavior = userInfo.IsSavior;

            this.context.Users.Update(user);
            this.context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{phoneNumber}/{secretkey}")]
        public ActionResult<User> Put(string phoneNumber,string secretKey,[FromBody] PutChangeUserConditionView userInfo)
        {
            if (!this.dBContext.PLSDesktopUsers.Any(x => x.SecretKey.ToString() == secretKey))
            {
                return StatusCode(404);
            }

            if (!this.context.Users.Any(x => x.PhoneNumber == phoneNumber))
            {
                return StatusCode(404);
            }
            var user = this.context.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);

            user.IsInDanger = userInfo.IsInDanger;

            this.context.Users.Update(user);
            this.context.SaveChanges();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
