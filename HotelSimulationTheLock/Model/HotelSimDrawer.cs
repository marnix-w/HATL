using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// Draws an Bitmap fot the hotel sim
    /// </summary>
    class HotelSimDrawer : IHotelDrawer
    {
        /// <summary>
        /// Draws an bitmap for the hotel sim
        /// </summary>
        /// <param name="areas">The hotel Areas</param>
        /// <param name="movables">The hotel Movables</param>
        /// <returns></returns>
        public Bitmap DrawHotel(List<IArea> areas, List<IMovable> movables)
        {
            // Setting the hotel height and widht to use during drawing
            int HotelWidth = areas.OrderBy(X => X.Position.X).Last().Position.X;
            int HotelHeight = areas.OrderBy(Y => Y.Position.Y).Last().Position.Y;
            
            // Setting the art size
            int artSize = Simulation.RoomArtSize;

            // Creating a bitmap with the correct dimensions
            Bitmap buffer = new Bitmap((HotelWidth + 1) * artSize, (HotelHeight) * artSize);

            // Drawing Layer 1
            #region
            // Layer 1 is and static image with all hallway images
            // This creates the emergance that its a real hotel
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

            // Drawing Layer 2
            #region
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

            // Drawing Layer 3
            #region
            // Drawing all the movables on top of the hotel
            using (Graphics graphics = Graphics.FromImage(buffer))
            {               
                lock (movables)
                {
                    try
                    {
                        // making sure the elevator is drawn first so it wont colide with other movables
                        graphics.DrawImage(movables.Find(X => X is ElevatorCart).Art, movables.Find(X => X is ElevatorCart).Position.X * artSize,
                                       (movables.Find(X => X is ElevatorCart).Position.Y - 1) * artSize);

                        foreach (IMovable movable in movables.Where(X => !(X is ElevatorCart)))
                        {
                            // on a few occations guest wont be drawn
                            // These will indicate that there in an room
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
                    catch (InvalidOperationException) // Somtimes this method crashes, we coudnt figure out why in the time we had
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
