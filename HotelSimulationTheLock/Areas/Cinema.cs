﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using HotelEvents;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// <para>A specific implementation of an IArea.</para>
    /// <para>Cinema Takes care of handling the START_CINEMA event.</para>
    /// <para>Metadata: "AreaType", "Cinema".</para>
    /// </summary>
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Cinema")]
    public class Cinema : IArea, IListener
    {            
        #region IArea properties
        /// <summary>
        /// A specific identifier for an IArea, this must be unique.
        /// </summary>
        public int ID { get; set; } // We didn't have the time to ensure this number would be unique.
        /// <summary>
        /// The Position where the IArea is located in the hotel, This must be unique.
        /// </summary>
        public Point Position { get; set; } // We didn't have the time to ensure this number would be unique.
        /// <summary>
        /// The Size of the IArea.
        /// </summary>
        public Size Dimension { get; set; } // It's usage is for setting the neigbors.
        /// <summary>
        /// The amount of movables that are allowed to enter the area.
        /// </summary>
        public int Capacity { get; set; } = 20; // This is not fully implemented but can be in the future.
        /// <summary>
        /// The Art that represents the IArea.
        /// </summary>
        public Bitmap Art { get; set; } = Properties.Resources.cinema;
        /// <summary>
        /// An enumarator that provides a status for an area.
        /// </summary>
        public AreaStatus AreaStatus { get; set; } // Currently all statuses can be used for rooms, this might change in the future.       
        #endregion

        #region Dijkstra search properties

        // We wanted to implement a more generic version of dijkstra
        // using an ISearchable interface. this is somthing to add in the future

        // BackTrackCost, NearestToStart and visted
        // will be reset every time dijkstra has ran its GetShortestPath() function

        /// <summary>
        /// A number wich is used for calculating the shortest path.
        /// </summary>
        public double? BackTrackCost { get; set; } = null; // This is double so the future ISearchable can be more reusable.
        /// <summary>
        /// The ISearchable that is closest to the start.
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

        #region Cinema Properties
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
        /// The AreaFactory creation method.
        /// This creates and initilizes a new IArea.
        /// </summary>
        /// <returns>A new Cinema</returns>
        public IArea CreateArea()
        {
            Cinema newCinema = new Cinema();
            HotelEventManager.Register(newCinema);
            return newCinema;
        }

        /// <summary>
        /// Sets values from the given json file
        /// Used by the Json Hotelbuilder
        /// </summary>
        /// <param name="id">ID of the area</param>
        /// <param name="position">Position of the area in the hotel</param>
        /// <param name="capacity">Capacity of the area</param>
        /// <param name="dimension">Dimension of the area</param>
        /// <param name="classification">Classification of the area</param>
        public void SetJsonValues(int id, Point position, int capacity, Size dimension, int classification)
        {
            // This method is not very expendable
            // a possibility is to pass a collection so it can work with other IHotelBuilders

            ID = id;
            Position = position;
            Dimension = dimension;
        }

        /// <summary>
        /// Performs the event handling
        /// </summary>
        /// <param name="evt">The given event</param>
        public void Notify(HotelEvent evt)
        {
            // Checks for a START_CINEMA event.
            // it changes its status and visualize(plays) a movie
            if (evt.EventType.Equals(HotelEventType.START_CINEMA))
            {
                AreaStatus = AreaStatus.PLAYING_MOVIE;
                Art = Properties.Resources.cinem_playinga;                
            }
        }

        
    }
}