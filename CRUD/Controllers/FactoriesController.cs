using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD.Models.FactoryDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoriesController : ControllerBase
    {
        private readonly FactoryDbContext context;

        public FactoriesController(FactoryDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Factory> Get()
        {
            return context.Factories.ToList();
        }

        // GET api/<ValuesController>/5
        [HttpGet("read/{id}")]
        public string Get(int id)
        {
            var factory = context.Factories.Single(f => f.ID == id);
            return JsonConvert.SerializeObject(factory);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public string Post([FromBody] string stringFactory)
        {
            var factory = JsonConvert.DeserializeObject<Factory>(stringFactory);
            context.Factories.Add(factory);
            context.SaveChanges();
            factory.JSON_Id = factory.ID;
            context.Entry(factory).State = EntityState.Modified;
            context.SaveChanges();
            return JsonConvert.SerializeObject(factory);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string stringFactory)
        {
            var factory = JsonConvert.DeserializeObject<Factory>(stringFactory);
            var dbFactory = context.Factories.Single(f => f.ID == id);
            dbFactory.Name = factory.Name;
            dbFactory.Desc = factory.Desc;
            dbFactory.JSON_Id = factory.JSON_Id;
            context.Entry(dbFactory).State = EntityState.Modified;
            context.SaveChanges();
            return JsonConvert.SerializeObject(factory);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var factory = context.Factories.Single(f => f.ID == id);
            context.Factories.Remove(factory);
            context.SaveChanges();
        }
    }
}
