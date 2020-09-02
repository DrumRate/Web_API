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
        public async Task<IEnumerable<int>> GetEventIds(int unitId, int take, int skip)
        {
            using (var streamReader = new StreamReader("events.json"))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                var count = 0;
                var skipCount = 0;
                var eventIds = new List<int>();
                while (count < take && await jsonTextReader.ReadAsync())
                {
                    if (jsonTextReader.TokenType == JsonToken.StartObject)
                    {
                        var currentObject = (JObject)await JToken.ReadFromAsync(jsonTextReader);
                        var unitIdFromJson = currentObject["UnitId"].Value<int>();
                        if (unitIdFromJson != unitId) continue;

                        var id = currentObject["Id"].Value<int>();
                        if (skipCount < skip)
                        {
                            skipCount++;
                            continue;
                        }

                        eventIds.Add(id);
                        count++;
                    }
                }
                return eventIds;
            }
        }

        // POST api/<EventsController>
        [HttpPost]
        public async IAsyncEnumerable<Event> GetEvents(int[] eventIds)
        {
            using (var streamReader = new StreamReader("events.json"))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                foreach (var id in eventIds)
                {
                    var unitEvent = new Event();
                    while (await jsonReader.ReadAsync())
                    {
                        if (jsonReader.TokenType == JsonToken.StartObject)
                        {
                            JToken currentToken = JObject.Load(jsonReader);
                            if ((int)currentToken["Id"] == id)
                            {
                                unitEvent = currentToken.ToObject<Event>();
                                break;
                            }
                        }
                    }
                    yield return unitEvent;
                }
            }

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
