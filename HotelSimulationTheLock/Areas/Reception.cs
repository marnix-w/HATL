using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// <para>A specific implementation of an IArea.</para>
    /// <para>An IArea that handles 1 Checkin at a time.</para>
    /// <para>Metadata: "AreaType", "Reception".</para>
    /// </summary>
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Reception")]
    public class Reception : IArea
    {       
        #region IArea properties
        /// <summary>
        /// A specific identifier for an IArea, this must be unique.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The Position where the IArea is located in the hotel, This must be unique.
        /// </summary>
        public Point Position { get; set; }
        /// <summary>
        /// The Size of the IArea.
        /// </summary>
        public Size Dimension { get; set; } = new Size(1, 1);
        /// <summary>
        /// The amount of movables that are allowed to enter the area.
        /// </summary>
        public int Capacity { get; set; } = 2;
        /// <summary>
        /// The Art that represents the IArea.
        /// </summary>
        public Bitmap Art { get; set; } = Properties.Resources.reception;
        /// <summary>
        /// An enumarator the provides a status for an area.
        /// </summary>
        public AreaStatus AreaStatus { get; set; }
        #endregion

        #region Dijkstra search properties

        // We wanted to implement a more generic version of dijkstra
        // using an ISearchable interface. this is somthing to add in the future

        // BackTrackCost, NearestToStart and visted
        // will be reset every time dijkstra has ran its GetShortestPath() function

        /// <summary>
        /// A number which is used for calculating the shortest path.
        /// </summary>
        public double? BackTrackCost { get; set; } = null; // This is double so the future ISearchable can be more reusable.
        /// <summary>
        /// The ISearchable neighbour that is closest to the start.
        /// </summary>
        public IArea NearestToStart { get; set; } = null;
        /// <summary>
        /// Determines whether this ISearchable has been visted.
        /// </summary>
        public bool Visited { get; set; } = false;
        /// <summary>
        /// A collection of connections that the ISearchable has.
        /// </summary>
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>(); // IArea will be changed to ISearchable in the future.
        #endregion

        // Reception Properties:
        /// <summary>
        /// The Queue of guests waiting to be checked in
        /// </summary>
        public Queue<IMovable> CheckInQueue { get; set; } = new Queue<IMovable>();

        /// <summary>
        /// The AreaFactory creation method.
        /// This creates and initializes a new IArea.
        /// </summary>
        /// <returns>A new elevator</returns>
        public IArea CreateArea()
        {
            return new Reception();
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

        /// <summary>
        /// Checks whether the IMovable is the next one in the CheckInQueue
        /// </summary>
        /// <param name="movable">The IMovable that wants to check in</param>
        /// <returns>Whether the IMovable can enter the reception area to check in</returns>
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