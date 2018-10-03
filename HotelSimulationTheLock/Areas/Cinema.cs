using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Cinema")]
    public class Cinema : IArea, HotelEventListener
    {
        // Properties
        public Point Position { get; set; }
        public Point Dimension { get; set; }
        public int Capacity { get; set; }
        public Image Art { get; set; } = Properties.Resources.cinema;
        AreaStatus IArea.AreaStatus { get; set; }

        // Dijkstra search varibles
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();

        private Cinema()
        {
            HotelEventManager.Register(this);           
        }

        public IArea CreateArea(Point position, int capacity, Point dimension, int clasification)
        {
            Cinema cm = new Cinema
            {
                Position = position,
                Capacity = capacity,
                Dimension = dimension
            };

            return cm;
        }
        
        public void Notify(HotelEvent evt)
        {
            if (evt.EventType.Equals(HotelEventType.STAR_CINEMA))
            {

            }
        }

    }
}
