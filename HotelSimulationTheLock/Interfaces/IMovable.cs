using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSimulationTheLock
{
    public enum MovableStatus
    {
        LOBBY,
        IN_ROOM,
        EATING,
        WORKING_OUT,
        WATCHING_MOVIE,
        EVACUATING
        //Etc
    }
    public interface IMovable
    {
        // area status
        IArea area { get; set; }

        // properties
        Point Position { get; set; }
        Bitmap Art { get; set; }
        MovableStatus Status { get; set; }

    }
}
