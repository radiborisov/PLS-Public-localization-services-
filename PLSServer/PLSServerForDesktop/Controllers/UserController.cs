using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using PLSDataBase;
using PLSServerForDesktop.ViewModels.Users;
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
                .ProjectTo<CreateUserAllView>()
                .ToList();

            return userLocations;
        }

        // GET api/values/5
        public void Post([FromBody] string value)
        {
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
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
