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
        private readonly FactoryDbContext context;
        public FactoryRepository(FactoryDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Factory> GetAll()
        {
            List<Factory> factories = context.Factory.ToList();
            return factories;
        }
        public string Get(int? id)
        {
            var factory = context.Factory.Single(f => f.Id == id);
            return JsonConvert.SerializeObject(factory);
        }
        public string Create(string stringFactory)
        {
            Factory factory = JsonConvert.DeserializeObject<Factory>(stringFactory);
            context.Factory.Add(factory);
            context.SaveChanges();
            context.Entry(factory).State = EntityState.Modified;
            context.SaveChanges();
            return JsonConvert.SerializeObject(factory);
        }

        public string Update(int id, [FromBody] string stringFactory)
        {
            var factory = JsonConvert.DeserializeObject<Factory>(stringFactory);
            var dbFactory = context.Factory.Single(f => f.Id == id);
            dbFactory.Name = factory.Name;
            dbFactory.Description = factory.Description;
            context.Entry(dbFactory).State = EntityState.Modified;
            context.SaveChanges();
            return JsonConvert.SerializeObject(factory);
        }

        public bool Delete(int id)
        {
            var factory = context.Factory.Single(f => f.Id == id);
            context.Factory.Remove(factory);
            context.SaveChanges();
            return true;
        }

    }
}
