using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using HotelEvents;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// <para>An specefic implemantation of an IArea.</para>
    /// <para>Cinema Takes care of handeling the START_CINEMA event.</para>
    /// <para>Metadata: "AreaType", "Cinema".</para>
    /// </summary>
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Cinema")]
    public class Cinema : IArea, IListner
    {
        // IArea properties implementation:        
        #region
        /// <summary>
        /// An Specefic identifier for an IArea, this must be uniqe.
        /// </summary>
        public int ID { get; set; } // We didn't have the time to ensure this number would be unique.
        /// <summary>
        /// The Position where the IArea stands in the hotel, This must be uniqe.
        /// </summary>
        public Point Position { get; set; } // We didn't have the time to ensure this number would be unique.
        /// <summary>
        /// The Size of the IArea.
        /// </summary>
        public Size Dimension { get; set; } // It's usage is for setting the neigbors.
        /// <summary>
        /// The amount of movabales that are allowed to enter the area.
        /// </summary>
        public int Capacity { get; set; } = 20; // This is not fully implemented but can be in the future.
        /// <summary>
        /// The Art that represents the IArea.
        /// </summary>
        public Bitmap Art { get; set; } = Properties.Resources.cinema;
        /// <summary>
        /// An enumarator the provides a status for a room .
        /// </summary>
        public AreaStatus AreaStatus { get; set; } // Currently all statuses can be used for rooms, this might change in the future.       
        #endregion

        // Dijkstra search properties
        #region

        // We wanted to implement a more generic version of dijkstra
        // using an ISearchable interface. this is somthing to add in the future

        // BackTrackCost, NeasestToStart and visted
        // will be reset everytime dijkstra has ran it GetShortestPath() function

        /// <summary>
        /// A number wich is used for calculating the shortest path.
        /// </summary>
        public double? BackTrackCost { get; set; } = null; // This is double so the future ISearchable can be more reusable.
        /// <summary>
        /// The ISerachable that is closest to the starting from this current ISearchable.
        /// </summary>
        public IArea NearestToStart { get; set; } = null;
        /// <summary>
        /// Deterimens wheter this ISearchable has been visted.
        /// </summary>
        public bool Visited { get; set; } = false;
        /// <summary>
        /// An collection of connection that the ISearchable has.
        /// </summary>
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>(); // IArea will be changed to ISearchable in the future.
        #endregion

        // Cinema Properties
        #region
        /// <summary>
        /// The duration in HTE of an movie
        /// </summary>
        public int Duration { get; set; } // Currently timeing is handeld in guest, it should be moved to Cinema
        
        // This can be used to cap the cinema on its capicity

        /// <summary>
        /// The Amount of Viewers in the cinema    
        /// </summary>
        // public List<IMovable> MovablesInCinema { get; set; } = new List<IMovable>();      
        #endregion
            
        /// <summary>
        /// Creates a new IArea and registers that IArea whith the HotelEventManager
        /// </summary>
        /// <returns>A new Cinema</returns>
        public IArea CreateArea()
        {
            Cinema newCinema = new Cinema();
            HotelEventManager.Register(newCinema);
            return newCinema;
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