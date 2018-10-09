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
    public class Maid : IMovable, HotelEventListener
    {
        public Point Position { get; set; }
        public Bitmap Art { get; set; } = Properties.Resources.maid;
        public MovableStatus Status { get; set; }
        Queue<HotelEvent> CleaningEmergencies { get; set; }
        public IArea Area { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Dictionary<MovableStatus, Action> Actions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Maid(Point startLocation)
        {
            Position = startLocation;


            // CHANGE
            
        }

        public void Notify(HotelEvent evt)
        {         

            if (evt.EventType.Equals(HotelEventType.CLEANING_EMERGENCY))
            {
                // add to cleaning ques next
            }
            else if (evt.EventType.Equals(HotelEventType.CHECK_OUT))
            {
                // clean the room
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
