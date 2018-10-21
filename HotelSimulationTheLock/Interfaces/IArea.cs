using System.Collections.Generic;
using System.Drawing;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// The enumarator that sets a status for an IArea
    /// </summary>
    public enum AreaStatus
    {
        EMPTY,
        OCCUPIED,
        NEED_CLEANING,
        IN_CLEANING_QUEUE,
        PLAYING_MOVIE
    }

    /// <summary>
    /// A identifier for an IArea implementation
    /// </summary>
    public interface IAreaType
    {
        // This Identifier is currently not unique and can cause problems
        // This is something to work on in the future
        string AreaType { get; }
    }

    /// <summary>
    /// The generic declaration for an area in the hotel
    /// </summary>
    public interface IArea
    {           
        #region IArea properties
        /// <summary>
        /// A specific identifier for an IArea, this must be unique.
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// The Position where the IArea is located in the hotel. This must be unique.
        /// </summary>
        Point Position { get; set; }
        /// <summary>
        /// The Size of the IArea.
        /// </summary>
        Size Dimension { get; set; }
        /// <summary>
        /// The amount of movables that are allowed to enter the area.
        /// </summary>
        int Capacity { get; set; }
        /// <summary>
        /// The Art that represents the IArea.
        /// </summary>
        Bitmap Art { get; set; }
        /// <summary>
        /// An enumarator that provides a status for an area.
        /// </summary>
        AreaStatus AreaStatus { get; set; }
        #endregion

        #region Dijkstra search properties

        // We wanted to implement a more generic version of dijkstra
        // using an ISearchable interface. this is something to add in the future

        // BackTrackCost, NeasestToStart and visited
        // will be reset every time dijkstra has ran its GetShortestPath() function

        /// <summary>
        /// A number which is used for calculating the shortest path.
        /// </summary>
        double? BackTrackCost { get; set; } // This is double so the future ISearchable can be more reusable.
        /// <summary>
        /// The ISerachable neighbour that is closest to the start.
        /// </summary>
        IArea NearestToStart { get; set; }
        /// <summary>
        /// Determines whether this ISearchable has been visited.
        /// </summary>
        bool Visited { get; set; }
        /// <summary>
        /// A collection of connections that the ISearchable has.
        /// </summary>
        Dictionary<IArea, int> Edge { get; set; } // IArea will be changed to ISearchable in the future.
        #endregion

        /// <summary>
        /// An custom constructor to be used with the AreaFactory
        /// </summary>
        /// <returns></returns>
        IArea CreateArea();

        /// <summary>
        /// Sets values from the given json file
        /// </summary>
        /// <param name="id">ID of the area</param>
        /// <param name="position">Position of the area in the hotel</param>
        /// <param name="capacity">Capacity of the area</param>
        /// <param name="dimension">Dimension of the area</param>
        /// <param name="classification">Classification of the area</param>
        void SetJsonValues(int id, Point position, int capacity, Size dimension, int classification);
        
    }
}