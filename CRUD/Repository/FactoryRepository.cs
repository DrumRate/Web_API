using CRUD.Models.FactoryDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Repository
{
    public class FactoryRepository : IFactoryRepository<Factory>
    {
        private readonly FactoryDbContext _context;
        public FactoryRepository(FactoryDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Factory>> GetAll()
        {
            List<Factory> factories = await _context.Factories.ToListAsync();
            return factories;
        }
        public async Task<Factory> Get(int? id)
        {
            return await _context.Factories.FirstOrDefaultAsync(f => f.Id == id);

        }
        public async Task<Factory> Create(Factory factory)
        {
            //Factory factory = JsonConvert.DeserializeObject<Factory>(stringFactory);
            await _context.Factories.AddAsync(factory);
            await _context.SaveChangesAsync();
            //context.Entry(factory).State = EntityState.Modified;
            return factory;
        }

        public async Task<Factory> Update(int id, Factory factory)
        {
            //var updateFactory = JsonConvert.DeserializeObject<Factory>(factory);
            var dbFactory = await _context.Factories.FirstOrDefaultAsync(f => f.Id == id);
            dbFactory.Name = factory.Name;
            dbFactory.Description = factory.Description;
            _context.Entry(dbFactory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return factory;
        }

        public async Task<bool> Delete(int id)
        {
            Factory factory = await _context.Factories.FirstOrDefaultAsync(f => f.Id == id);
            _context.Factories.Remove(factory);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
