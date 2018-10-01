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
        IArea CreateArea(Point position, int capacity, Point dimension, int clasification);  
        Point Position { get; set; }
        Point Dimension { get; set; }
        int Capacity { get; set; }
        Image Art { get; set; }
        Enum Status { get; set; }

    }
}
