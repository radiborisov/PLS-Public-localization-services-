using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nexmo.Api;
using PLSDataBase;
using PLSDataBase.Models;
using PLSMobileServer.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLSServer.Controllers
{
    [Route("add/[controller]")]
    [ApiController]
    public class PhoneVerificationController : Controller
    {
        private readonly PLSDBContext context;
        private readonly IMapper mapper;

        public PhoneVerificationController(PLSDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return this.context.Users.ToList();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<int> Post(CreateInputUser userInfo)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(404);
            }

            Random random = new Random();
            int randomNumber = random.Next(1000,9999);

            if (!SendSms(userInfo.PhoneNumber, randomNumber))
            {
                return StatusCode(404);
            }

            var user = this.mapper.Map<RegistrationQueue>(userInfo);
            user.VerificationCode = randomNumber.ToString();

            this.context.RegistrationQueues.Add(user);
            this.context.SaveChanges();

            return StatusCode(200);
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
            catch(Exception e)
            {
                return false;
            }


            return true;
        }
    }
}
