﻿using AutoMapper;
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

        [HttpGet("{phoneNumber}/{token}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> Get(string phoneNumber, string token)
        {
            bool IsUserValid = this.context.Users.Any(x => x.PhoneNumber == phoneNumber && x.Token.ToString() == token);

            if (!IsUserValid)
            {
                return StatusCode(404);
            }

            return true;
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public ActionResult<string> Post(RegisterInputUser userInfo)
        //{
        //    bool IsUserValid = this.context.RegistrationQueues.Any(x => x.PhoneNumber == userInfo.PhoneNumber &&
        //    x.IsRegistered == false &&
        //    x.VerificationCode == userInfo.VerificationCode);

        //    if (!ModelState.IsValid)
        //    {
        //        return this.CreatedAtAction(nameof(Get), new { id = "Invalid Input" });
        //    }
        //    else if (this.context.Users.Any(x => x.PhoneNumber == userInfo.PhoneNumber) && IsUserValid)
        //    {
        //        this.context.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber).Token = Guid.NewGuid();
        //        this.context.SaveChanges();
        //        this.context.RegistrationQueues.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber).IsRegistered = true;
        //        this.context.SaveChanges();

        //        return this.context.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber).Token.ToString();
        //    }
        //    else if(!IsUserValid)
        //    {
        //        return StatusCode(404);
        //    }          

        //    var user = this.mapper.Map<User>(userInfo);

        //    this.context.Users.Add(user);
        //    this.context.SaveChanges();

        //    this.context.RegistrationQueues.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber).IsRegistered = true;
        //    this.context.SaveChanges();

        //    //TODO IMPROVE THE SECURITY            

        //    return this.context.Users.FirstOrDefault(p => p.PhoneNumber == userInfo.PhoneNumber).Token.ToString();
        //}

        // PUT api/values/5                                     
        [HttpPut]
        public ActionResult<User> Put([FromBody] ChangeUserCondition userInfo)
        {
            if (!this.context.Users.Any(x => x.PhoneNumber == userInfo.PhoneNumber))
            {
                return StatusCode(404);
            }
        
            var user = this.context.Users.FirstOrDefault(x => x.PhoneNumber == userInfo.PhoneNumber && x.Token.ToString() == userInfo.Token);

            user.IsInDanger = userInfo.IsInDanger;

            this.context.Users.Update(user);
            this.context.SaveChanges();

            var emergencyMessage = new UserEmergencyMessage
            {
                Message = userInfo.Message,
                UserId = user.Id
            };

            this.context.UserEmergencyMessages.Add(emergencyMessage);
            this.context.SaveChanges();

            return NoContent();
        }
    }
}
