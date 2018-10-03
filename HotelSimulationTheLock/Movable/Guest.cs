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
    class Guest : IMovable, HotelEventListener
    {
        public Point Position { get; set; }
        public PictureBox Art { get; set; }
        public MovableStatus Status { get; set; }
        public string Name { get; set; }
        public int RoomRequest { get; set; }

      

        public Guest(string name, int roomRequest)
        {
            Art = new PictureBox();
            RoomRequest = roomRequest;
            Name = name;
            Art.Image = Properties.Resources.customer;
            Art.Location = new Point(0, Hotel.HotelHieght + 1);
        }

        public void Notify(HotelEvent evt)
        {
            switch (evt.EventType)
            {         
                
                // find requested guest

                case HotelEventType.CHECK_OUT:
                    // guest.checkout()
                    break;
                case HotelEventType.EVACUATE:
                    // guest.evacuate()
                    break;
                case HotelEventType.NEED_FOOD:
                    // guest.GoToRestaurant()
                    break;
                case HotelEventType.GOTO_CINEMA:
                    // guest.GoToCinema()
                    break;
                case HotelEventType.GOTO_FITNESS:
                    // guest.GoToFitness()
                    break;
                default:
                    break;
            }
        }
    }
}
