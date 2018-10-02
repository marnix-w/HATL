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
    public class Reception : IArea, HotelEventListener
    {
        public Point Position { get; set; } = new Point(0, 1); // LOOK FOR Y OR X (nu als xy)
        public Point Dimension { get; set; } = new Point(1, 1);
        public int Capacity { get; set; } = 1;
        public Image Art { get; set; } = Properties.Resources.reception;
        Status IArea.Status { get; set; }

        public Reception()
        {
            HotelEventManager.Register(this);
        }

        public IArea CreateArea(Point position, int capacity, Point dimension, int clasification)
        {
            return new Reception();
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
