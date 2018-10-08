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
        public Bitmap Art { get; set; }
        public MovableStatus Status { get; set; }
        public IArea Area { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Receptionist()
        {
            
        }

        public void Notify(HotelEvent evt)
        {
            if (evt.EventType.Equals(HotelEventType.EVACUATE))
            {

            }
        }

        public void PerformAction()
        {
        }

        public void SetPath(IArea destination)
        {
        }
    }
}
