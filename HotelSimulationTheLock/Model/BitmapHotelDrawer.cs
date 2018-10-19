using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    class BitmapHotelDrawer : IHotelDrawer
    {
        public Bitmap DrawHotel(List<IArea> areas, List<IMovable> movables)
        {
            int HotelWidth = areas.OrderBy(X => X.Position.X).Last().Position.X;
            int HotelHeight = areas.OrderBy(Y => Y.Position.Y).Last().Position.Y;

            int artSize = Simulation.RoomArtSize;

            Bitmap buffer = new Bitmap((HotelWidth + 1) * artSize, (HotelHeight) * artSize);

            using (Graphics graphics = Graphics.FromImage(buffer))
            {
                lock (areas)
                {
                    for (int i = 0; i < Hotel.HotelHeight; i++)
                    {
                        for (int j = 0; j < Hotel.HotelWidth; j++)
                        {
                            graphics.DrawImage(Properties.Resources.hallway, j * artSize, i * artSize, artSize, artSize);
                        }
                    }
                }
            }
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
            using (Graphics graphics = Graphics.FromImage(buffer))
            {
                lock (areas)
                {
                    foreach (IArea area in areas)
                    {
                      
                        if (area.Dimension.Height > 1 || area.Dimension.Width > 1)
                        {                           

                            graphics.DrawImage(area.Art,
                                       area.Position.X * artSize,
                                       (area.Position.Y - 1) * artSize,
                                       area.Dimension.Width * artSize / area.Dimension.Width,
                                       area.Dimension.Height * artSize / area.Dimension.Height);
                        }



                    }


                }
            }

            using (Graphics graphics = Graphics.FromImage(buffer))
            {
                // Prevent opperation from coliding with eachother
                lock (movables)
                {
                    try
                    {
                        foreach (IMovable movable in movables)
                        {
                            //if the moveable have the status IN_ROOM we don't draw them
                            if (movable.Status != MovableStatus.IN_ROOM && movable.Status != MovableStatus.EATING && movable.Status != MovableStatus.WATCHING && movable.Status != MovableStatus.WORKING_OUT)
                            {
                                graphics.DrawImage(movable.Art,
                                       movable.Position.X * artSize,
                                       (movable.Position.Y - 1) * artSize,
                                       movable.Art.Width, movable.Art.Height);
                            }
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        return buffer;
                    }


                }
            }


            return buffer;
        }
    }
}
