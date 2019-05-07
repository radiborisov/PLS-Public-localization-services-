using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PLSDataBase;
using PLSDataBase.Models;
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

        public UserController(PLSDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<CreateUserAllView>> Get()
        {
            var userLocations = this.context.Users
                .Select(x => new
                {
                    x.PhoneNumber,
                    x.IsSavior,
                    x.IsOnline,
                    Locations = x.Locations.Where(l => l.Date.Day == DateTime.Now.Day)
                })
                .ProjectTo<CreateUserAllView>(this.mapper.ConfigurationProvider)
                .ToList();

            return userLocations;
        }                                                       
                                                                
        // POST api/values                                      
        [HttpPost]                                              
        // PUT api/values/5                                     
        [HttpPut("{secret}")]                                       
        public ActionResult<User> Put(string secret, [FromBody] PutUserView userInfo)
        {
            var user = this.context.Users.FirstOrDefault(x => x.Secret.ToString() == secret);

            user.IsSavior = userInfo.IsSavior;

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
