using HotelEvents;
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
        public List<JsonModel> HotelLayout { get; set; }

        // Drawing properties
        private PictureBox HotelBackground { get; set; }
        private Bitmap Movables { get; set; }
        private Bitmap Areas { get; set; }
        public static int RoomArtSize { get; } = 96;

        public Simulation(List<JsonModel> layout, SettingsModel Settings)
        {
            // 0.5f should be a varible in the settings data set
            Hotel = new Hotel(layout, Settings);
            HotelLayout = layout;
            HotelEventManager.HTE_Factor = 0.5f;

            // Does this timer work corectly with the HTE factor? -marnix
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer
            {
                Interval = 500 // specify interval time as you want
            };
            t.Tick += new EventHandler(Timer_Tick);
            t.Start();

            Areas = Hotel.DrawHotel();
            Movables = Hotel.DrawMovables();

            HotelBackground = new PictureBox
            {
                Location = new Point(50, 100),
                Width = Hotel.HotelWidth * RoomArtSize,
                Height = Hotel.HotelHeight * RoomArtSize,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Areas
            };

            Controls.Add(HotelBackground);

            // Last methods for setup
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            _fillRichTextBox();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            Hotel.PerformAllActions();

            //guest overview
            guestTB.Clear();

            //maid overview
            maidTB.Clear();

            // Causes errors with current version
            // list is not lockeable 
            // status handle should not be done here

            try
            {
                foreach (IMovable g in Hotel.HotelMovables)
                {
                    if (g is Guest t)
                    {
                        guestTB.Text += t.Name + "\t" + g.Status + "\t" + t.RoomRequest + "\t" + t.Position;
                        guestTB.Text += Environment.NewLine;
                    }
                    if (g is Maid m)
                    {
                        maidTB.Text += "Maid \t" + m.Status + "\t" + m.Position;
                        maidTB.Text += Environment.NewLine;
                    }
                }
            }
            catch (Exception)
            {

                Debug.WriteLine("Jasper fix je stats shit");
            }  

            // Disposing the movable bitmap to prevent memory leaking
            // https://blogs.msdn.microsoft.com/davidklinems/2005/11/16/three-common-causes-of-memory-leaks-in-managed-applications/
            Areas.Dispose();


            Areas = Hotel.DrawHotel();
            Movables = Hotel.DrawMovables();

            HotelBackground.Image = GetHotelMap();

            Movables.Dispose();
        }


        public Bitmap GetHotelMap()
        {
            Bitmap imposedBitmap = Areas;

            Graphics g = Graphics.FromImage(imposedBitmap);
            g.CompositingMode = CompositingMode.SourceOver;

            g.DrawImage(Movables, new Point(0, 0));
            return imposedBitmap;
        }

        //Overview of hotel facilities
        private void _fillRichTextBox()
        {
            foreach (IArea i in Hotel.HotelAreas)
            {
                string type = i.GetType().ToString().Replace("HotelSimulationTheLock.", "");

                switch (type)
                {

                    case "Room":
                        roomTB.Text += ((Room)i).Classification + " Star " + type + "\t" + i.AreaStatus + "\t " + ((Room)i).Position;
                        roomTB.Text += Environment.NewLine;                      
                        break;
                    case "Fitness":
                        fitnessTB.Text += i.GetType().ToString().Replace("HotelSimulationTheLock.", "") + ": " + i.AreaStatus;
                        fitnessTB.Text += Environment.NewLine;
                        break;
                    case "Restaurant":
                        restaurantTB.Text += i.GetType().ToString().Replace("HotelSimulationTheLock.", "") + ": " + i.AreaStatus;
                        restaurantTB.Text += Environment.NewLine;
                        break;
                    default:
                        break;
                }


            }


        }

    }
}