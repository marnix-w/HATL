using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    class Reception : IArea
    {

        public Reception()
        {
            HotelEventManager.Register(this);
        }

        public void Notify(HotelEvent evt)
        {
            if (evt.EventType.Equals(HotelEventType.CHECK_IN))
            {
                string name = "";
                string request = "";

                if (!(evt.Data is null))
                {
                    name = evt.Data.FirstOrDefault().Key;
                    request = evt.Data.FirstOrDefault().Value;
                }
                else
                {
                    name = "test guest";
                    request = "no request";
                }
                 
                Guest guest = new Guest(name, request);

                Debug.WriteLine($"new guest = name: {name}, request: {request} ");
            }
        }
    }
}
