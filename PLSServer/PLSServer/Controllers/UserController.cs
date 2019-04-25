using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        public ActionResult<IEnumerable<CreateOutputUser>> Get()
        {
            return this.context.Users
                .ProjectTo<CreateOutputUser>(mapper.ConfigurationProvider)
                .ToList();
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
