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
    public class Cinema : IArea, IListner
    {
        public int ID { get; set; }
        // Properties
        public Point Position { get; set; }
        public Size Dimension { get; set; }
        public int Capacity { get; set; } = 11;
        public Bitmap Art { get; set; } = Properties.Resources.cinema;
        public int Duration { get; set; }
        public AreaStatus AreaStatus { get; set; }

        // Dijkstra search variables
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();
        public List<IMovable> MovablesInCinema { get; set; } = new List<IMovable>();              

  

        /// <summary>
        /// Creates a new IArea and registers that IArea whith the HotelEventManager
        /// </summary>
        /// <returns>A new Cinema</returns>
        public IArea CreateArea()
        {
            Cinema c = new Cinema();
            HotelEventManager.Register(c);
            return c;
        }

        /// <summary>
        /// Checks wheter the given event is a cinema event and in that case starts the event
        /// </summary>
        /// <param name="evt">The given event</param>
        public void Notify(HotelEvent evt)
        {
            if (evt.EventType.Equals(HotelEventType.START_CINEMA))
            {
                AreaStatus = AreaStatus.PLAYING_MOVIE;
                Art = Properties.Resources.cinem_playinga;                
            }
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
        }
    }
}