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
    [ExportMetadata("AreaType", "Fitness")]
    public class Fitness : IArea
    {
        public Point Position { get; set; }
        public Size Dimension { get; set; }
        public int Capacity { get; set; }
        public Bitmap Art { get; set; } = Properties.Resources.fitness;
        public AreaStatus AreaStatus { get; set; }

        // Dijkstra search varibles
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();
        public List<IMovable> Movables { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Fitness()
        {
            
        }

        public IArea CreateArea()
        {
            return new Fitness();
        }

        public void SetJsonValues(Point position, int capacity, Size dimension, int classification)
        {
            Position = position;
            Dimension = dimension;
        }

        public bool AddMovable(IMovable movable)
        {
            throw new NotImplementedException();
        }
    }
}