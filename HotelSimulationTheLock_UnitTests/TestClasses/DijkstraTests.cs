﻿using System;
using HotelEvents;
using System.Drawing;
using HotelSimulationTheLock;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class DijkstraTests
    {                       
        [TestMethod]
        public void TestDijkstraPathfinding()
        {
            //arrange
            List<IArea> l;
            List<IArea> g;
            List<IArea> f;

            // act
            l = new List<IArea>();
            

            for (int i = 0; i < 9; i++)
            {
                l.Add(new Room() { ID = i, Position = new Point(11,12)});
                
            }

            JsonHotelBuilder.AddDirectedEdge(l[7], l[0], 3);
            JsonHotelBuilder.AddDirectedEdge(l[7], l[2], 6);
            JsonHotelBuilder.AddDirectedEdge(l[7], l[1], 6);
            JsonHotelBuilder.AddDirectedEdge(l[3], l[8], 12);
            JsonHotelBuilder.AddDirectedEdge(l[6], l[8], 5);
            JsonHotelBuilder.AddDirectedEdge(l[3], l[5], 4);
            JsonHotelBuilder.AddDirectedEdge(l[5], l[6], 4);
            JsonHotelBuilder.AddDirectedEdge(l[4], l[6], 9);
            JsonHotelBuilder.AddDirectedEdge(l[4], l[3], 2);
            JsonHotelBuilder.AddDirectedEdge(l[1], l[3], 1);
            JsonHotelBuilder.AddDirectedEdge(l[0], l[3], 3);
            JsonHotelBuilder.AddDirectedEdge(l[0], l[4], 4);
            JsonHotelBuilder.AddDirectedEdge(l[2], l[4], 6);
            JsonHotelBuilder.AddDirectedEdge(l[6], l[3], 4);

            Hotel h = new Hotel();
            h.HotelAreas = l;       
            Dijkstra.IntilazeDijkstra(h);

            g = Dijkstra.GetShortestPathDijkstra(l[7], l[8]);
            
            f = new List<IArea>();
            f.Add(l[7]);
            f.Add(l[0]);
            f.Add(l[3]);
            f.Add(l[8]);

            //assert
            Assert.AreEqual(f[0].ID, g[0].ID);
            Assert.AreEqual(f[1].ID, g[1].ID);
            Assert.AreEqual(f[2].ID, g[2].ID);
            Assert.AreEqual(f[3].ID, g[3].ID);
        }

        [TestMethod]
        public void TestIfElevatorIsCloser()
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

            Hotel hotel = new Hotel(null, g, new StubedHotelBuilder());

            IMovable t = new Receptionist(new Point(2,4), hotel);

            IArea a = Dijkstra.IsElevatorCloser(t.Area, hotel.GetAreaByID(21));

            Assert.AreEqual(a.GetType(), typeof(Elevator));

            IMovable p = new Receptionist(new Point(8, 4), hotel);

            IArea q = Dijkstra.IsElevatorCloser(p.Area, hotel.GetAreaByID(21));

            Assert.AreNotEqual(g.GetType(), typeof(Elevator));
        }
    }



}
