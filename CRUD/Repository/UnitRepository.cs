using CRUD.Models.FactoryDbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Repository
{
    public class UnitRepository : IFactoryRepository<Unit>
    {
        private readonly FactoryDbContext _context;

        public UnitRepository(FactoryDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Unit>> GetAll()
        {
            List<Unit> units = await _context.Units.Include(t => t.Tank).ToListAsync();
            return units;
        }
        public async Task<Unit> Get(int? id)
        {
            return await _context.Units.FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<Unit> Create(Unit unit)
        {
            await _context.Units.AddAsync(unit);
            await _context.SaveChangesAsync();
            return unit;
        }

        public async Task<Unit> Update(int id, Unit unit)
        {
            var dbUnit = await _context.Units.FirstOrDefaultAsync(f => f.Id == id);
            dbUnit.Name = unit.Name;
            await _context.SaveChangesAsync();
            return unit;
        }

        public async Task<bool> Delete(int id)
        {
            Unit unit = await _context.Units.FirstOrDefaultAsync(f => f.Id == id);
            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
