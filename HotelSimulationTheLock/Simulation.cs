using HotelEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
                Interval = 1000 // specify interval time as you want
            };
            t.Tick += new EventHandler(Timer_Tick);
            t.Start();

            Areas = Hotel.DrawHotel();

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
            _fillRichTextBox();           
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            //guest overview
            _guestStatus.Text = string.Empty;
            //maid overview
            _maidStatus.Text = string.Empty;

            // Fix this!
            #region

            // Causes errors with current version
            // list is not lockeable 
            // status handle should not be done here

            //foreach (IMovable g in Hotel.HotelMovables)
            //{
            //    if (g is Guest t)
            //    {
            //        _guestStatus.Text += t.Name + "\t" + g.Status + "\t" + t.RoomRequest + "\t" + t.Position;
            //        _guestStatus.Text += "\n";

            //    }
            //    if (g is Maid m)
            //    {
            //        _maidStatus.Text += "Maid \t" + m.Status + "\t" + m.Position;
            //        _maidStatus.Text += "\n";
            //    }
            //}

            #endregion
            
            Movables = Hotel.DrawMovables();

            HotelBackground.Image = GetHotelMap();

            // Disposing the movable bitmap to prevent memory leaking
            // https://blogs.msdn.microsoft.com/davidklinems/2005/11/16/three-common-causes-of-memory-leaks-in-managed-applications/
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
                        _roomsStatus.Text += ((Room)i).Classification + " Star " + type + "\t" +  i.AreaStatus + "\t " + ((Room)i).Position;
                        _roomsStatus.Text += "\n";
                        break;
                    case "Restaurant":
                        _fitnessStatus.Text += i.GetType().ToString().Replace("HotelSimulationTheLock.", "") + ": " + i.AreaStatus;
                        _fitnessStatus.Text += "\n";
                        break;
                    case "Fitness":
                        _fitnessStatus.Text += i.GetType().ToString().Replace("HotelSimulationTheLock.", "") + ": " + i.AreaStatus;
                        _fitnessStatus.Text += "\n";
                        break;
                    default:
                        break;
                }
               
            }

            
        }
        
    }
}