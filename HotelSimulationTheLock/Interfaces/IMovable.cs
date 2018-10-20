using System;
using System.Collections.Generic;
using System.Drawing;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// The status that a movable can have
    /// based on their status the correct behavior is chosen
    /// </summary>
    public enum MovableStatus
    {        
        CHEKING_IN,
        IN_ROOM,
        GOING_TO_ROOM,
        LEAVING,
        GET_FOOD,
        IN_ELEVATOR,
        EATING,
        WORKING_OUT,
        EVACUATING,
        WATCHING,
        GOING_TO_CINEMA,
        GOING_TO_FITNESS,
        CHECKING_OUT,
        WAITING_TO_START,        
        ELEVATOR_REQUEST,
        LEAVING_ELEVATOR,
        WAITING_FOR_ELEVATOR,
        UP,
        DOWN,
        IDLE,
        CLEANING
    }

    /// <summary>
    /// The movable items inside of the hotel
    /// </summary>
    public interface IMovable
    {
        #region Movable properties
        /// <summary>
        /// The Movable its current position
        /// </summary>
        IArea Area { get; set; }
        /// <summary>
        /// The final destanation a movable is going
        /// </summary>
        IArea FinalDes { get; set; }
        /// <summary>
        /// The position of the movable
        /// This coresponds with the Area
        /// </summary>
        Point Position { get; set; } // this is used for drawing and the elevatorcart
        /// <summary>
        /// The vizual representation of the movable
        /// </summary>
        Bitmap Art { get; set; } // Bob, Barbra or cool bob
        /// <summary>
        /// The status of the movable 
        /// </summary>
        MovableStatus Status { get; set; }
        /// <summary>
        /// The List of actions a movable can perfrom
        /// </summary>
        Dictionary<MovableStatus, Action> Actions { get; set; } // This list uses the status and an delegate to handle bahavoir
        /// <summary>
        /// The movable knows the hotel so it can find its way around
        /// and call for varius types of data
        /// </summary>
        Hotel Hotel { get; set; }
        #endregion
        
        #region Methods
        /// <summary>
        /// Calls the Actions list and performs an action
        /// </summary>
        void PerformAction();
        /// <summary>
        /// Sets a path truh calling dijkstra to an destanation
        /// </summary>
        void SetPath(IArea destination);
        #endregion
    }
}
