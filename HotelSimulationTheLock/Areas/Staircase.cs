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
    [ExportMetadata("AreaType", "Staircase")]
    public class Staircase : IArea
    {
        public Point Position { get; set; }
        public Point Dimension { get; set; } = new Point(1, 1);
        public int Capacity { get; set; }
        public Image Art { get; set; } = Properties.Resources.staircase;
        Status IArea.Status { get; set; }

        // Dijkstra search varibles
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();

        private Staircase()
        {
                     
        }

        
        public IArea CreateArea(Point position, int capacity, Point dimension, int clasification)
        {
            Staircase st = new Staircase
            {
                Position = position,
                Capacity = capacity
            };

            return st;
        }

        

    }
}
