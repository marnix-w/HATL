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
        public int ID { get; set; }
        public Point Position { get; set; }
        public Size Dimension { get; set; } = new Size(1, 1);
        public int Capacity { get; set; } = int.MaxValue;
        public Bitmap Art { get; set; } = Properties.Resources.staircase;
        public AreaStatus AreaStatus { get; set; }

        // Dijkstra search variables
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();
        public List<IMovable> Movables { get; set; } = new List<IMovable>();

<<<<<<< HEAD
        public Staircase()
        {

        }

        /// <summary>
        /// Creates a new IArea
        /// </summary>
        /// <returns>A new Staircase</returns>
=======
>>>>>>> hotel-team
        public IArea CreateArea()
        {
            return new Staircase();
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
            ID = id;
            Position = position;
        }

<<<<<<< HEAD
        /// <summary>
        /// Checks wheter the capacity of the area allows a new IMovable to enter
        /// </summary>
        /// <returns>Wheter the IMovable can enter the area</returns>
        public bool MoveToArea()
        {
            if (Capacity == Movables.Count)
            {
                return false;
            }
            return true;
        }
=======
>>>>>>> hotel-team
    }
}