using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainProject
{
    public class Factory : IDataBaseEntity
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc{ get; set; }

        public Factory(int id, string name, string desc)
        {
            ID = id;
            Name = name;
            Desc = desc;
        }
        
        public void GetInfo()
        {
            Console.WriteLine($"ID: {ID} Название: {Name} Описание: {Desc}\n");
        }

    }
}
