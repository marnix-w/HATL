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
    [ExportMetadata("AreaType", "Elevator")]
    public class Elevator : IArea
    {
        public int ID { get; set; }
        public Point Position { get; set; }
        public Size Dimension { get; set; } = new Size(1, 1);
        public int Capacity { get; set; }
        public Bitmap Art { get; set; } = Properties.Resources.elevator_not_pressent;
        public AreaStatus AreaStatus { get; set; }

        public ElevatorCart ElevatorCart { get; set; }

        // Dijkstra search variables
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();
       
        public Elevator()
        {

        }

        /// <summary>
        /// Creates a new IArea
        /// </summary>
        /// <returns>A new Elevator</returns>
        public IArea CreateArea()
        {
            return new Elevator();
        }

        /// <summary>
        /// Sets values from the given json file
        /// </summary>
        /// <param name="id">ID of the area</param>
        /// <param name="position">Position of the area in the hotel</param>
        /// <param name="capacity">Capacity of the area</param>
        /// <param name="dimension">Dimension of the area</param>
        /// <param name="classification">Classification of the area</param>
        public void SetJsonValues(int id, Point position, int capacity, Size dimension, int classification)
        {
            Position = position;
        }
        
    }
}