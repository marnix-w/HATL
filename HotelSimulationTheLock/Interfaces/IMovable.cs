using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    public enum MovableStatus
    {
        IN_ROOM,
        EATING,
        WORKING_OUT,
        WATCHING_MOVIE,
        EVACUATING
        //Etc
    }
    public interface IMovable
    {
        Point Position { get; set; }
        Image Art { get; set; }
        MovableStatus Status { get; set; }
    }
}
