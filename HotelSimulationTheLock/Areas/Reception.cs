using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    public class Reception : IArea
    {
        public Point Position { get; set; } = new Point(0, 1); // LOOK FOR Y OR X (nu als xy)
        public Point Dimension { get; set; } = new Point(1, 1);
        public int Capacity { get; set; } = 1;
        public Image Art { get; set; } = Properties.Resources.reception;
        Status IArea.Status { get; set; }

        // Dijkstra search varibles
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();

        public Reception()
        {
            
        }

        public IArea CreateArea(Point position, int capacity, Point dimension, int clasification)
        {
            return new Reception();
        }
        
        
    }
}
