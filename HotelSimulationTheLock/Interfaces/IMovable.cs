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
        WATCHING_MOVIE,
        EVACUATING,
       
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
