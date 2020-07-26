using System;
using System.Collections.Generic;
using System.Text;

namespace FirstTask
{
    public class Factory
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
