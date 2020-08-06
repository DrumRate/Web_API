using System;

namespace CRUD
{
    public class Factory : IDataBaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc{ get; set; }
        public int JSON_Id { get; set; }
    }
}
