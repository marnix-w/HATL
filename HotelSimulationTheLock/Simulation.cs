﻿using HotelEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSimulationTheLock
{
    public partial class Simulation : Form
    {
        public Simulation()
        {
            InitializeComponent();

            HotelEventManager.Start();

            float newHTE = 10000f;

            HotelEventManager.HTE_Factor = newHTE;

            Console.WriteLine(!HotelEventManager.Running);

            Model.showListener temp = new Model.showListener();

            HotelEventManager.Register(temp);
                        
            new Thread((ThreadStart)(() =>
            {
                while (true)
                {

                }
            })).Start();
        }
    }
}