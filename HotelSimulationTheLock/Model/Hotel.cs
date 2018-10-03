using HotelEvents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace HotelSimulationTheLock
{
    public class Hotel : HotelEventListener
    {
        //this list will be filled with IAreas objects
        public List<IArea> HotelAreaList = new List<IArea>();


        //a hotel needs to have a list of current guests 
        public List<IMovable> IMovableList = new List<IMovable>();

        public static int HotelHeight { get; set; }
        public static int HotelWidth { get; set; }

        public int HtePerSecond { get; set; }

        //an hotel has a background image
        public PictureBox Background { get; set; }

        public Hotel(List<JsonModel> layout, float HTESeconds)
        {
            HotelEventManager.HTE_Factor = HTESeconds;



            HotelEventManager.Register(this);


            AreaFactory Factory = new AreaFactory();
            

            foreach (JsonModel i in layout)
            {
                int temp = 0;

                if (i.Classification != null)
                {
                    temp = int.Parse(Regex.Match(i.Classification, @"\d+").Value);
                }


                HotelAreaList.Add(Factory.GetArea(i.AreaType, i.Position, i.Capacity, i.Dimension, temp));
            }

            HotelWidth = HotelAreaList.OrderBy(X => X.Position.X).Last().Position.X;
            HotelHeight = HotelAreaList.OrderBy(Y => Y.Position.Y).Last().Position.Y;
            

            for (int i = 1; i < HotelHeight + 2; i++)
            {
                // 5 is the capacity get from setting screen
                HotelAreaList.Add(Factory.GetArea("Elevator", new Point(0, i), 5, new Point(1, 1), i));
                HotelAreaList.Add(Factory.GetArea("Staircase", new Point(HotelWidth + 1, i), 5, new Point(1, 1), 0));
            }
            for (int i = 1; i < HotelWidth + 1; i++)
            {
                if (i == 1)
                {
                    HotelAreaList.Add(Factory.GetArea("Reception", new Point(1, HotelHeight + 1), 5, new Point(1, 1), 1));
                }                
                else
                {
                    HotelAreaList.Add(Factory.GetArea("Lobby", new Point(i, HotelHeight + 1), 5, new Point(1, 1), i)); // lobby
                }
            }
            

            IMovableList.Add(new Maid());
            IMovableList.Add(new Maid());

            SetNeighbor();
                     

        }

        public Bitmap DrawHotel()
        {
            // all art is 96 * 96 pixels
            Bitmap buffer = new Bitmap((HotelWidth + 2) * 96, (HotelHeight + 1) * 96, PixelFormat.Format16bppRgb565);

            using (Graphics graphics = Graphics.FromImage(buffer))
            {
                foreach (IArea area in HotelAreaList)
                {
                    graphics.DrawImage(area.Art, area.Position.X * 96, (area.Position.Y - 1) * 96, area.Dimension.X * 96, area.Dimension.Y * 96);
                } 
            }
            
            return buffer;

        }

        private void SetNeighbor()
        {
            
            foreach (IArea area in HotelAreaList)
            {

                bool rightSet = false;
                bool leftSet = false;

                for (int i = 1; i < HotelWidth + 1; i++)
                {
                    // right edge
                    if (!rightSet && AddNeihgbor(area, i, 0, i))
                    {
                        rightSet = true;
                        continue;
                    }
                    //left edge
                    if (!leftSet && AddNeihgbor(area, -i, 0, i))
                    {
                        leftSet = true;
                        continue;
                    }
                }
                if (area.Position.X == 0 || area.Position.X == HotelWidth + 1)
                {
                    // add top neighbor
                    AddNeihgbor(area, 0, 1, 1);
                    // add bothem neighbor
                    AddNeihgbor(area, 0, -1, 1);
                }
            }
        }

        private bool AddNeihgbor(IArea area, int xOffset, int yOffset, int wieght)
        {
            if (!(HotelAreaList.Find(X => X.Position == new Point(area.Position.X + xOffset, area.Position.Y + yOffset)) is null))
            {
                area.Edge.Add(HotelAreaList.Find(X => X.Position == new Point(area.Position.X + xOffset, area.Position.Y + yOffset)), wieght);
                return true;
            }
            return false;
        }

        public void Notify(HotelEvent evt)
        {
            if (evt.EventType.Equals(HotelEventType.CHECK_IN))
            {
                
                string name = string.Empty;
                string request = string.Empty;
                int requestInt = 0;

                if (!(evt.Data is null))
                {
                    name = evt.Data.FirstOrDefault().Key;
                    request = evt.Data.FirstOrDefault().Value;

                    requestInt = int.Parse(Regex.Match(request, @"\d+").Value);
                }
                else
                {
                    name = "test guest";
                    request = "no request";
                }

                Guest guest = new Guest(name, requestInt);

                IMovableList.Add(guest);
               
                Console.WriteLine($"new guest = name: {name}, request: {request} ");
            }
        }


    }
}
