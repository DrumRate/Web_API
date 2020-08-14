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
        private readonly FactoryDbContext context;

        public UnitRepository(FactoryDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Unit> GetAll()
        {
            List<Unit> units = context.Unit.Include(f => f.Factory).ToList();
            return units;
        }
        public string Get(int? id)
        {
            var unit = context.Unit.Single(f => f.Id == id);
            return JsonConvert.SerializeObject(unit);
        }
        public string Create(string stringUnit)
        {
            Unit unit = JsonConvert.DeserializeObject<Unit>(stringUnit);
            context.Unit.Add(unit);
            context.SaveChanges();
            context.Entry(unit).State = EntityState.Modified;
            context.SaveChanges();
            return JsonConvert.SerializeObject(unit);
        }

        public string Update(int id, string stringUnit)
        {
            var unit = JsonConvert.DeserializeObject<Unit>(stringUnit);
            var dbUnit = context.Unit.Single(f => f.Id == id);
            dbUnit.Name = unit.Name;
            context.Entry(dbUnit).State = EntityState.Modified;
            context.SaveChanges();
            return JsonConvert.SerializeObject(unit);
        }

        public bool Delete(int id)
        {
            var unit = context.Unit.Single(f => f.Id == id);
            context.Unit.Remove(unit);
            context.SaveChanges();
            return true;
        }

    }
}
