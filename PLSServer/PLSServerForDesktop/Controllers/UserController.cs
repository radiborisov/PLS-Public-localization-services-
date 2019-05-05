using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PLSDataBase;
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
                    x.IsSavioer,
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
        [HttpPut("{id}/")]
        public void Put(string token, [FromBody] string value)
        {
        }

        [HttpPatch("update/{phoneNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> Patch(string phoneNumber, [FromBody]JsonPatchDocument<PatchUserView> jsonPatch)
        {
            var user = this.context.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            var userDto = mapper.Map<PatchUserView>(jsonPatch);

            jsonPatch.ApplyTo(userDto);

            mapper.Map(userDto, user);

            this.context.Update(user);
            this.context.SaveChanges();

            return true;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
