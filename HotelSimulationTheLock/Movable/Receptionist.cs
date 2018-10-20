using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelEvents;

namespace HotelSimulationTheLock
{
    public class Receptionist : IMovable, IListner
    {
        public Point Position { get; set; }
        public Bitmap Art { get; set; } = Properties.Resources.receptionist;
        public MovableStatus Status { get; set; }
        public IArea Area { get; set; }
        
        public Dictionary<MovableStatus, Action> Actions { get; set; }

        public Hotel Hotel { get; set; }
        public IArea FinalDes { get; set; }

        public Receptionist()
        {

        }

        public Receptionist(Point position, Hotel hotel)
        {
            Position = position;
            Hotel = hotel;
            Area = hotel.GetArea(typeof(Reception));
            Status = MovableStatus.IDLE;
            HotelEventManager.Register(this);
        }

        public void Notify(HotelEvent evt)
        {
            if (evt.EventType == HotelEventType.EVACUATE)
            {
                Status = MovableStatus.EVACUATING;
            }
        }

        public void PerformAction()
        {
            if (Status == MovableStatus.EVACUATING && Hotel.IsHotelSafe())
            {
                Status = MovableStatus.IDLE;
            }
        }

        public void SetPath(IArea destination)
        {
        }
    }
}
