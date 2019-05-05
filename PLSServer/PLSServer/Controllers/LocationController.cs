using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLSDataBase;
using PLSDataBase.Models;
using PLSServer.ViewModels.Location;
using System.Collections.Generic;
using System.Linq;

namespace PLSServer.Controllers
{
    [Route("add/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        PLSDBContext context;
        private readonly IMapper mapper;

        public LocationController(PLSDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{phoneNumber}/{token}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Location>> Get(string phoneNumber, string token)
        {          
            if (!CheckAutharization(token, phoneNumber))
            {
                return StatusCode(404);
            }

            return this.context.Locations.Where(u => u.User.PhoneNumber == phoneNumber).ToList();
        }

        //[HttpGet("{id}")]
        //[ProducesResponseType(200)]
        //public ActionResult<Location> Get(int id, string token)
        //{
        //    if (!CheckAutharization(token, id))
        //    {
        //        return StatusCode(401);
        //    }

        //    var user = this.context.Users.FirstOrDefault(t => t.Token == token);

        //    var currentLocation = this.context.Locations.FirstOrDefault(x => x.Id == id);

        //    if (currentLocation == null)
        //    {
        //        return this.CreatedAtAction(nameof(Get), new { id = "Invalid Input" });
        //    }

        //    return this.CreatedAtAction(nameof(Get), new { id = currentLocation.Id, currentLocation });
        //}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Location> Post([FromBody] CreateInputLocation locationInfo)
        {
            if (!CheckAutharization(locationInfo.Token, locationInfo.PhoneNumber))
            {
                return StatusCode(401);
            }

            if (!ModelState.IsValid)
            {
                return this.CreatedAtAction(nameof(Get), new { id = "Invalid Input" });
            }
            else if (!this.context.Users.Any(t => t.Token.ToString() == locationInfo.Token.ToString() && t.PhoneNumber == locationInfo.PhoneNumber))
            {
                return this.CreatedAtAction(nameof(Get), new { id = "Current devices can't be found" });
            }

            var location = this.mapper.Map<Location>(locationInfo);

            location.UserId = this.context.Users.FirstOrDefault(x => x.PhoneNumber == locationInfo.PhoneNumber).Id;

            this.context.Locations.Add(location);

            this.context.SaveChanges();

            return StatusCode(201);
        }


        private bool CheckAutharization(string token, string phoneNumber)
        {
            if (!this.context.Users.Any(t => t.Token.ToString() == token && t.PhoneNumber == phoneNumber))
            {
                return false;
            }

            return true;
        }
    }
}
