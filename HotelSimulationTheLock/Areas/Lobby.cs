using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Lobby")]
    public class Lobby : IArea
    {
        public int ID { get; set; }
        public Point Position { get; set; }
        public Size Dimension { get; set; } = new Size(1, 1);
        public int Capacity { get; set; } = int.MaxValue;
        public Bitmap Art { get; set; } = Properties.Resources.lobby_window;
        public AreaStatus AreaStatus { get; set; }

        // Dijkstra variables
        public double? BackTrackCost { get; set; }
        public IArea NearestToStart { get; set; }
        public bool Visited { get; set; }
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();
 

        /// <summary>
        /// Creates a new IArea
        /// </summary>
        /// <returns></returns>
        public IArea CreateArea()
        {
            return new Lobby();

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

            if (classification % 2 == 0)
            {
                Art = Properties.Resources.lobby_couch;
            }

        }
        
    }
}