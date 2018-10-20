using Newtonsoft.Json;
using System.Drawing;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// An temporary class for saving the information pulled from
    /// an json layout file
    /// </summary>
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

        [JsonProperty("Dimension")]
        public Size Dimension { get; set; }

        [JsonProperty("Capacity")]
        public int Capacity { get; set; }
    }
}
