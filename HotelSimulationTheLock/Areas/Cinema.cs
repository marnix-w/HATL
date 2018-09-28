using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    class Cinema : IArea, HotelEventListener
    {


        public void Notify(HotelEvent evt)
        {
            if (evt.EventType.Equals(HotelEventType.STAR_CINEMA))
            {

            }
        }

        }
}
