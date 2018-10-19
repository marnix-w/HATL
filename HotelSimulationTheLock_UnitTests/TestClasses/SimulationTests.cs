using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulationTheLock;
using System.IO;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class SimulationTests
    {
        [TestMethod]
        public void TestIfSettingsModelIsUsedBySettingsInSimulation()
        {
            //arrange         
            Simulation test_Simulation;
            StartupScreen test_startupscreen;
            string test_path;


            //act       
            test_startupscreen = new StartupScreen();

            test_startupscreen.settings = new SettingsModel
            {
                AmountOfMaids = 3,
                ElevatorDuration = 3,
                ElevatorCapicity = 3,
                HTEPerSeconds = 3,
                StairsDuration = 3,
                CinemaDuration = 3,
                RestaurantCapicity = 3,
                RestaurantDuration = 3,
                EatingDuration = 3,
                FitnessCapicity = 3,
            };

            test_path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\..\HotelSimulationTheLock\Assets\Libraries\Hotel_reparatie.layout");
            test_startupscreen.layout = test_startupscreen.ReadLayoutJson(test_path);

            test_Simulation = new Simulation(test_startupscreen, test_startupscreen.layout, test_startupscreen.settings);
            test_Simulation._Settings = test_startupscreen.settings;

            //assert
            Assert.AreEqual(3, test_Simulation._Settings.EatingDuration);
        }
    }
}
