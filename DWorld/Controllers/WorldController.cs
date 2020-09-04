using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DockerWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldController : ControllerBase
    {
        // GET: api/<WorldController>
        [HttpGet]
        public string Get()
        {
            return "world";
        }

        // GET api/<WorldController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WorldController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WorldController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WorldController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
