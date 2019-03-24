using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLSDataBase;
using PLSDataBase.Models;
using System.Collections.Generic;
using System.Linq;

namespace PLSServer.Controllers
{
    [Route("add/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        PLSDBContext context;

        public LocationController(PLSDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            return this.context.Locations.ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public ActionResult<Location> Get(int id)
        {
            var currentLocation = this.context.Locations.FirstOrDefault(x => x.Id == id);

            if (currentLocation == null)
            {
                return this.CreatedAtAction(nameof(Get), new { id = "Invalid Input" });
            }

            return this.CreatedAtAction(nameof(Get), new { id = currentLocation.Id, currentLocation });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Location> Post([FromBody] Location userInfo)
        {
            if (!ModelState.IsValid)
            {
                return this.CreatedAtAction(nameof(Get), new { id = "Invalid Input" });
            }
            else if (!this.context.Users.Any(u => u.Id == userInfo.UserId))
            {
                return this.CreatedAtAction(nameof(Get), new { id = "Current devices can't be found" });
            }

            this.context.Locations.Add(userInfo);

            this.context.SaveChanges();

            return this.CreatedAtAction(nameof(Get), new { id = userInfo.UserId, userInfo });
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }
    }
}
