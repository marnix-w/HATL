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
    public class ElevatorRequest
    {
        /// <summary>
        /// Up = true, Down = false;
        /// </summary>
        public bool UpOrDown { get; set; }


    }


    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Elevator")]
    public class Elevator : IArea
    {
        public Point Position { get; set; }
        public Point Dimension { get; set; }
        public int Capacity { get; set; }
        public Image Art { get; set; } = Properties.Resources.elevator_not_pressent;
        Status IArea.Status { get; set; }
        
        

        // Dijkstra search varibles
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();

        private Elevator()
        {
            
        }

        public IArea CreateArea(Point position, int capacity, Point dimension, int clasification)
        {
            Elevator ev = new Elevator
            {
                Position = position,
                Capacity = capacity,
                Dimension = dimension
            };
            
            return ev;
        }

        

    }
}
