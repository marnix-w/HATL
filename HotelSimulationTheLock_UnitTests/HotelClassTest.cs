using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulationTheLock;
using System.IO;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class HotelClassTest
    {
        [TestMethod]
        public void TestIfCallElevatorIsWorkingOnGuestRequest()
        {
            //arrange   
            Simulation test_Simulation;
            StartupScreen test_startupscreen;
            string test_path;
            Hotel HotelTest;
            Guest g;
            ElevatorCart elevator;
            IArea Areatest;

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


            HotelTest = new Hotel(test_startupscreen.layout, test_startupscreen.settings);
            g = new Guest(null, "SUPERMAN BOB", 155, new System.Drawing.Point(5, 5), 10);

            elevator = new ElevatorCart(new System.Drawing.Point(0, 1), HotelTest, 5);
            Areatest = new Room();


            g.FinalDes = Areatest;
           
            HotelTest.CallElevator(g);
            elevator.RequestElevator(g, 5);
            Areatest.Position = new System.Drawing.Point(5, 5);

            elevator.RequestList.Add(g);         


            //assert
            Assert.AreNotEqual(0, elevator.RequestList.Count);
        }
    }
}
