using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;


namespace HotelSimulationTheLock
{
    public interface IArea
    {
        IArea CreateArea();
    //    string AreaType { get; set; }
        Point Position { get; set; }
        Point Dimension { get; set; }
        int Capacity { get; set; }
        Image Art { get; set; }
        int ArtWidth { get; set; }
        int ArtHeight { get; set; }
        Enum Status { get; set; }

    }
}
