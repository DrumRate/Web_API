using System;
using System.Collections.Generic;
using System.Text;

namespace FirstTask
{
    public class Tank
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Volume { get; set; }
        public float MaxVolume { get; set; }
        public int UnitId { get; set; }

        public Tank(int id, string name, float volume, float maxVolume, int unitId)
        {
            ID = id;
            Name = name;
            Volume = volume;
            MaxVolume = maxVolume;
            UnitId = unitId;
        }

        //public void Sum()
        //{
        //    Console.WriteLine(Volume)
        //}
    }
}
