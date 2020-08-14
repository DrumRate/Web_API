using CRUD.Models.FactoryDbContext;
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
        private readonly FactoryDbContext context;

        public TankRepository(FactoryDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Tank> GetAll()
        {
            List<Tank> tanks = context.Tank.Include(u => u.Unit.Factory).ToList();
            return tanks;
        }
        public string Get(int? id)
        {
            var tank = context.Tank.Single(f => f.Id == id);
            return JsonConvert.SerializeObject(tank);
        }
        public string Create(string stringTank)
        {
            Tank tank = JsonConvert.DeserializeObject<Tank>(stringTank);
            context.Tank.Add(tank);
            context.SaveChanges();
            context.Entry(tank).State = EntityState.Modified;
            context.SaveChanges();
            return JsonConvert.SerializeObject(tank);
        }

        public string Update(int id, string stringTank)
        {
            var tank = JsonConvert.DeserializeObject<Tank>(stringTank);
            var dbTank = context.Tank.Single(f => f.Id == id);
            dbTank.Name = tank.Name;
            dbTank.Volume = tank.Volume;
            dbTank.MaxVolume = tank.MaxVolume;
            context.Entry(dbTank).State = EntityState.Modified;
            context.SaveChanges();
            return JsonConvert.SerializeObject(tank);
        }

        public bool Delete(int id)
        {
            var tank = context.Tank.Single(f => f.Id == id);
            context.Tank.Remove(tank);
            context.SaveChanges();
            return true;
        }

    }
}
