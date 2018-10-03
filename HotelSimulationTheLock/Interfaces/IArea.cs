using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;


namespace HotelSimulationTheLock
{
    public enum AreaStatus
    {
        EMPTY,
        OCCUPIED,
        NEED_CLEANING
        //Etc
    }
    
    public interface IAreaType
    {
        string AreaType { get; }
    }
    
    public interface IArea
    {
        // Properties
        Point Position { get; set; }
        Point Dimension { get; set; }
        int Capacity { get; set; }
        Image Art { get; set; }
        AreaStatus AreaStatus { get; set; }

        // Properties for dijkstra search
        double? BackTrackCost { get; set; }
        IArea NearestToStart { get; set; }
        bool Visited { get; set; }
        /// <summary>
        /// IArea: Conected to, Int: Time to treverse in HTE
        /// </summary>
        Dictionary<IArea, int> Edge { get; set; }
        

        // Functions
        IArea CreateArea(Point position, int capacity, Point dimension, int clasification);

    }
}
