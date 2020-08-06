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
    public class UnitsController : ControllerBase
    {
        private readonly FactoryDbContext context;

        public UnitsController(FactoryDbContext context)
        {
            this.context = context;
        }
        // GET: api/<UnitsController>
        [HttpGet]
        public IEnumerable<Unit> Get()
        {
            return context.Units.ToList();
        }

        // GET api/<UnitsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var unit = context.Units.Single(u => u.ID == id);
            return JsonConvert.SerializeObject(unit);
        }

        // POST api/<UnitsController>
        [HttpPost]
        public string Post([FromBody] string stringUnit)
        {
            var unit = JsonConvert.DeserializeObject<Unit>(stringUnit);
            context.Units.Add(unit);
            context.SaveChanges();
            context.Entry(unit).State = EntityState.Modified;
            context.SaveChanges();
            return JsonConvert.SerializeObject(unit);
        }

        // PUT api/<UnitsController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string stringUnit)
        {
            var unit = JsonConvert.DeserializeObject<Unit>(stringUnit);
            var dbUnit = context.Units.Single(u => u.ID == id);
            dbUnit.Name = unit.Name;
            dbUnit.factoryId = unit.factoryId;
            context.Entry(dbUnit).State = EntityState.Modified;
            context.SaveChanges();
            return JsonConvert.SerializeObject(unit);
        }

        // DELETE api/<UnitsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
