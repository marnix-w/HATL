using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// <para>An specefic implemantation of an IArea.</para>
    /// <para>An IA that houses IMovables</para>
    /// <para>Metadata: "AreaType", "Room".</para>
    /// </summary>
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Room")]
    public class Room : IArea
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
        public Size Dimension { get; set; }
        /// <summary>
        /// The amount of movabales that are allowed to enter the area.
        /// </summary>
        public int Capacity { get; set; } = 1;
        /// <summary>
        /// The Art that represents the IArea.
        /// </summary>
        public Bitmap Art { get; set; }
        /// <summary>
        /// An enumarator the provides a status for a room.
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

        // Room properties:
        public int Classification { get; set; }

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

            // Checks the classefecation and sets the correct art
            // if the classification is out of bound it creates a 1 star room
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
                    Art = Properties.Resources.room_one_star_open;
                    Classification = 1;
                    break;
            }
        }

        

    }
}