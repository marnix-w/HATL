using HotelEvents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class Hotel : HotelEventListener
    {
        //this list will be filled with IAreas objects
        public List<IArea> HotelAreaList = new List<IArea>();

        //a hotel needs to have a list of current guests 
        private List<IMovable> _iMovableList = new List<IMovable>();

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

            HotelEventManager.Register(this);

            foreach (JsonModel i in layout)
            {
                int temp = 0;

                if (i.Classification != null)
                {
                    temp = int.Parse(Regex.Match(i.Classification, @"\d+").Value);
                }


                HotelAreaList.Add(tijdelijk.GetArea(i.AreaType, i.Position, i.Capacity, i.Dimension, temp));
            }

        }

        public void Notify(HotelEvent evt)
        {
            if (evt.EventType.Equals(HotelEventType.CHECK_IN))
            {
                string name = "";
                string request = "";
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

                _iMovableList.Add(guest);

                Debug.WriteLine($"new guest = name: {name}, request: {request} ");
            }
        }

    }
}
