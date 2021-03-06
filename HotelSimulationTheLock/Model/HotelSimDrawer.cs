﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// Draws an Bitmap for the hotel simulation
    /// </summary>
    class HotelSimDrawer : IHotelDrawer
    {
        /// <summary>
        /// Draws an bitmap for the hotel simulation
        /// </summary>
        /// <param name="areas">The hotel areas</param>
        /// <param name="movables">The hotel movables</param>
        /// <returns></returns>
        public Bitmap DrawHotel(List<IArea> areas, List<IMovable> movables)
        {
            // Setting the hotel height and width to use during drawing
            int HotelWidth = areas.OrderBy(X => X.Position.X).Last().Position.X;
            int HotelHeight = areas.OrderBy(Y => Y.Position.Y).Last().Position.Y;
            
            // Setting the art size
            int artSize = Simulation.RoomArtSize;

            // Creating a bitmap with the correct dimensions
            Bitmap buffer = new Bitmap((HotelWidth + 1) * artSize, (HotelHeight) * artSize);

            #region Layer 1
            // Layer 1 is a static image with all hallway images
            // This creates the emergence that its a real hotel
            using (Graphics graphics = Graphics.FromImage(buffer))
            {
                // Loading a hallway image for layer 1
                Bitmap HallwayIamge = Properties.Resources.hallway;

                lock (areas)
                {
                    for (int i = 0; i < Hotel.HotelHeight; i++)
                    {
                        for (int j = 0; j < Hotel.HotelWidth; j++)
                        {
                            graphics.DrawImage(HallwayIamge, j * artSize, i * artSize, artSize, artSize);
                        }
                    }
                }
            }
            #endregion
            
            #region Layer 2
            // Drawing all the areas in the hotel over the hallways to create the rooms
            using (Graphics graphics = Graphics.FromImage(buffer))
            {
                lock (areas)
                {
                    foreach (IArea area in areas)
                    {
                        graphics.DrawImage(area.Art,
                                            area.Position.X * artSize,
                                            (area.Position.Y - 1) * artSize,
                                            artSize,
                                            artSize);
                    }
                }
            }
            #endregion
            
            #region Layer 3
            // Drawing all the movables on top of the hotel
            using (Graphics graphics = Graphics.FromImage(buffer))
            {               
                lock (movables)
                {
                    try
                    {
                        // making sure the elevator is drawn first so it won't collide with other movables
                        graphics.DrawImage(movables.Find(X => X is ElevatorCart).Art, movables.Find(X => X is ElevatorCart).Position.X * artSize,
                                       (movables.Find(X => X is ElevatorCart).Position.Y - 1) * artSize);

                        foreach (IMovable movable in movables.Where(X => !(X is ElevatorCart)))
                        {
                            // Skip drawing when exiting the hotel during an evacuation
                            if (movable.Status == MovableStatus.EVACUATING && movable.Area is Reception)
                            {
                                continue;
                            }

                            // On a few occasions guests won't be drawn
                            // These will indicate that they are in a room
                            if (movable.Status != MovableStatus.IN_ROOM && 
                                movable.Status != MovableStatus.EATING && 
                                movable.Status != MovableStatus.WATCHING && 
                                movable.Status != MovableStatus.WORKING_OUT)
                            {
                                graphics.DrawImage(movable.Art,
                                       movable.Position.X * artSize,
                                       (movable.Position.Y - 1) * artSize,
                                       movable.Art.Width, movable.Art.Height);
                            }
                        }
                    }
                    catch (InvalidOperationException) // Sometimes this method crashes, we couldn't figure out why in the time we had
                    {
                        return buffer;
                    }
                }
            }
            #endregion

            // Returning the new frame
            return buffer;
        }
    }
}
