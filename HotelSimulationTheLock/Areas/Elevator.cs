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
        public Size Dimension { get; set; } = new Size(1, 1);
        public int Capacity { get; set; }
        public Bitmap Art { get; set; } = Properties.Resources.elevator_not_pressent;
        public AreaStatus AreaStatus { get; set; }
        
        

        // Dijkstra search varibles
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();
        public List<IMovable> Movables { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Elevator()
        {
            
        }

        public IArea CreateArea()
        {
            return new Elevator();
        }

        public void SetJsonValues(Point position, int capacity, Size dimension, int classification)
        {
            Position = position;            
        }

        public bool AddMovable(IMovable movable)
        {
            throw new NotImplementedException();
        }
    }
}
