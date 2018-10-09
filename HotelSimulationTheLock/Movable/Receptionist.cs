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
    public class Receptionist : IMovable, HotelEventListener
    {
        public Point Position { get; set; }
        public Bitmap Art { get; set; } = Properties.Resources.receptionist;
        public MovableStatus Status { get; set; }
        public IArea Area { get; set; }

        private Hotel Hotel { get; set; }
        public Dictionary<MovableStatus, Action> Actions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Receptionist(Point position, Hotel hotel)
        {
            Position = position;
            Hotel = hotel;
            Area = hotel.HotelAreas.Find(X => X is Reception);
            Area.Movables.Add(this);

        }

        public void Notify(HotelEvent evt)
        {
            if (evt.EventType.Equals(HotelEventType.EVACUATE))
            {

            }
        }
        
        public IArea GiveThisGuestHisRoom(int classification)
        {
            IArea result = null;

            // upgrade guests room if request is not available
            for (int i = classification; i <= 5; i++)
            {
                if (!(Hotel.GetRoom(i) is null))
                {
                    result = Hotel.GetRoom(i);
                    break;
                }
            }

            return result;
        }
       

        public void RemoveGuest(Guest guest)
        {
            Hotel.RemoveGuest(guest);
        }

        public void PerformAction()
        {
        }

        public void SetPath(IArea destination)
        {
        }
    }
}
