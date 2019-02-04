using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLSServer.Data;

namespace PLSServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ApiController
    {
        private DbServer context;

        public ValuesController(DbServer context)
        {
            this.context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            return this.context.Location.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]


        public ActionResult<Location> Get(int id)
        {
            var location = this.context.Location.FirstOrDefault(x => x.UserId == id);
            if (location == null)
            {
                return this.NotFound();
            }
            return location;
        }


        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Location> Post([FromBody] Location userInfo)
        {
            this.context.Location.Add(userInfo);
            this.context.SaveChanges();

            return this.CreatedAtAction(nameof(Get), new { id = userInfo.UserId, userInfo });
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
