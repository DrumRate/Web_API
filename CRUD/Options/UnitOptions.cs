using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Options
{
    public class UnitOptions
    {
        public IEnumerable<int> UnitsToTrackEvents { get; set; }
        public int TakeCount { get; set; }
        public int SyncIntervalMinutes { get; set; }
    }
}
