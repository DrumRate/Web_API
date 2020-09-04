using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DockerHello.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        // GET: api/<HelloController>
        [HttpGet]
        public async Task<string> GetAsync()
        {
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync("http://ApiWorld/api/world/");
                var world = await res.Content.ReadAsStringAsync();
                return $"Hello, {world}";
            }
                

        }

        // GET api/<HelloController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HelloController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HelloController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HelloController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
