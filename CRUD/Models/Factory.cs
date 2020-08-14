using System;
using AttributionLib;

namespace CRUD
{
    public class Factory : IDataBaseEntity
    {
        [CustomDescription("ID")]
        public int Id { get; set; }
        [CustomDescription("Название фабрики")]
        public string Name { get; set; }
        [CustomDescription("Описание фабрики")]
        public string Description { get; set; }
    }
}
