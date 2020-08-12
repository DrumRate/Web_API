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
    public class TanksController : ControllerBase
    {
        readonly TankRepository tankRepository;

        public TanksController(FactoryDbContext context)
        {
            tankRepository = new TankRepository(context);
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Tank> Get()
        {
            return tankRepository.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("read/{id}")]
        public string Get(int id)
        {
            return tankRepository.Get(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public string Post([FromBody] string stringTank)
        {
            return tankRepository.Create(stringTank);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string stringTank)
        {
            return tankRepository.Update(id, stringTank);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return tankRepository.Delete(id);
        }
    }
}
