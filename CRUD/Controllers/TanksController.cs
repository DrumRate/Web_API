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
    public class TanksController : ControllerBase
    {
        private readonly FactoryDbContext context;

        public TanksController(FactoryDbContext context)
        {
            this.context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
       public IEnumerable<Tank> Get()
        {
            return context.Tanks.ToList();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var tank = context.Tanks.Single(f => f.ID == id);
            return JsonConvert.SerializeObject(tank);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public string Post([FromBody] string stringTank)
        {
            var tank = JsonConvert.DeserializeObject<Tank>(stringTank);
            context.Tanks.Add(tank);
            context.SaveChanges();
            context.Entry(tank).State = EntityState.Modified;
            context.SaveChanges();
            return JsonConvert.SerializeObject(tank);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string stringTank)
        {
            var tank = JsonConvert.DeserializeObject<Tank>(stringTank);
            var dbTank = context.Tanks.Single(t => t.ID == id);
            dbTank.Name = tank.Name;
            dbTank.MaxVolume = tank.MaxVolume;
            dbTank.Volume = tank.Volume;
            dbTank.UnitId = tank.UnitId;
            context.Entry(dbTank).State = EntityState.Modified;
            context.SaveChanges();
            return JsonConvert.SerializeObject(tank);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var tank = context.Tanks.Single(t => t.ID == id);
            context.Tanks.Remove(tank);
            context.SaveChanges();
        }
    }
}
