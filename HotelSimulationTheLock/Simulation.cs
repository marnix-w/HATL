﻿using HotelEvents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HotelSimulationTheLock
{
    public partial class Simulation : Form
    {
        public Hotel Hotel { get; set; }
        public int UnitTestvalue { get; set; }

        private Timer t { get; set; }

        private bool _pauseResume { get; set; }

        // Drawing properties
        private PictureBox HotelBackground { get; set; }
        private Bitmap HotelImage { get; set; }
        public static int RoomArtSize { get; } = 96;


        public Simulation(List<JsonModel> layout, SettingsModel Settings)
        {
            // 0.5f should be a varible in the settings data set
            Hotel = new Hotel(layout, Settings);
            //   HotelLayout = layout;
            HotelEventManager.HTE_Factor = 0.5f;

            // Does this timer work corectly with the HTE factor? -marnix
            t = new Timer
            {
                Interval = 650 // specify interval time as you want
            };
            t.Tick += new EventHandler(Timer_Tick);
            t.Start();

            HotelImage = Hotel.HotelDrawer.DrawHotel(Hotel.HotelAreas, Hotel.HotelMovables);

            HotelBackground = new PictureBox
            {
                Location = new Point(450, 100),
                Width = Hotel.HotelWidth * RoomArtSize,
                Height = Hotel.HotelHeight * RoomArtSize,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = HotelImage
            };

            Controls.Add(HotelBackground);

            // Last methods for setup
            InitializeComponent();

            _pauseResume = false;
            button1.Text = "Pause";
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            Hotel.PerformAllActions();

            // fix frequent GC calls

            //every timer tick we refresh the facillity layout
            _fillFacillityTB();

            //every timer tick we refresh the moveable layout
            _fillMoveAbleTB();

            // Disposing the movable bitmap to prevent memory leaking
            // https://blogs.msdn.microsoft.com/davidklinems/2005/11/16/three-common-causes-of-memory-leaks-in-managed-applications/
            HotelImage.Dispose();
            HotelImage = Hotel.HotelDrawer.DrawHotel(Hotel.HotelAreas, Hotel.HotelMovables);
            HotelBackground.Image = HotelImage;
        }


        //Overview of hotel facilities
        private void _fillFacillityTB()
        {
            roomTB.Clear();
            //fintess overview
            facillityTB.Clear();

            foreach (string i in Hotel.currentValueIArea())
            {
                try
                {                  
                    if (i.Contains("Room"))
                    {
                        roomTB.AppendText(i);
                    }
                    if (i.Contains("Fitness") || i.Contains("Restaurant") || i.Contains("Cinema") || i.Contains("Elevator"))
                    {
                        facillityTB.AppendText(i);
                    } 
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        private void _fillMoveAbleTB()
        {
            guestTB.Clear();

            foreach (string value in Hotel.currentValue())
            {
                try
                {
                    guestTB.AppendText(value);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!_pauseResume)
            {
                _pauseResume = true;
                button1.Text = "Resume";
                label1.Text = "Simulation has been paused";
                t.Stop();
                HotelEventManager.Stop();
            }
            else
            {
                _pauseResume = false;
                button1.Text = "Pause";
                label1.Text = "Simulation is running on normaal speed";
                t.Start();
                HotelEventManager.Start();
            }

        }


    }
}