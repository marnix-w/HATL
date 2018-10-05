using HotelEvents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        public List<IArea> HotelAreas = new List<IArea>();


        //a hotel needs to have a list of current guests 
        public List<IMovable> HotelMovables = new List<IMovable>();

        public static int HotelHeight { get; set; }
        public static int HotelWidth { get; set; }        
        public Bitmap HotelBackground { get; set; } 



        public Hotel(List<JsonModel> layout, List<dynamic> SettingsDataSet)
        {
            // Hotel Will handle the checkins (might move this to reception?)
            HotelEventManager.Register(this);

            // Create a factory to make the rooms (factory actualy singelton?)
            AreaFactory Factory = new AreaFactory();
            
            // read out the json and add rooms to the layout
            foreach (JsonModel i in layout)
            {
                int clasifactionNum = 0;

                if (i.Classification != null)
                {
                    clasifactionNum = int.Parse(Regex.Match(i.Classification, @"\d+").Value);
                }

                IArea area = Factory.GetArea(i.AreaType);
                area.SetJsonValues(i.Position, i.Capacity, i.Dimension, clasifactionNum);
                HotelAreas.Add(area);
            }

            HotelWidth = HotelAreas.OrderBy(X => X.Position.X).Last().Position.X;
            HotelHeight = HotelAreas.OrderBy(Y => Y.Position.Y).Last().Position.Y;


            for (int i = 1; i < HotelHeight + 2; i++)
            {
                // 5 is the capacity get from setting screen
                IArea elevator = Factory.GetArea("Elevator");
                IArea staircase = Factory.GetArea("Staircase");

                elevator.SetJsonValues(new Point(0, i), 5, new Size(1, 1), i);
                staircase.SetJsonValues(new Point(HotelWidth + 1, i), 5, new Size(1, 1), 0);

                HotelAreas.Add(elevator);
                HotelAreas.Add(staircase);
            }
            for (int i = 1; i < HotelWidth + 1; i++)
            {
                if (i == 1)
                {
                    IArea reception = Factory.GetArea("Reception");

                    reception.SetJsonValues(new Point(1, HotelHeight + 1), 5, new Size(1, 1), 1);

                    HotelAreas.Add(reception);
                }
                else
                {
                    IArea Lobby = Factory.GetArea("Lobby");

                    Lobby.SetJsonValues(new Point(i, HotelHeight + 1), 5, new Size(1, 1), i);

                    HotelAreas.Add(Lobby);
                }
            }

            HotelMovables.Add(new Maid(new Point(4, HotelHeight + 1)));
            HotelMovables.Add(new Maid(new Point(6, HotelHeight + 1)));


            SetNeighbor();
        }

        public Bitmap Superimpose(Bitmap smallBmp)
        {
            Bitmap imposedBitmap = HotelBackground; 

            Graphics g = Graphics.FromImage(imposedBitmap);
            g.CompositingMode = CompositingMode.SourceOver;

            g.DrawImage(smallBmp, new Point(0, 0));
            return imposedBitmap;
        }


        public void SetHotelBitmap()
        {
            HotelBackground = DrawHotel();
        }

        public Bitmap DrawHotel()
        {
            // all art is 96 * 96 pixels
            Bitmap buffer = new Bitmap((HotelWidth + 2) * 96, (HotelHeight + 1) * 96);

            using (Graphics graphics = Graphics.FromImage(buffer))
            {
                foreach (IArea area in HotelAreas)
                {
                    graphics.DrawImage(area.Art, area.Position.X * 96, (area.Position.Y - 1) * 96, area.Dimension.Width * 96, area.Dimension.Height * 96);
                }
            }
            return buffer;
        }

        public Bitmap DrawMovables()
        {
            Bitmap buffer = new Bitmap((HotelWidth + 2) * 96, (HotelHeight + 1) * 96);

            using (Graphics graphics = Graphics.FromImage(buffer))
            {
                List<IMovable> t = HotelMovables;

                foreach (IMovable movable in t)
                {
                    graphics.DrawImage(movable.Art, movable.Position.X * 96 * 1.05f, (movable.Position.Y - 1) * 96 * 1.05f, movable.Art.Width, movable.Art.Height);

                }
            }

            return buffer;
        }

        private void SetNeighbor()
        {

            foreach (IArea area in HotelAreas)
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
            if (!(HotelAreas.Find(X => X.Position == new Point(area.Position.X + xOffset, area.Position.Y + yOffset)) is null))
            {
                area.Edge.Add(HotelAreas.Find(X => X.Position == new Point(area.Position.X + xOffset, area.Position.Y + yOffset)), wieght);
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

                HotelMovables.Add(new Guest(name, requestInt, new Point(0, HotelHeight + 1)));
                //Guest guest = new Guest(name, requestInt, new Point(0, HotelHeight + 1));


                //IMovableList.Add(guest);

                Console.WriteLine($"new guest = name: {name}, request: {request} ");
            }
        }       
    }
}