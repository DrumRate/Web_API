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
    public class FactoriesController : ControllerBase
    {
        readonly FactoryRepository factoryRepository;

        public FactoriesController(FactoryDbContext context)
        {
            factoryRepository = new FactoryRepository(context);
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Factory> Get()
        {
            return factoryRepository.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("read/{id}")]
        public string Get(int id)
        {
            return factoryRepository.Get(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public string Post([FromBody] string stringFactory)
        {
            return factoryRepository.Create(stringFactory);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string stringFactory)
        {
            return factoryRepository.Update(id, stringFactory);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return factoryRepository.Delete(id);
        }
    }
}
