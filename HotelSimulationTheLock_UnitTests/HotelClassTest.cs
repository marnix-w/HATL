using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulationTheLock;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class HotelClassTest
    {
        [TestMethod]
        public void TestIfCallElevatorIsWorkingOnGuestRequest()
        { 
            //arrange         
            Hotel HotelTest;
            Guest g;
            ElevatorCart elevator;
            IArea Areatest;

            //act       
            HotelTest = new Hotel(null, null);
            g = new Guest(null, "SUPERMAN BOB", 155, new System.Drawing.Point(5, 5), 10);
            elevator = new ElevatorCart(new System.Drawing.Point(5, 5), null, 10);
            Areatest = new Room();
 

            g.FinalDes = Areatest;
            g.CallElevator();
            HotelTest.CallElevator(g);
            elevator.RequestElevator(g, 5);
            Areatest.Position = new System.Drawing.Point(5, 5);

            elevator.RequestList.Add(g);
            //Goes UP
            if (elevator.RequestList[0].Position.Y < elevator.Position.Y)
            {
                elevator.Up.Add(elevator.RequestList[0].Position.Y);
                elevator.Up.UpdateList();
            }
            //Goes DOWN
            else
            {
                elevator.Down.Add(elevator.RequestFloor.Position.Y);
                elevator.UpdateList();
            }

            //assert
            Assert.AreEqual(g.FinalDes, elevator.FinalDes);
        }
    }
}
