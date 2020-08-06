using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD
{
    public class Tank : IDataBaseEntity
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public float Volume { get; set; }
        public float MaxVolume { get; set; }
        public Unit UnitId { get; set; }
    }
}
