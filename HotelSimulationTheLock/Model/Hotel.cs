using HotelEvents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace HotelSimulationTheLock
{
    public class Hotel
    {
        //this list will be filled with IAreas objects
        private List<IArea> _hotelList = new List<IArea>();

        //a hotel needs to have a list of current guests 
        private List<Guest> _guestsList = new List<Guest>();

        //a hotel needs to have maids to cleanup stuff
        private List<Maid> _maidList = new List<Maid>();

        //a hotel has only 1 elavator
        private Elevator _elavator = new Elevator();

        //a hotel has only 1 staircase
        private Staircase _starcase = new Staircase();

        // private Timer HteTimer { get; set; }

        //amount of events per second
        public int HtePerSecond { get; set; }

        //an hotel has a background image
        public PictureBox Background { get; set; }

        public Hotel(float HTESeconds)
        {
            HotelEventManager.HTE_Factor = HTESeconds;
        }

    }
}
