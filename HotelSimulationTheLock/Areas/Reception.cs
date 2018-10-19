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
    [ExportMetadata("AreaType", "Reception")]
    public class Reception : IArea
    {
        public int ID { get ; set ; }
        public Point Position { get; set; }
        public Size Dimension { get; set; } = new Size(1, 1);
        public int Capacity { get; set; } = 2;
        public Bitmap Art { get; set; } = Properties.Resources.reception;
        public AreaStatus AreaStatus { get; set; }

        // Dijkstra search varibles
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();

        // event properties
        public Receptionist Receptionist { get; set; }
        public Queue<IMovable> CheckInQueue { get; set; } = new Queue<IMovable>();
       

        public Reception()
        {

        }

        public IArea CreateArea()
        {
            return new Reception();
        }

        public void SetJsonValues(int id, Point position, int capacity, Size dimension, int classification)
        {
            ID = id;
            Position = position;
        }

        public bool EnterArea(IMovable movable)
        {
            if (CheckInQueue.Any() && CheckInQueue.First() == movable)
            {
                return true;
            }
            return false;
        }
 
    }
}