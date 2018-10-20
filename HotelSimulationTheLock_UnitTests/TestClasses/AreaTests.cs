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
            List<IArea> l = new List<IArea>();

            l.Add(new Cinema());
            l.Add(new Elevator());
            l.Add(new Fitness());
            l.Add(new Lobby());
            l.Add(new Reception());
            l.Add(new Restaurant());
            l.Add(new Room());
            l.Add(new Staircase());

            // act
            foreach (var item in l)
            {
                item.SetJsonValues(5, new Point(0, 0), 1, new Size(1, 1), 3);
            }
            

            // assert
            Assert.AreEqual(l[0].ID, 5);
            Assert.AreEqual(l[1].Position, new Point(0, 0));
            Assert.AreEqual(l[2].Dimension, new Size(1, 1));
            Assert.AreEqual(l[3].Position, new Point(0, 0));
            Assert.AreEqual(l[4].ID, 5);
            Assert.AreEqual(l[5].Dimension, new Size(1, 1));
            Assert.AreEqual(((Room)l[6]).Classification, 3);
            Assert.AreEqual(l[7].ID, 5);
        }

        [TestMethod]
        public void TestEnterReception()
        {
            // arrange
            Reception c = new Reception();

            // act
            IMovable d = new Receptionist();


            c.CheckInQueue.Enqueue(d);
        
            bool a = c.EnterArea(d);


            // assert
            Assert.IsTrue(a);
   
        }


    }
}
