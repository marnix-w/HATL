using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    public class IArea
    {
        public int ID { get; set; }
        public string Classification { get; set; }
        public string AreaType { get; set; }
        public Point Position { get; set; }
        public Point Dimension { get; set; }
        public int Capacity { get; set; }
    }

}
