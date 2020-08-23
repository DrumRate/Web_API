using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRUD.DTO;
using CRUD.Models.FactoryDbContext;
using CRUD.Repository;
using CRUD.Validators;
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
        private readonly IFactoryRepository<Factory> _factoryRepository;
        private readonly IMapper _mapper;
        private FactoryValidator _validator = new FactoryValidator();
        public FactoriesController(IFactoryRepository<Factory> repository, IMapper mapper)
        {
            _factoryRepository = repository;
            _mapper = mapper;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public Task<IEnumerable<Factory>> GetAll()
        {
 
            return _factoryRepository.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("read/{id}")]
        public async Task<Factory> Get(int? id)
        {
            return await _factoryRepository.Get(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<FactoryDto>> Create(FactoryDto factoryDto)
        {
            var factory = _mapper.Map<Factory>(factoryDto);
            var validationRes = _validator.Validate(factory);
            if (!validationRes.IsValid) return BadRequest(new { errors = validationRes.Errors });
            var res = await _factoryRepository.Create(factory);
            return CreatedAtAction(nameof(Get), new { Id = factory.Id }, factory);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] FactoryDto factory)
        {
            var factoryUpdate = await _factoryRepository.Get(id);
            if (factoryUpdate == null) return NotFound();
            _mapper.Map(factory, factoryUpdate);
            await _factoryRepository.Update(id, factoryUpdate);
            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _factoryRepository.Delete(id);
        }
    }
}
