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
        /// The receptionist position
        /// </summary>
        public Point Position { get; set; }
        /// <summary>
        /// The movable his art
        /// </summary>
        public Bitmap Art { get; set; } = Properties.Resources.receptionist;
        /// <summary>
        /// The moable his status
        /// </summary>
        public MovableStatus Status { get; set; }
        /// <summary>
        /// The movable his area
        /// </summary>
        public IArea Area { get; set; }
        /// <summary>
        /// The movables action list
        /// </summary>
        public Dictionary<MovableStatus, Action> Actions { get; set; }
        /// <summary>
        /// The hotel he belongs to
        /// </summary>
        public Hotel Hotel { get; set; }
        /// <summary>
        /// Its final destination
        /// </summary>
        public IArea FinalDes { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor used for unit testinf
        /// </summary>
        public Receptionist()
        {

        }

        /// <summary>
        /// Creates an receptionist that can work with the hotel
        /// </summary>
        /// <param name="position"></param>
        /// <param name="hotel"></param>
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
        /// <param name="evt"></param>
        public void Notify(HotelEvent evt)
        {
            if (evt.EventType == HotelEventType.EVACUATE)
            {
                Status = MovableStatus.EVACUATING;
            }
        }

        /// <summary>
        /// Implemantation for evacuate event
        /// </summary>
        public void PerformAction()
        {
            if (Status == MovableStatus.EVACUATING && Hotel.IsHotelSafe())
            {
                Status = MovableStatus.IDLE;
            }
        }

        /// <summary>
        /// Seth his path function
        /// </summary>
        /// <param name="destination"></param>
        public void SetPath(IArea destination)
        {
        }
        #endregion
    }
}
