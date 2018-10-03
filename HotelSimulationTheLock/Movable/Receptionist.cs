using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    class Receptionist : IMovable, HotelEventListener
    {
        public Point Position { get; set; }
        public Image Art { get; set; }
        public MovableStatus Status { get; set; }
        public void Notify(HotelEvent evt)
        {
            if (evt.EventType.Equals(HotelEventType.EVACUATE))
            {

            }
        }
    }
}
