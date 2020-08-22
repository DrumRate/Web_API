using AttributionLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD
{
    public class Unit : IDataBaseEntity
    {
        [CustomDescription("ID")]
        public int Id { get; set; }
        [CustomDescription("Название установки")]
        public string Name { get; set; }
        [CustomDescription("ID завода")]
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }
        //[CustomDescription("Имеющиеся резервуары")]
        //public List<Tank> Tanks { get; set; }
    }
}
