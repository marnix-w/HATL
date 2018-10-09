using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulationTheLock;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class StartupScreenTest
    {
        [TestMethod]
        public void TestIfThePathRouteIsNotNull()
        {
            //arrange         
            StartupScreen test_startupscreen;

            //act       
            test_startupscreen = new StartupScreen();
           
         
            //assert
            Assert.IsNotNull(test_startupscreen._path);           
        }

        [TestMethod]
        public void TestIfJsonFileIsDesirialized()
        {
            //arrange         
            StartupScreen test_startupscreen;
            string test_path;        

            //act       
            test_startupscreen = new StartupScreen();            
            test_path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\..\HotelSimulationTheLock\Assets\Libraries\Hotel_reparatie.layout");
            test_startupscreen.layout = test_startupscreen.ReadLayoutJson(test_path);

          
            //assert
            Assert.IsNotNull(test_startupscreen.layout);
        }

        [TestMethod]
        public void TestIfPathIsNotFound()
        {
            //arrange         
            StartupScreen test_startupscreen;
            string test_path;

            //act       
            test_startupscreen = new StartupScreen();
            test_path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\HotelSimulationTheLock\Assets\Libraries\Hotel_reparatie.layout");
            test_startupscreen.layout = test_startupscreen.ReadLayoutJson(test_path);

            //assert
            Assert.IsNull(test_startupscreen.layout);
        }

        [TestMethod]
        public void TestIfLabelIsNotNullOnStartUp()
        {
            //arrange         
            StartupScreen test_startupscreen;

            //act       
            test_startupscreen = new StartupScreen();            


            //assert
            Assert.IsNotNull(test_startupscreen.maid_LB.Text);
        }

        // rework this test its with the new settings object
        [TestMethod]
        public void TetstIfOnClickPassingDataThroughIsWorking()
        {
            //arrange         
            StartupScreen test_startupscreen;           
            string test_path;
            Simulation test_simulation;
            SettingsModel test_settings;

            //act       
            test_startupscreen = new StartupScreen();
            test_settings = new SettingsModel();
            test_path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\..\HotelSimulationTheLock\Assets\Libraries\Hotel_reparatie.layout");
            test_startupscreen.layout = test_startupscreen.ReadLayoutJson(test_path);

            test_simulation = new Simulation(test_startupscreen.layout, test_settings);
            test_simulation.HotelLayout = test_startupscreen.layout;
            //assert
            Assert.AreEqual(test_startupscreen.layout, test_simulation.HotelLayout);
        }
    }
}
