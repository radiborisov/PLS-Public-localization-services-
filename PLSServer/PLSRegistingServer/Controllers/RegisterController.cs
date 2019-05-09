using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexmo.Api;
using PLSDataBase;
using PLSDataBase.Models;
using PLSMobileRegisterDB;
using PLSRegisterDB.Models;
using PLSRegistingServer.Models.UsersModels;

namespace PLSRegistingServer.Controllers
{
    [Route("add/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private PLSMobileRegisterDBContext registerContext;
        private PLSDBContext plsDBContext;
       private readonly IMapper mapper;

        public RegisterController(PLSMobileRegisterDBContext registerContext,PLSDBContext plsDBContext )
        {
            this.registerContext = registerContext;
            this.plsDBContext = plsDBContext;
            this.mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return plsDBContext.Users.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<string> Post(RegisterUserDto userInfo)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(404);
            }

            Random random = new Random();
            int randomNumber = 4444;

            //if (!SendSms(userInfo.PhoneNumber, randomNumber))
            //{
            //    return StatusCode(404);
            //}

            var user = this.mapper.Map<MobileUserRegisterQueue>(userInfo);
            user.VerificationCode = randomNumber.ToString();

            this.registerContext.MobileUserRegisterQueues.Add(user);
            this.registerContext.SaveChanges();

            return user.SecretKey.ToString();
        }

        private bool SendSms(string phoneNumber, int randomNumber)
        {
            string bulgarianPhoneNumber = "359" + phoneNumber.Substring(1, phoneNumber.Length - 1);


            try
            {
                var client = new Client(creds: new Nexmo.Api.Request.Credentials
                {
                    ApiKey = "6d78d2d9",
                    ApiSecret = "McaWa0gUEg2DP4dr"
                });
                var results = client.SMS.Send(request: new SMS.SMSRequest
                {
                    from = "Nexmo",
                    to = bulgarianPhoneNumber,
                    text = "This is your verification code:" + randomNumber.ToString()
                });
            }
            catch (Exception e)
            {
                return false;
            }


            return true;
        }
    }
}
