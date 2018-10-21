using System;
using System.Collections.Generic;
using System.Drawing;
using HotelEvents;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// The receptionist of the hotel
    /// </summary>
    public class Receptionist : IMovable, IListener
    {
        #region Properties
        /// <summary>
        /// The receptionists position
        /// </summary>
        public Point Position { get; set; }
        /// <summary>
        /// The movable's art
        /// </summary>
        public Bitmap Art { get; set; } = Properties.Resources.receptionist;
        /// <summary>
        /// The movable's status
        /// </summary>
        public MovableStatus Status { get; set; }
        /// <summary>
        /// The movable's area
        /// </summary>
        public IArea Area { get; set; }
        /// <summary>
        /// The movable's action list
        /// </summary>
        public Dictionary<MovableStatus, Action> Actions { get; set; }
        /// <summary>
        /// The hotel the movable belongs to
        /// </summary>
        public Hotel Hotel { get; set; }
        /// <summary>
        /// The movable's final destination
        /// </summary>
        public IArea FinalDes { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor used for unit testing
        /// </summary>
        public Receptionist()
        {

        }

        /// <summary>
        /// Creates an receptionist that can work with the hotel
        /// </summary>
        /// <param name="position">The position of the receptionist</param>
        /// <param name="hotel">The hotel in which the receptionist is located</param>
        public Receptionist(Point position, Hotel hotel)
        {
            Position = position;
            Hotel = hotel;
            Area = hotel.GetArea(typeof(Reception));
            Status = MovableStatus.IDLE;
            HotelEventManager.Register(this);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles an evacuate event
        /// </summary>
        /// <param name="evt">The given event</param>
        public void Notify(HotelEvent evt)
        {
            if (evt.EventType == HotelEventType.EVACUATE)
            {
                Status = MovableStatus.EVACUATING;
            }
        }

        /// <summary>
        /// Implementation for evacuate event
        /// </summary>
        public void PerformAction()
        {
            if (Status == MovableStatus.EVACUATING && Hotel.IsHotelSafe())
            {
                Status = MovableStatus.IDLE;
            }
        }

        /// <summary>
        /// Set the movables path
        /// </summary>
        /// <param name="destination">The destination the movable wants to go to</param>
        public void SetPath(IArea destination)
        {
        }
        #endregion
    }
}
