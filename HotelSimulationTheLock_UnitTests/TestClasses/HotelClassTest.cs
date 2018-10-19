using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulationTheLock;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

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


            HotelTest = new Hotel(test_startupscreen.layout, test_startupscreen.settings, new JsonHotelBuilder());
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

        [TestMethod]
        public void TestIfCurrentValueOfMoveableMethodIsFilledWithData()
        {
                  
            //arrange   
            Simulation test_Simulation;
            StartupScreen test_startupscreen;
            string test_path;
            Hotel HotelTest;
            Guest g;
            List<string> ListTest;

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
            ListTest = new List<string>();


            HotelTest = new Hotel(test_startupscreen.layout, test_startupscreen.settings, new JsonHotelBuilder());
            g = new Guest(null, "Arthas died as Lich King", 155, new System.Drawing.Point(5, 5), 10);

            ListTest = new List<string>();
        
           ListTest = HotelTest.CurrentValue();

            //assert
            Assert.AreEqual(ListTest, HotelTest.CurrentValue());
        }

        [TestMethod]
        public void TestIfCurrentValueOfAreaMethodIsFilledWithData()
        {
                 
            //arrange   
            Simulation test_Simulation;
            StartupScreen test_startupscreen;
            string test_path;
            Hotel HotelTest;
            Guest g;
            List<string> ListTest;

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
            ListTest = new List<string>();


            HotelTest = new Hotel(test_startupscreen.layout, test_startupscreen.settings, new JsonHotelBuilder());
            g = new Guest(null, "Iron Man", 4, new System.Drawing.Point(5, 5), 10);

            ListTest = new List<string>();

            ListTest = HotelTest.CurrentValueIArea();

            //assert
            Assert.AreEqual(ListTest, HotelTest.CurrentValueIArea());
        }

        [TestMethod]
        public void TestHotelGetAreaMethodReturnArea()
        {
                  
            //arrange   
            Simulation test_Simulation;
            StartupScreen test_startupscreen;
            string test_path;
            Hotel HotelTest;
            IArea AreaTest;
      

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

            HotelTest = new Hotel(test_startupscreen.layout, test_startupscreen.settings, new JsonHotelBuilder());
            AreaTest = new Room();
            AreaTest.Position = new Point(5, 5);
            Point locationTest = new Point(5, 5);     

            HotelTest.HotelAreas.Add(AreaTest);

            //assert
            Assert.AreNotEqual(AreaTest, HotelTest.GetArea(AreaTest.Position));
        }


        [TestMethod]
        public void TestIfGetAreaMethodReturnIDisWorking()
        {
            //arrange   
            Simulation test_Simulation;
            StartupScreen test_startupscreen;
            string test_path;
            Hotel HotelTest;
            IArea AreaTestID;

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

            HotelTest = new Hotel(test_startupscreen.layout, test_startupscreen.settings, new JsonHotelBuilder());
            AreaTestID = new Room();
            AreaTestID.ID = 101;

            HotelTest.HotelAreas.Add(AreaTestID);


            //assert
            Assert.AreNotEqual(101, HotelTest.GetArea(AreaTestID.ID));       
          
        }

        [TestMethod]
        public void TestIfGetAreaMethodIsWorkingReturnInt()
        {
            //arrange   
            Simulation test_Simulation;
            StartupScreen test_startupscreen;
            string test_path;
            Hotel HotelTest;
            IArea AreaTestID;

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

            HotelTest = new Hotel(test_startupscreen.layout, test_startupscreen.settings, new JsonHotelBuilder());
            AreaTestID = new Room();
            AreaTestID.ID = 101;

            HotelTest.HotelAreas.Add(AreaTestID);


            //assert
            Assert.AreNotEqual(101, HotelTest.GetArea(AreaTestID.ID));

        }


    }
}
