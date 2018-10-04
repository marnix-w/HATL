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
    public class Guest : IMovable, HotelEventListener
    {
        public Random rng = new Random();
        public Point Position { get; set; }
        public PictureBox Art { get; set; }
        public MovableStatus Status { get; set; }
        public Label bobsname { get; set; }
      
        public string Name { get; set; }
        public int RoomRequest { get; set; }
        public IArea area { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
       
        public Guest(string name, int roomRequest, int x, int y)
        {
          
          
            // TO BE CHANGED NO PIXTURE BOX SPAM
            Art = new PictureBox();
            RoomRequest = roomRequest;
            Name = name;
            Art.Image = Properties.Resources.customer;
            Art.SizeMode = PictureBoxSizeMode.AutoSize;
            Position = new Point(x, y);
            Art.Location = Position;

            bobsname = new Label();
            bobsname.Text = Name;
            bobsname.Location = new Point(x, y - 30);
            bobsname.Width = 30;
            bobsname.Height = 15;
            bobsname.ForeColor = Color.Red;
            bobsname.BackColor = Color.Transparent;
        }

        public void MoveCustomer(Guest guest)
        {
            Position = new Point(guest.Position.X + rng.Next(1,100), guest.Position.Y);
            Art.Location = Position;
            bobsname.Location = new Point(Position.X - 10 , Position.Y - 25);
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
