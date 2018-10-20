This is the location to add new IArea implemtations.
Follow the correct format for it to work.
if the correct format is not followd it will not load

# Begin format #

    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "[Name of Area (unique)]")]
    public class [Name of Area]: IArea
    {
        // IArea properties implementation:        
        #region
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
        public int Capacity { get; set; } = 20;
        /// <summary>
        /// The Art that represents the IArea.
        /// </summary>
        public Bitmap Art { get; set; } = Properties.Resources.fitness;
        /// <summary>
        /// An enumarator the provides a status for a room .
        /// </summary>
        public AreaStatus AreaStatus { get; set; }
        #endregion

        // Dijkstra search properties:
        #region
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

        /// <summary>
        /// Creates a new IArea
        /// </summary>
        /// <returns></returns>
        public IArea CreateArea()
        {
            return new [Name of Area]();
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
            // Write implementation
        }

    }

# End Format #