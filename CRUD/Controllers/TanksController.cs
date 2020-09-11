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
    public class TanksController : ControllerBase
    {
        private readonly IFactoryRepository<Tank> _tankRepository;
        private readonly IMapper _mapper;
        private TankValidator _validator = new TankValidator();
        public TanksController(IFactoryRepository<Tank> repository, IMapper mapper)
        {
            _tankRepository = repository;
            _mapper = mapper;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Tank>> GetAll()
        {
            return await _tankRepository.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("read/{id}")]
        public async Task<Tank> Get(int? id)
        {
            return await _tankRepository.Get(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<TankDto>> Create(TankDto tankDto)
        {
            var tank = _mapper.Map<Tank>(tankDto);
            var validationRes = _validator.Validate(tank);
            if (!validationRes.IsValid) return BadRequest(new { errors = validationRes.Errors });
            var res = await _tankRepository.Create(tank);
            return CreatedAtAction(nameof(Get), new { Id = tank.Id }, tank);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TankDto tankDto)
        {
            var tankUpdate = await _tankRepository.Get(id);
            if (tankUpdate == null) return NotFound();
            _mapper.Map(tankDto, tankUpdate);
            await _tankRepository.Update(id, tankUpdate);
            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _tankRepository.Delete(id);
        }
    }
}
