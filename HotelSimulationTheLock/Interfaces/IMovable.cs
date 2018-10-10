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
        CHEKING_IN,
        IN_HOTEL,
        IN_ROOM,
        LEAVING,
        EATING,
        WORKING_OUT,
        WATCHING_MOVIE,
        EVACUATING,
        GOING_TO_ROOM
        //Etc
    }
    public interface IMovable
    {
        // area status
        IArea Area { get; set; }

        // properties
        Point Position { get; set; }
        Bitmap Art { get; set; }
        MovableStatus Status { get; set; }
        Dictionary<MovableStatus, Action> Actions { get; set; }

        void PerformAction();

        void SetPath(IArea destination);

    }
}
