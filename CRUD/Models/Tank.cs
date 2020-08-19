using System;
using System.Collections.Generic;
using System.Text;
using AttributionLib;

namespace CRUD
{
    public class Tank : IDataBaseEntity
    {
        [CustomDescription("ID")]
        public int Id { get; set; }
        [CustomDescription("Название резервуара")]
        public string Name { get; set; }
        [CustomDescription("Текущий объем")]
        [AllowedRange(0, 1000)]
        public float Volume { get; set; }
        [CustomDescription("Максмальный объем")]
        [AllowedRange(200, 1000)]
        public float MaxVolume { get; set; }
        //public string FactoryName { get; set; }
        [CustomDescription("Id установки")]
        public int UnitId { get; set; }
    }
}
