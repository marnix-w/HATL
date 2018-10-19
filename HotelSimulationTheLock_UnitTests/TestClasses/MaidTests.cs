using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulationTheLock;
using System.IO;
using System.Collections.Generic;
using HotelEvents;
using System.Drawing;

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

        public void Evacuate()
        {
            Maid m = new Maid(new Point(0, 4), Hotel());

            Dictionary<string, string> t = new Dictionary<string, string>();
            
            m.Status = MovableStatus.IDLE;

            m.Notify(new HotelEvent() { EventType = HotelEventType.EVACUATE });
            
            m.PerformAction();

            Assert.AreEqual(m.Status, MovableStatus.EVACUATING);
        }
    }
}
