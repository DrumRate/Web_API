using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIEvent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        // GET: api/<EventsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EventsController>/5
        //api/events/keys?unitId=1&take=3&skip=3
        [HttpGet]
        [Route("keys")]
        public async Task<string> GetEventIdsAsync(int unitId, int take, int skip)
        {
            var dirInfo = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Parent?.Parent?.Parent;
            string path = Path.Combine(dirInfo.FullName, "events.json");
            List<Event> events = JsonConvert.DeserializeObject<List<Event>>(await System.IO.File.ReadAllTextAsync(path));
            var filteredEvents = events.Where(u => u.UnitId == unitId).Skip(skip).Take(take);
            JArray array = new JArray(filteredEvents.Select(s => s.Id).ToArray());
            return array.ToString();
        }

        // POST api/<EventsController>
        [HttpPost]
        public void Post([FromBody] int[] array)
        {
        }

        // PUT api/<EventsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
