using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulationTheLock;
using System.IO;
using System.Collections.Generic;
using HotelEvents;
using System.Drawing;
using System.Linq;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class MaidTests
    {
        public Hotel Hotel()
        {
            SettingsModel g = new SettingsModel()
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

            return new Hotel(null, g, new StubedHotelBuilder());
        }


        [TestMethod]
        public void CleaningEventTest()
        {
            CleaningEvent ce = new CleaningEvent();

            ce.Time = 5;
            ce.ToClean = new Room();

            Assert.AreEqual(ce.Time, 5);
            Assert.AreEqual(ce.ToClean.GetType(), typeof(Room));

        }

        [TestMethod]
        public void IsSomthingInCleanQueue()
        {
            
            Maid m = new Maid(new Point(1, 1), new Hotel());

            m.ToCleanList.Enqueue(new CleaningEvent() { ToClean = new Room(), Time = 7 });

            m.PerformAction();

            Assert.AreEqual(m.Status, MovableStatus.GOING_TO_ROOM);
            
        }

        [TestMethod]
        public void CleaningTests()
        {                      
            Maid m = new Maid(new Point(0, 4), Hotel());

            m.FinalDes = m.Area;

            m.Status = MovableStatus.GOING_TO_ROOM;
            
            m.PerformAction();

            Assert.AreEqual(m.Status, MovableStatus.CLEANING);
        }

        [TestMethod]
        public void CleanignEmergancey()
        {
            

            Maid m = new Maid(new Point(0, 4), Hotel());

            Dictionary<string, string> t = new Dictionary<string, string>();

            t.Add("kamer", "18");
            t.Add("HTE", "5");

            m.Notify(new HotelEvent() { EventType = HotelEventType.CLEANING_EMERGENCY, Data = t});

            m.Status = MovableStatus.IDLE;

            m.PerformAction();

            Assert.AreEqual(m.Status, MovableStatus.GOING_TO_ROOM);
        }

        [TestMethod]
        public void Evacuate()
        {
            Maid m = new Maid(new Point(0, 4), Hotel());
            
            m.Notify(new HotelEvent() { EventType = HotelEventType.EVACUATE });
            
            Assert.AreEqual(m.Status, MovableStatus.EVACUATING);
        }

        [TestMethod]
        public void LeavingElevator()
        {
            Maid m = new Maid(new Point(1, 4), Hotel());

            m.Area = m.Hotel.GetArea(new Point(0, 4));

            m.Status = MovableStatus.LEAVING_ELEVATOR;
            m.LastStatus = MovableStatus.EVACUATING;

            m.PerformAction();

            Assert.AreEqual(MovableStatus.EVACUATING, m.Status);

        }

        [TestMethod]
        public void CallElevator()
        {
            Maid m = new Maid(new Point(0, 4), Hotel());

            m.Area = m.Hotel.GetArea(new Point(0, 4));
            m.FinalDes = m.Hotel.GetAreaByID(13);

            m.Status = MovableStatus.ELEVATOR_REQUEST;

            m.WantsElevator = true;

            m.PerformAction();

            Assert.AreEqual(MovableStatus.WAITING_FOR_ELEVATOR, m.Status);

        }

        [TestMethod]
        public void evacuateMethod()
        {
            Maid m = new Maid(new Point(0, 4), Hotel());

            m.Area = m.Hotel.GetArea(new Point(0, 4));

            m.Status = MovableStatus.EVACUATING;

            m.PerformAction();

            Assert.AreEqual(m.Hotel.GetAreaByID(9), m.Path.Last());

        }

        [TestMethod]
        public void GoToROom()
        {
            Maid m = new Maid(new Point(3, 2), Hotel());

            m.Area = m.Hotel.GetArea(new Point(3, 2));
            m.FinalDes = m.Hotel.GetAreaByID(13);

            m.SetPath(m.FinalDes);

            Assert.AreEqual(m.Path.Last(), m.Hotel.GetAreaByID(13));

        }

        [TestMethod]
        public void CheckIdle()
        {
            Maid m = new Maid(new Point(1, 4), Hotel());

            m.Area = m.Hotel.GetArea(new Point(1, 4));

            m.Status = MovableStatus.WAITING_FOR_ELEVATOR;
            m.LastStatus = MovableStatus.EVACUATING;

            m.PerformAction();

            Assert.AreEqual(MovableStatus.EVACUATING, m.Status);

        }
    }
}
