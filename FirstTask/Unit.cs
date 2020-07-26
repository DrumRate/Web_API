using System;
using System.Collections.Generic;
using System.Text;

namespace FirstTask
{
    public class Unit
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

        protected string ToString()
        {
            return $"ID = {ID}, Название = {Name}";
        }
    }
}
