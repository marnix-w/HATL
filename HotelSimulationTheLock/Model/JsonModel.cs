using Newtonsoft.Json;
using System.Drawing;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// update from hotel.layout 1 there were typo's in Dimension and Position
    /// update from hotel.layout 2 there has been an addition of ID
    /// update from hotel.layout 3 there was more data to be found
    /// 
    /// A temporary class for saving the information pulled from
    /// a json layout file
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
