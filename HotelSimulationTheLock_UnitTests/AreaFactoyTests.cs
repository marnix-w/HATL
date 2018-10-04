using System.Drawing;
using System.Collections.Generic;
using HotelSimulationTheLock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            
            gotten = areaFactory.GetArea("SexDungeon", new Point(69, 69), int.MaxValue, new Point(-1, -1), 9001);

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
            
            gotten.Add(areaFactory.GetArea("Cinema", new Point(1, 1), 1, new Point(1, 1), 5));
            gotten.Add(areaFactory.GetArea("Elevator", new Point(1, 1), 1, new Point(1, 1), 5));
            gotten.Add(areaFactory.GetArea("Fitness", new Point(1, 1), 1, new Point(1, 1), 5));
            gotten.Add(areaFactory.GetArea("Lobby", new Point(1, 1), 1, new Point(1, 1), 5));
            gotten.Add(areaFactory.GetArea("Reception", new Point(1, 1), 1, new Point(1, 1), 5));
            gotten.Add(areaFactory.GetArea("Restaurant", new Point(1, 1), 1, new Point(1, 1), 5));
            gotten.Add(areaFactory.GetArea("Room", new Point(1, 1), 1, new Point(1, 1), 5));
            gotten.Add(areaFactory.GetArea("StairCase", new Point(1, 1), 1, new Point(1, 1), 5));

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

            gotten = areaFactory.GetArea("Pool", new Point(420, 420), int.MaxValue, new Point(-1, -1), 666);

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

            gotten = areaFactory.GetArea("ActualPool", new Point(420, 420), int.MaxValue, new Point(-1, -1), 666);

            //assert
            Assert.AreEqual(expected.GetType(), gotten.GetType());
        }

    }
}
