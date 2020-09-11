using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRUD.DTO;
using CRUD.Models.FactoryDbContext;
using CRUD.Repository;
using CRUD.Validators;
using FluentValidation;
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
        private readonly IFactoryRepository<Unit> _unitRepository;
        private readonly IMapper _mapper;
        private UnitValidator _validator = new UnitValidator();
        public UnitsController(IFactoryRepository<Unit> repository, IMapper mapper)
        {
            _unitRepository = repository;
            _mapper = mapper;
        }
        // GET: api/<UnitsController>
        [HttpGet]
        public async Task<IEnumerable<Unit>> GetAll()
        {
            return await _unitRepository.GetAll();
        }

        // GET api/<UnitsController>/5
        [HttpGet("read/{id}")]
        public async Task<Unit> Get(int? id)
        {
            return await _unitRepository.Get(id);
        }

        // POST api/<UnitsController>
        [HttpPost]
        public async Task<ActionResult<UnitDto>> Create(UnitDto unitDto)
        {
            var unit = _mapper.Map<Unit>(unitDto);
            var validationRes = _validator.Validate(unit);
            if (!validationRes.IsValid)
            {
                return BadRequest(new { errors = validationRes.Errors });
            } 
            var res = await _unitRepository.Create(unit);
            return CreatedAtAction(nameof(Get), new { Id = unit.Id }, unit);
        }

        // PUT api/<UnitsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UnitDto unitDto)
        {
            var unitUpdate = await _unitRepository.Get(id);
            if (unitUpdate == null) return NotFound();
            _mapper.Map(unitDto, unitUpdate);
            await _unitRepository.Update(id, unitUpdate);
            return NoContent();
        }

        // DELETE api/<UnitsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _unitRepository.Delete(id);
        }
    }
}
