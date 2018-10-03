﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelEvents;

namespace HotelSimulationTheLock
{
    class Maid : IMovable, HotelEventListener
    {
        public Point Position { get; set; }
        public PictureBox Art { get; set; }

        public MovableStatus Status { get; set; }
        Queue<HotelEvent> CleaningEmergencies { get; set; }

        public void Notify(HotelEvent evt)
        {
            Art = new PictureBox();
            Art.Image = Properties.Resources.maid;

            if (evt.EventType.Equals(HotelEventType.CLEANING_EMERGENCY))
            {
                // add to cleaning ques next
            }
            else if (evt.EventType.Equals(HotelEventType.CHECK_OUT))
            {
                // clean the room
            }
        }
    }
}
