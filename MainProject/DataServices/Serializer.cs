using Newtonsoft.Json;
using System.Collections.Generic;

namespace MainProject
{
    public static class Serializer
    {


        public static string Serialize(IReadOnlyCollection<Factory> factories, IReadOnlyCollection<Unit> units, IReadOnlyCollection<Tank> tanks)
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
