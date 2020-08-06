using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD
{
    public class Unit : IDataBaseEntity
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public Factory factoryId { get; set; }

    }
}
