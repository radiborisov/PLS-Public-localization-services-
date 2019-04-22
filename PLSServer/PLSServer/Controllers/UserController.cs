using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLSDataBase;
using PLSDataBase.Models;
using PLSMobileServer.ViewModels.User;
using PLSServer.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PLSServer.Controllers
{
    [Route("add/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly PLSDBContext context;
        private readonly IMapper mapper;

        public UserController(PLSDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return this.context.Users.ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public ActionResult<CreateOutputUser> Get(string id)
        {
            var currentUser = this.context.Users.FirstOrDefault(x => x.PhoneNumber == id);

            if (currentUser == null)
            {
                //TODO Log the exception
            }

            var mappedUser = this.mapper.Map<CreateOutputUser>(currentUser);

            return this.CreatedAtAction(nameof(Get), new { id = mappedUser.PhoneNumber, mappedUser });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<string> Post(RegisterInputUser userInfo)
        {
            bool IsUserValid = this.context.RegistrationQueues.Any(x => x.PhoneNumber == userInfo.PhoneNumber &&
            x.IsRegistered == false &&
            x.VerificationCode == userInfo.VerificationCode);

            if (!ModelState.IsValid)
            {
                return this.CreatedAtAction(nameof(Get), new { id = "Invalid Input" });
            }
            else if (this.context.Users.Any(x => x.PhoneNumber == userInfo.PhoneNumber) && IsUserValid)
            {
                this.context.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber).Token = Guid.NewGuid();
                this.context.SaveChanges();

                return this.context.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber).Token.ToString();
            }
            else if(!IsUserValid)
            {
                return StatusCode(404);
            }          

            var user = this.mapper.Map<User>(userInfo);

            this.context.Users.Add(user);

            this.context.SaveChanges();

            //TODO IMPROVE THE SECURITY            

            return this.context.Users.FirstOrDefault(p => p.PhoneNumber == userInfo.PhoneNumber).Token.ToString();
        }
    }
}
