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
        public int ID { get; set; }
        public Point Position { get; set; }
        public Size Dimension { get; set; }
        public int Capacity { get; set; } = int.MaxValue;
        public Bitmap Art { get; set; } = Properties.Resources.restaurant;
        public AreaStatus AreaStatus { get; set; }

        public int Duration { get; set; }

        // Dijkstra search varibles
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();

        // event properties
        public List<IMovable> MovablesInRestaurant { get; set; } = new List<IMovable>();
       

        public Restaurant()
        {
          
        }

        public IArea CreateArea()
        {
            return new Restaurant();
        }

        public void SetJsonValues(int id, Point position, int capacity, Size dimension, int classification)
        {
            ID = id;
            Position = position;
            Dimension = dimension;
            Capacity = capacity;
        }

        

        public bool MoveToArea()
        {
            if (Capacity == MovablesInRestaurant.Count)
            {
                return false;
            }
            return true;
        }
    }
}