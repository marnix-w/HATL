using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulationTheLock;
using System.IO;
using System.Drawing;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class MaidTests
    {
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

        


    }
}
