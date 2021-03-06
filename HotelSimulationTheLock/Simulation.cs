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
        /// 
        /// !!! Note if the simulation causes a breakmode please stop and try to run the simulation again !!!
        /// 


        /// <summary>
        /// In order to close the simulation form we needed to have the StartupScreen aswell
        /// </summary>
        private StartupScreen _options { get; set; }
        /// <summary>
        /// Passing data for the timer to hotel
        /// </summary>
        public Hotel Hotel { get; set; }
        /// <summary>
        /// Adding a timer to handle the events
        /// </summary>
        private Timer _timer { get; set; }
        /// <summary>
        /// Able to pause or resume the simulation
        /// </summary>
        private bool _pauseResume { get; set; }
        // Drawing properties
        /// <summary>
        /// Sets the _buffer on the picturebox
        /// </summary>
        private PictureBox _hotelBackground { get; set; }
        /// <summary>
        /// Converts the drawing items to bitmap
        /// </summary>
        private Bitmap _hotelImage { get; set; }
        /// <summary>
        /// A constant member is considered static by the compiler
        /// </summary>
        public static int RoomArtSize { get; } = 96;
        /// <summary>
        /// Showing a count timer to show how many Events have passed
        /// </summary>
        private int _count { get; set; } = 0;
        /// <summary>
        /// Setting the SettingsModel value to use
        /// </summary>
        public SettingsModel Settings { get; set; }



        public Simulation(StartupScreen firstScreen, List<JsonModel> layout, SettingsModel Settings)
        {
            _options = firstScreen;
            // 0.5f should be a variable in the Settings data set
            Hotel = new Hotel(layout, Settings, new JsonHotelBuilder());

            this.Settings = Settings;

            HotelEventManager.HTE_Factor = this.Settings.HTEPerSeconds;

            //We have a timer that will have a different interval depending on the HTE settings
            //in order to keep drawing the guest correclty
            _timer = new Timer
            {
                Interval = 1000 / this.Settings.HTEPerSeconds // Specify interval time as you want 
            };
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();

            _hotelImage = Hotel.DrawMap();

            //Puts the bitmap on the Picturebox
            _hotelBackground = new PictureBox
            {
                Location = new Point(450, 100),
                Width = Hotel.HotelWidth * RoomArtSize,
                Height = Hotel.HotelHeight * RoomArtSize,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = _hotelImage
            };
            //Adding the picturebox to the form
            Controls.Add(_hotelBackground);

            // Last methods for setup
            InitializeComponent();

            _pauseResume = false;
            pauseBtn.Text = "Pause";
        }

        /// <summary>
        /// Every time the timer ticks we will perform actions based on the Events that were send from the dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {

            Console.WriteLine($"{_count++} HTE elapsed");

            Hotel.PerformAllActions();

            // Fix frequent GC calls

            //Every timer tick we refresh the facility layout
            _fillFacillityTB();

            //Every timer tick we refresh the movable layout
            _fillMoveAbleTB();

            // Disposing the movable bitmap to prevent memory leaking
            // https://blogs.msdn.microsoft.com/davidklinems/2005/11/16/three-common-causes-of-memory-leaks-in-managed-applications/
            _hotelImage.Dispose();
            _hotelImage = Hotel.DrawMap();
            _hotelBackground.Image = _hotelImage;


        }

        /// <summary>
        /// Overview of hotel facilities
        /// </summary>
        private void _fillFacillityTB()
        {
            roomTB.Clear();
            facillityTB.Clear();

            roomTB.Text = "ID: \t Facillity:  \t\tPosition: \t\tStatus:\n";
            facillityTB.Text = "ID: \tPosition: \t \tMax: \tFacility:\n";
            foreach (string i in Hotel.CurrentValueIArea())
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

        /// <summary>
        /// Overview for all the movables
        /// </summary>
        private void _fillMoveAbleTB()
        {
            guestTB.Clear();
            guestTB.Text = "Moveable: \tPosition: \t\tStatus \n";
            foreach (string value in Hotel.CurrentValue())
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

        /// <summary>
        /// When pressed on the pause button the simulation goes into pause state and once pressed again it resumes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!_pauseResume)
            {
                _pauseResume = true;
                pauseBtn.Text = "Resume";
                label1.Text = "Simulation has been paused";
                _timer.Stop();
                HotelEventManager.Pauze();
            }
            else
            {
                _pauseResume = false;
                pauseBtn.Text = "Pause";
                label1.Text = "Simulation is running on " + HotelEventManager.HTE_Factor + "Events per " + _timer.Interval + "milisecond";
                _timer.Start();
                HotelEventManager.Pauze();
            }
        }

        /// <summary>
        /// When the simulation screen is closed it should also close other forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Simulation_FormClosed(object sender, FormClosedEventArgs e)
        {
            _stopSimulation();

            this.Dispose();
            _options.Dispose();
        }

        /// <summary>
        /// When the fastforward button is clicked the events will go faster and the interval will go, also
        /// the Hte events factor will be multiplied by 2
        /// this will also increase the interval time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            _stopSimulation();

            if (Settings.HTEPerSeconds >= 4)
            {
                Settings.HTEPerSeconds = 4;
            }
            else
            {
                Settings.HTEPerSeconds = Settings.HTEPerSeconds * 2;
                HotelEventManager.HTE_Factor = Settings.HTEPerSeconds;
            }

            _updateInterval();

            SetButtonsText();

            _startSimulation();
        }

        /// <summary>
        /// Once the slow down button is pressed the events will be divided by 2 maximum to 1 event per second
        /// this will slower the interval
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            _stopSimulation();

            if (Settings.HTEPerSeconds <= 1)
            {
                Settings.HTEPerSeconds = 1;

            }
            else
            {
                Settings.HTEPerSeconds = Settings.HTEPerSeconds / 2;
                HotelEventManager.HTE_Factor = Settings.HTEPerSeconds;

            }
            _updateInterval();

            SetButtonsText();

            _startSimulation();
        }

        /// <summary>
        /// This button will reset all the intervals back to normal which is 1 Event per 1 second
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            _stopSimulation();


            Settings.HTEPerSeconds = 1;
            HotelEventManager.HTE_Factor = Settings.HTEPerSeconds;
            _timer.Interval = 1000 / Settings.HTEPerSeconds;

            _updateInterval();

            SetButtonsText();

            _startSimulation();
        }

        /// <summary>
        /// Properties of the buttons once pressed 
        /// </summary>
        private void SetButtonsText()
        {
            _fastForward.Text = "Speed up \n the simulation times 2  \n current speed is " + HotelEventManager.HTE_Factor;
            _slowDown.Text = "Slow down \n the simulation subtract by 2 \n current speed is " + HotelEventManager.HTE_Factor;
            _resetSpeed.Text = "Reset the HTE factor to 1";

            label1.Text = "Simulation is running on " + HotelEventManager.HTE_Factor + "Events per " + _timer.Interval + "milisecond";
        }

        /// <summary>
        /// Stopping the simulation from running new events
        /// </summary>
        private void _stopSimulation()
        {
            _timer.Stop();
            HotelEventManager.Stop();
        }

        /// <summary>
        /// Resuming the simulation and starting the events again
        /// </summary>
        private void _startSimulation()
        {
            _timer.Start();
            HotelEventManager.Start();
        }
        /// <summary>
        /// updates the interval so it will be displayed correctly on the screen
        /// </summary>
        private void _updateInterval()
        {
            _timer.Interval = 1000 / Settings.HTEPerSeconds;
        }
    }
}