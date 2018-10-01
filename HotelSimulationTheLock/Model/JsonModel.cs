using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    public class JsonModel
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Classification")]
        public string Classification { get; set; }

        [JsonProperty("AreaType")]
        public string AreaType { get; set; }

        [JsonProperty("Position")]
        public Point Position { get; set; }

        [JsonProperty("Dimention")]
        public Point Dimension { get; set; }

        [JsonProperty("Capacity")]
        public int Capacity { get; set; }
    }
}
