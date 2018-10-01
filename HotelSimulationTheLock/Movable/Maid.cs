using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    class Maid : IMovable, HotelEventListener
    {
        public void Notify(HotelEvent evt)
        {
            if (evt.EventType.Equals(HotelEventType.CLEANING_EMERGENCY))
            {
                // add to cleaning ques next
            }
        }
    }
}
