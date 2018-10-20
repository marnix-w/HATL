using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HotelSimulationTheLock;
using HotelEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class GuestTests
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
        public void Evacuate()
        {
            Guest m = new Guest(Hotel(), "I need one week more for a good unit test project", 3, new Point(1,1),1);

            m.Notify(new HotelEvent() { EventType = HotelEventType.EVACUATE });

            Assert.AreEqual(m.Status, MovableStatus.EVACUATING);
        }

        [TestMethod]
        public void GoingToRoom()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(7,2), 1);

            m.Status = MovableStatus.GOING_TO_ROOM;
            

            m.MyRoom = m.Hotel.GetAreaByID(16);
            m.FinalDes = m.MyRoom;
            m.Area = m.Hotel.GetArea(m.Position);

            m.PerformAction();

            Assert.AreEqual(m.Path.Last(), m.Hotel.GetAreaByID(16));

            m.Area = m.MyRoom;

            m.PerformAction();
            m.PerformAction();

            Assert.AreEqual(MovableStatus.IN_ROOM, m.Status);

        }

        [TestMethod]
        public void GoingToCinema()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(3, 1), 1);

            m.Status = MovableStatus.GOING_TO_CINEMA;
            
            m.Area = m.Hotel.GetArea(m.Position);

            m.PerformAction();

            Assert.AreEqual(m.Path.Last(), m.Hotel.GetAreaByID(20));

            m.Area = m.Hotel.GetAreaByID(20);

            m.Path.Clear();

            m.PerformAction();

            Assert.AreEqual(MovableStatus.WAITING_TO_START, m.Status);

        }

        [TestMethod]
        public void GoingToFitness()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(5, 1), 1);

            m.Status = MovableStatus.GOING_TO_FITNESS;

            m.Area = m.Hotel.GetArea(m.Position);

            m.PerformAction();

            Assert.AreEqual(m.Path.Last(), m.Hotel.GetAreaByID(21));

            m.Area = m.Hotel.GetAreaByID(21);

            m.Path.Clear();

            m.PerformAction();

            Assert.AreEqual(MovableStatus.WORKING_OUT, m.Status);

        }

        [TestMethod]
        public void GoingToRestaurant()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(7, 1), 1);

            m.Status = MovableStatus.GET_FOOD;

            m.Area = m.Hotel.GetArea(m.Position);

            m.PerformAction();

            Assert.AreEqual(m.Path.Last(), m.Hotel.GetAreaByID(22));

            m.Area = m.Hotel.GetAreaByID(22);

            m.Path.Clear();

            m.PerformAction();

            Assert.AreEqual(MovableStatus.EATING, m.Status);

        }

        [TestMethod]
        public void GoToElevator()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(1, 4), 1);

            m.Area = m.Hotel.GetArea(new Point(1, 4));
            m.FinalDes = m.Hotel.GetAreaByID(13);

            m.SetPath(m.FinalDes);

            Assert.AreEqual(m.Path.Last().GetType(), typeof(Elevator));

        }

        [TestMethod]
        public void GoToROom()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(3, 2), 1);

            m.Area = m.Hotel.GetArea(new Point(3, 2));
            m.FinalDes = m.Hotel.GetAreaByID(13);

            m.SetPath(m.FinalDes);

            Assert.AreEqual(m.Path.Last(), m.Hotel.GetAreaByID(13));

        }

        [TestMethod]
        public void Checkin()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(1, 4), 5);

            m.Area = m.Hotel.GetArea(new Point(1, 4));

            m.Status = MovableStatus.CHEKING_IN;

            m.PerformAction();
            m.PerformAction();

            Assert.AreEqual(MovableStatus.GOING_TO_ROOM, m.Status);

        }

        [TestMethod]
        public void CheckinOmhoogBoek()
        {
            Hotel h = Hotel();

            Guest m = new Guest(h, "The Handler", 4, new Point(1, 4), 3);
            Guest m2 = new Guest(h, "The Handler", 4, new Point(1, 4), 4);

            m.Area = m.Hotel.GetArea(new Point(1, 4));
            m2.Area = m.Hotel.GetArea(new Point(1, 4));

            m.Status = MovableStatus.CHEKING_IN;
            m2.Status = MovableStatus.CHEKING_IN;

            m.PerformAction();
            m.PerformAction();

            m2.PerformAction();
            m2.PerformAction();

            Assert.AreEqual(m2.MyRoom, h.GetAreaByID(13));

        }

        [TestMethod]
        public void notifyGuestCheckout()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(1, 4), 5);

            m.Area = m.Hotel.GetArea(new Point(4, 4));

            Dictionary<string, string> d = new Dictionary<string, string>();

            d.Add("Gast", "5");

            HotelEvent ev = new HotelEvent();

            ev.Data = d;
            ev.EventType = HotelEventType.CHECK_OUT;

            m.Status = MovableStatus.IN_ROOM;

            m.Notify(ev);
            
            Assert.AreEqual(MovableStatus.CHECKING_OUT, m.Status);

        }

        [TestMethod]
        public void notifyGuestNeedFood()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(1, 4), 5);

            m.Area = m.Hotel.GetArea(new Point(4, 4));

            Dictionary<string, string> d = new Dictionary<string, string>();

            d.Add("Gast", "5");

            HotelEvent ev = new HotelEvent();

            ev.Data = d;
            ev.EventType = HotelEventType.NEED_FOOD;

            m.Status = MovableStatus.IN_ROOM;

            m.Notify(ev);

            Assert.AreEqual(MovableStatus.GET_FOOD, m.Status);

        }

        [TestMethod]
        public void notifyGuestToCinema()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(1, 4), 5);

            m.Area = m.Hotel.GetArea(new Point(4, 4));

            Dictionary<string, string> d = new Dictionary<string, string>();

            d.Add("Gast", "5");

            HotelEvent ev = new HotelEvent();

            ev.Data = d;
            ev.EventType = HotelEventType.GOTO_CINEMA;

            m.Status = MovableStatus.IN_ROOM;

            m.Notify(ev);

            Assert.AreEqual(MovableStatus.GOING_TO_CINEMA, m.Status);

        }

        [TestMethod]
        public void notifyGuestFitness()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(1, 4), 5);

            m.Area = m.Hotel.GetArea(new Point(4, 4));

            Dictionary<string, string> d = new Dictionary<string, string>();

            d.Add("Gast", "5");

            HotelEvent ev = new HotelEvent();

            ev.Data = d;
            ev.EventType = HotelEventType.GOTO_FITNESS;

            m.Status = MovableStatus.IN_ROOM;

            m.Notify(ev);

            Assert.AreEqual(MovableStatus.GOING_TO_FITNESS, m.Status);

        }

        [TestMethod]
        public void CheckIdle()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(1, 4), 5);

            m.Area = m.Hotel.GetArea(new Point(1, 4));

            m.Status = MovableStatus.WAITING_FOR_ELEVATOR;
            m.LastStatus = MovableStatus.EVACUATING;

            m.PerformAction();
           
            Assert.AreEqual(MovableStatus.EVACUATING, m.Status);

        }

        [TestMethod]
        public void LeavingElevator()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(1, 4), 5);

            m.Area = m.Hotel.GetArea(new Point(0, 4));

            m.Status = MovableStatus.LEAVING_ELEVATOR;
            m.LastStatus = MovableStatus.EVACUATING;

            m.PerformAction();

            Assert.AreEqual(MovableStatus.EVACUATING, m.Status);

        }

        [TestMethod]
        public void LeavingElevatorScen2()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(1, 4), 5);

            m.Area = m.Hotel.GetArea(new Point(0, 4));
            m.FinalDes = m.Hotel.GetAreaByID(15);

            m.Status = MovableStatus.LEAVING_ELEVATOR;
            m.LastStatus = MovableStatus.GOING_TO_ROOM;

            m.PerformAction();
            m.PerformAction();

            Assert.AreEqual(MovableStatus.GOING_TO_ROOM, m.Status);

        }

        [TestMethod]
        public void GoWatchMovieBob()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(1, 4), 5);

            m.Area = m.Hotel.GetArea(new Point(0, 4));
            m.FinalDes = m.Hotel.GetAreaByID(15);

            Dictionary<string, string> d = new Dictionary<string, string>();
            
            m.Status = MovableStatus.WAITING_TO_START;
            d.Add("Gast", "5");

            HotelEvent ev = new HotelEvent();

            ev.Data = d;
            ev.Data = d;
            ev.EventType = HotelEventType.START_CINEMA;
            
            m.Notify(ev);
            

            Assert.AreEqual(MovableStatus.WATCHING, m.Status);

        }

        [TestMethod]
        public void evacuateMethod()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(0, 4), 5);

            m.Area = m.Hotel.GetArea(new Point(0, 4));

            m.Status = MovableStatus.EVACUATING;
            
            m.PerformAction();

            Assert.AreEqual(m.Hotel.GetAreaByID(9), m.Path.Last());

        }

        [TestMethod]
        public void CallElevator()
        {
            Guest m = new Guest(Hotel(), "The Handler", 3, new Point(0, 4), 5);

            m.Area = m.Hotel.GetArea(new Point(0, 4));
            m.FinalDes = m.Hotel.GetAreaByID(13);

            m.Status = MovableStatus.ELEVATOR_REQUEST;
           
            m.WantsElevator = true;
            
            m.PerformAction();

            Assert.AreEqual(MovableStatus.WAITING_FOR_ELEVATOR, m.Status);

        }

    }
}
