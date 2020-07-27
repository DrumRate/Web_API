using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FirstTask
{
    public static class Serializer
    {


        public static string Serialize(List<Factory> factories, List<Unit> units, List<Tank> tanks)
        {
            Dictionary<string, object> data = new Dictionary<string, object> 
            {
                { "factories", factories },
                { "units", units },
                { "tanks", tanks }
            };
            var result = JsonConvert.SerializeObject(data);
            //File.WriteAllText()
            return result.ToString();
        }
    }
}
