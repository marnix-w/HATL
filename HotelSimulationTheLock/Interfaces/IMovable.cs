using System;
using System.Collections.Generic;
using System.Drawing;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// The status that a movable can have
    /// based on their status the correct behavoir is chosen
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
        NOONE_INSIDE,
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
        // area status
        IArea Area { get; set; }

        // properties
        Point Position { get; set; }
        Bitmap Art { get; set; }
        MovableStatus Status { get; set; }
        Dictionary<MovableStatus, Action> Actions { get; set; }

        Hotel Hotel { get; set; }

        void PerformAction();
        IArea FinalDes { get; set; }
        void SetPath(IArea destination);

    }
}
