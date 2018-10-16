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
        // implemented
        CHEKING_IN,
        IN_ROOM,
        GOING_TO_ROOM,
        LEAVING,
        GET_FOOD,
        IN_ELEVATOR,


        // To implement
        EATING,
        WORKING_OUT,
        EVACUATING,

        //added
        WATCHING,
        GOING_TO_CINEMA,
        GOING_TO_FITNESS,
        CHECKING_OUT,
        WAITING_TO_START


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

        Hotel Hotel { get; set; }

        void PerformAction();

        void SetPath(IArea destination);

    }
}
