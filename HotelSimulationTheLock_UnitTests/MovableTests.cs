﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            IMovable CoolBob;

            //act
            CoolBob = new Receptionist();

            //assert
            Assert.IsInstanceOfType(CoolBob, typeof(Receptionist));
        }

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
            Bob = new Guest("Bob", 1, new System.Drawing.Point(0,0));

            //assert
            Assert.IsInstanceOfType(Bob, typeof(Guest));
        }
    }
}