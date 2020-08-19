using System;
using System.Collections.Generic;
using System.Text;

namespace MainProject
{
    public class Unit : IDataBaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int FactoryId { get; set; }

        public Unit(int id, string name, int factoryId)
        {
            ID = id;
            Name = name;
            FactoryId = factoryId;
        }

        //public string ToString()
        //{
        //    return $"ID = {ID}, Название = {Name}";
        //}
    }
}
