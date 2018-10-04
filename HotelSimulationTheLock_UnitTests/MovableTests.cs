using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSimulationTheLock;
using HotelEvents;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class MovableTests
    {
        [TestMethod]
        public void TestIfReceptionistLivesAfterCreation()
        {
            //arrange
             Receptionist CoolBob;

            //act
            CoolBob = new Receptionist();

            //assert
            Assert.IsInstanceOfType(CoolBob, typeof(Receptionist));
        }

        [TestMethod]
        public void TestIfMaidLivesAfterCreation()
        {
            //arrange
            Maid Barbra;

            //act
            Barbra = new Maid(1, 1);

            //assert
            Assert.IsInstanceOfType(Barbra, typeof(Maid));
        }

        [TestMethod]
        public void TestIfGuestLivesAfterCreation()
        {
            //arrange
            Guest Bob;

            //act
            Bob = new Guest("Bob", 1);

            //assert
            Assert.IsInstanceOfType(Bob, typeof(Guest));
        }
    }
}
