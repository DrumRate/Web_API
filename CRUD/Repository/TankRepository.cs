using AutoMapper;
using CRUD.DTO;
using CRUD.Models.FactoryDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Repository
{
    public class TankRepository : IFactoryRepository<Tank>
    {
        private readonly FactoryDbContext _context;

        public TankRepository(FactoryDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Tank>> GetAll()
        {
            List<Tank> tanks = await (from t in _context.Tanks 
                                      join u in _context.Units on t.UnitId equals u.Id
                                      join f in _context.Factories on u.FactoryId equals f.Id
                                      select new Tank { Id = t.Id, MaxVolume = t.MaxVolume, Name = t.Name, Volume = t.Volume, UnitId = t.UnitId} ).ToListAsync();
            return tanks;
        }
        public async Task<Tank> Get(int? id)
        {
            return await _context.Tanks.FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<Tank> Create(Tank tank)
        {
            await _context.Tanks.AddAsync(tank);
            await _context.SaveChangesAsync();
            return tank;
        }

        public async Task<Tank> Update(int id, Tank tank)
        {
            var dbTank = await _context.Tanks.FirstOrDefaultAsync(f => f.Id == id);
            dbTank.Name = tank.Name;
            dbTank.Volume = tank.Volume;
            dbTank.MaxVolume = tank.MaxVolume;
            //dbTank.FactoryName = tank.FactoryName;
            await _context.SaveChangesAsync();
            return tank;
        }

        public async Task<bool> Delete(int id)
        {
            Tank tank = await _context.Tanks.FirstOrDefaultAsync(f => f.Id == id);
            _context.Tanks.Remove(tank);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
