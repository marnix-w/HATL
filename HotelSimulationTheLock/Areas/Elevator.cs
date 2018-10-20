using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// <para>An specefic implemantation of an IArea.</para>
    /// <para>Elevator(shaft) is an Iarea used for pathfinding to the Elevator system.</para>
    /// <para>Metadata: "AreaType", "Elevator".</para>
    /// </summary>
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Elevator")]
    public class Elevator : IArea
    {      
        #region IArea properties
        /// <summary>
        /// An Specefic identifier for an IArea, this must be uniqe.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The Position where the IArea stands in the hotel, This must be uniqe.
        /// </summary>
        public Point Position { get; set; }
        /// <summary>
        /// The Size of the IArea.
        /// </summary>
        public Size Dimension { get; set; } = new Size(1, 1);
        /// <summary>
        /// The amount of movabales that are allowed to enter the area.
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// The Art that represents the IArea.
        /// </summary>
        public Bitmap Art { get; set; } = Properties.Resources.elevator_not_pressent;
        /// <summary>
        /// An enumarator the provides a status for a room .
        /// </summary>
        public AreaStatus AreaStatus { get; set; }
        #endregion

        #region Dijkstra search properties
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

        // Elevator properties:
        public ElevatorCart ElevatorCart { get; set; }

        /// <summary>
        /// The AreaFactory creation method.
        /// This creates and initilizes a new IArea.
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
            ID = id;
            Position = position;
        }
        
    }
}