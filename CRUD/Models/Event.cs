using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class Event
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public double StorageValue { get; set; }
        public string Name { get; set; }
        public int UnitId { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int MyProperty { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<UnitOperator> ResponsibleOperators { get; set; }
    }

}
