using AttributionLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CRUD.DTO
{
    public class TankDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [AllowedRange(0, 1000)]
        public double Volume { get; set; }
        [AllowedRange(200, 1000)]
        public double MaxVolume { get; set; }
        public int UnitId { get; set; }
        public Factory Factory { get; set; }
        public bool IsValid()
        {
            if (Volume > MaxVolume)
                return false;

            var volumeInfo = (AllowedRangeAttribute)typeof(TankDto).GetProperty("Volume")
                                                                .GetCustomAttributes(typeof(AllowedRangeAttribute));
            if (Volume < volumeInfo.MinValue || Volume > volumeInfo.MaxValue)
                return false;

            var MaxVolumeInfo = (AllowedRangeAttribute)typeof(TankDto).GetProperty("MaxVolume")
                                                                   .GetCustomAttributes(typeof(AllowedRangeAttribute));
            if (MaxVolume < MaxVolumeInfo.MinValue || MaxVolume > MaxVolumeInfo.MaxValue)
                return false;

            return true;
        }
    }
}
