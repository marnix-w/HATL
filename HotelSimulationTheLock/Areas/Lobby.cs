using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// <para>A specific implementation of an IArea.</para>
    /// <para>Metadata: "AreaType", "Lobby".</para>
    /// </summary>
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Lobby")]
    public class Lobby : IArea
    {               
        #region  IArea properties
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
        public int Capacity { get; set; }
        /// <summary>
        /// The Art that represents the IArea.
        /// </summary>
        public Bitmap Art { get; set; } = Properties.Resources.lobby_window; // Will be switched on even/uneven in SetJsonValues()
        /// <summary>
        /// An enumarator that provides a status for an area.
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
        /// Determiens whether this ISearchable has been visted.
        /// </summary>
        public bool Visited { get; set; } = false;
        /// <summary>
        /// A collection of connections that the ISearchable has.
        /// </summary>
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>(); // IArea will be changed to ISearchable in the future.
        #endregion

        /// <summary>
        /// Creates a new IArea
        /// </summary>
        /// <returns>A new lobby</returns>
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