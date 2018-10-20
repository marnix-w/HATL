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
    [ExportMetadata("AreaType", "Room")]
    public class Room : IArea
    {
        public int ID { get; set; }
        public Point Position { get; set; }
        public Size Dimension { get; set; }
        public int Capacity { get; set; } = int.MaxValue;
        public int Classification { get; set; }
        public Bitmap Art { get; set; }
        public AreaStatus AreaStatus { get; set; }

        // Dijkstra search variables
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();
        public List<IMovable> Movables { get; set; } = new List<IMovable>();


        /// <summary>
        /// Creates a new IArea
        /// </summary>
        /// <returns>A new Room</returns>
        public IArea CreateArea()
        {
            return new Room();
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
            Dimension = dimension;
            Classification = classification;

            switch (classification)
            {
                case 1:
                    Art = Properties.Resources.room_one_star_open;
                    break;
                case 2:
                    Art = Properties.Resources.room_two_star_open;
                    break;
                case 3:
                    Art = Properties.Resources.room_three_star_open;
                    break;
                case 4:
                    Art = Properties.Resources.room_four_star_open;
                    break;
                case 5:
                    Art = Properties.Resources.room_five_star_open;
                    break;
                default:
                    break;
            }
        }

        

    }
}