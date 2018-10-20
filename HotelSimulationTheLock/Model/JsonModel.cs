using Newtonsoft.Json;
using System.Drawing;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// update from hotel.layout 1 there were typo's in Dimension and Position
    /// update from hotel.layout 2 there has been ID added
    /// update from hotel.layout 3 there was more data to be found
    /// 
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
