using System;
using System.Collections.Generic;

using System.ComponentModel.Composition;

using System.Drawing;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Restaurant")]
    public class Restaurant : IArea
    {
        public Point Position { get; set; }
        public Point Dimension { get; set; }
        public int Capacity { get; set; }
        public Image Art { get; set; } = Properties.Resources.restaurant;
        Status IArea.Status { get; set; }

        private Restaurant()
        {
                        
        }

        public IArea CreateArea(Point position, int capacity, Point dimension, int clasification)
        {
            Restaurant rs = new Restaurant();

            rs.Position = position;
            rs.Capacity = capacity;
            rs.Dimension = dimension;

            return rs;
        }

        

    }
}
