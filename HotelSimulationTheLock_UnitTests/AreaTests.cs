using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HotelSimulationTheLock;
using HotelEvents;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class AreaTests
    {
        [TestMethod]
        public void TestListnerCinema()
        {
            // arrange
            Cinema c = new Cinema();
            HotelEvent evt = new HotelEvent();

            // act
            evt.EventType = HotelEventType.START_CINEMA;
            c.Notify(evt);

            // assert
            Assert.AreEqual(AreaStatus.PLAYING_MOVIE, c.AreaStatus);
        }

        [TestMethod]
        public void TestJsonValues()
        {
            // arrange
            Cinema c = new Cinema();

            // act
            c.SetJsonValues(5, new Point(0,0), 1, new Size(1,1), 3);

            // assert
            Assert.AreEqual(c.ID, 5);
            Assert.AreEqual(c.Position, new Point(0, 0));
            Assert.AreEqual(c.Dimension, new Size(1, 1));
        }



    }
}
