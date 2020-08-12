using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD.Models.FactoryDbContext;
using CRUD.Repository;
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
        readonly UnitRepository unitRepository;

        public UnitsController(FactoryDbContext context)
        {
            unitRepository = new UnitRepository(context);
        }
        // GET: api/<UnitsController>
        [HttpGet]
        public IEnumerable<Unit> Get()
        {
            return unitRepository.GetAll();
        }

        // GET api/<UnitsController>/5
        [HttpGet("read/{id}")]
        public string Get(int id)
        {
            return unitRepository.Get(id);
        }

        // POST api/<UnitsController>
        [HttpPost]
        public string Post([FromBody] string stringUnit)
        {
            return unitRepository.Create(stringUnit);
        }

        // PUT api/<UnitsController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string stringUnit)
        {
            return unitRepository.Update(id, stringUnit);
        }

        // DELETE api/<UnitsController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return unitRepository.Delete(id);
        }
    }
}
