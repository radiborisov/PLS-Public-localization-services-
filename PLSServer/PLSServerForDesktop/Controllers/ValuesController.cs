using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PLSDataBase;

namespace PLSServerForDesktop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private PLSDBContext context;

        public ValuesController(PLSDBContext context)
        {
            this.context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<Dictionary<string, List<decimal>>> Get()
        {
            var usersLocations = new Dictionary<string,List<decimal>>();

            var userLocations = context.Users.Select(u => new
            {
                u.PhoneNumber,
                locations = u.Locations
            });

            foreach (var item in userLocations)
            {
                usersLocations.Add(item.PhoneNumber, new List<decimal>());

                foreach (var location in item.locations)
                {
                    usersLocations[item.PhoneNumber].Add(location.Longitude);
                    usersLocations[item.PhoneNumber].Add(location.Latitude);
                    usersLocations[item.PhoneNumber].Add(location.Altitude);
                }
            }

            return usersLocations;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
