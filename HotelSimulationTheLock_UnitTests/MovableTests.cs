using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSimulationTheLock;
using HotelEvents;
using System.Drawing;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class MovableTests
    {       

        [TestMethod]
        public void TestIfMaidLivesAfterCreation()
        {
            //arrange
            IMovable Barbra;

            //act
            Barbra = new Maid(new System.Drawing.Point(0, 0));

            //assert
            Assert.IsInstanceOfType(Barbra, typeof(Maid));
        }

        [TestMethod]
        public void TestIfGuestLivesAfterCreation()
        {
            //arrange
            IMovable Bob;

            //act

            Bob = new Guest(null, "Bob", 1, new System.Drawing.Point(0,0), 0);

            //assert
            Assert.IsInstanceOfType(Bob, typeof(Guest));
        }
    }
}
