using System;
using System.Drawing;
using System.Collections.Generic;
using HotelSimulationTheLock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ActualPool;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class AreaFactoryTests
    {
        [TestMethod]
        public void CreateNonexistentRoomsUsingAreaFactory()
        {
            // arrange
            IArea expected;
            IArea gotten;
            AreaFactory areaFactory = new AreaFactory();

            //act
            expected = null;            
            
            gotten = areaFactory.GetArea("SexDungeon");

            //assert
            Assert.AreEqual(expected, gotten);            
        }

        [TestMethod]
        public void CreateExistentRoomsUsingAreaFactory()
        {
            // arrange
            List<IArea> expected = new List<IArea>();
            List<IArea> gotten = new List<IArea>();
            AreaFactory areaFactory = new AreaFactory();
            
            //act            
            expected.Add(new Cinema());
            expected.Add(new Elevator());
            expected.Add(new Fitness());
            expected.Add(new Lobby());
            expected.Add(new Reception());
            expected.Add(new Restaurant());
            expected.Add(new Room());
            expected.Add(new Staircase());
            
            gotten.Add(areaFactory.GetArea("Cinema"));
            gotten.Add(areaFactory.GetArea("Elevator"));
            gotten.Add(areaFactory.GetArea("Fitness"));
            gotten.Add(areaFactory.GetArea("Lobby"));
            gotten.Add(areaFactory.GetArea("Reception"));
            gotten.Add(areaFactory.GetArea("Restaurant"));
            gotten.Add(areaFactory.GetArea("Room"));
            gotten.Add(areaFactory.GetArea("StairCase"));

            //assert
            for (int i = 0; i < gotten.Count - 1; i++)
            {
                Assert.AreEqual(expected[i].GetType(), gotten[i].GetType());
            }
       
        }

        [TestMethod]
        public void CreateNonexistentRoomsUsingAreaFactoryAndFakeDLL()
        {
            // arrange
            IArea expected;
            IArea gotten;
            AreaFactory areaFactory = new AreaFactory();

            //act
            expected = null;

            gotten = areaFactory.GetArea("Pool");

            //assert
            Assert.AreEqual(expected, gotten);
        }

        [TestMethod]
        public void CreateNonexistentRoomsUsingAreaFactoryAndRealDLL()
        {
            // arrange
            IArea expected;
            IArea gotten;
            AreaFactory areaFactory = new AreaFactory();

            //act
            expected = new ActualPool.ActualPool();

            gotten = areaFactory.GetArea("ActualPool");

            //assert
            Assert.AreEqual(expected.GetType(), gotten.GetType());
        }
    }
}
