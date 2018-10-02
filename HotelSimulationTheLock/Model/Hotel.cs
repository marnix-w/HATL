using HotelEvents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace HotelSimulationTheLock
{
    public class Hotel
    {
        //this list will be filled with IAreas objects
        public List<IArea> HotelAreaList = new List<IArea>();

        //a hotel needs to have a list of current guests 
        private List<Guest> _guestsList = new List<Guest>();

        //a hotel needs to have maids to cleanup stuff
        private List<Maid> _maidList = new List<Maid>();

        //a hotel has only 1 elavator
        //private Elevator _elavator = new Elevator();

        //a hotel has only 1 staircase
        //private Staircase _starcase = new Staircase();

        //amount of events per second
        public int HtePerSecond { get; set; }

        //an hotel has a background image
        public PictureBox Background { get; set; }

        public Hotel(List<JsonModel> layout, float HTESeconds)
        {
            HotelEventManager.HTE_Factor = HTESeconds;

            AreaFactory tijdelijk = new AreaFactory();

            foreach (JsonModel i in layout)
            {
                int temp = 0;

                if (i.Classification != null)
                {
                    temp = int.Parse(Regex.Match(i.Classification, @"\d+").Value);
                }


                HotelAreaList.Add(tijdelijk.GetArea(i.AreaType, i.Position, i.Capacity, i.Dimension, temp));
            }

            SetNieghbors();

        }

        public void SetNieghbors()
        {
            int MaxX = HotelAreaList.OrderBy(X => X.Position.X).Last().Position.X;

            foreach (IArea area in HotelAreaList)
            {

                // add right niegbor
                for (int i = 1; i < MaxX; i++)
                {                    
                    if (!(HotelAreaList.Find(X => X.Position == new Point(area.Position.X + i, area.Position.Y)) is null))
                    {
                        area.Edge.Add(HotelAreaList.Where(X => X.Position == new Point(area.Position.X + i, area.Position.Y)).Single(), i);
                        break;
                    }
                }
                // add left niegbor
                for (int i = 1; i < MaxX; i++)
                {
                    if (!(HotelAreaList.Find(X => X.Position == new Point(area.Position.X - i, area.Position.Y)) is null))
                    {
                        area.Edge.Add(HotelAreaList.Where(X => X.Position == new Point(area.Position.X - i, area.Position.Y)).Single(), i);
                        break;
                    }
                }              
                if (area.Position.X == 0 || area.Position.X == MaxX)
                {
                    // add top neighbor
                    if (!(HotelAreaList.Find(X => X.Position == new Point(area.Position.X, area.Position.Y + 1)) is null))
                    {
                        area.Edge.Add(HotelAreaList.Where(X => X.Position == new Point(area.Position.X, area.Position.Y + 1)).Single(), 1);
                        break;
                    }
                    // add bothem neighbor
                    if (!(HotelAreaList.Find(X => X.Position == new Point(area.Position.X, area.Position.Y - 1)) is null))
                    {
                        area.Edge.Add(HotelAreaList.Where(X => X.Position == new Point(area.Position.X, area.Position.Y - 1)).Single(), 1);
                        break;
                    }
                }
            }
        }




    }
}
