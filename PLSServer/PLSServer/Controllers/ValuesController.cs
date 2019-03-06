﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLSServer.DBContext.Data;
using PLSServer.DBContext.Data.Models;

namespace PLSServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ApiController
    {
        PLSDBContext context;

        public ValuesController(PLSDBContext context)
        {
            this.context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            using (context)
            {
                return this.context.Locations.ToList();
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public ActionResult<int> Get(string id)
        {
            using (context)
            {
                var currentUser = this.context.Users.FirstOrDefault(x => x.PhoneNumber == id);
                if (currentUser == null)
                {
                    User user = new User()
                    {
                        PhoneNumber = id,
                        IsOnline = false                      
                    };

                    this.context.Users.Add(user);
                    this.context.SaveChanges();

                    currentUser = this.context.Users.FirstOrDefault(x => x.PhoneNumber == id);

                    return currentUser.Id;
                }
                return currentUser.Id;

            }
        }


        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Location> Post([FromBody] Location userInfo)
        {
            using (context)
            {
                this.context.Locations.Add(userInfo);
                this.context.SaveChanges();

                return this.CreatedAtAction(nameof(Get), new { id = userInfo.UserId, userInfo });
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
