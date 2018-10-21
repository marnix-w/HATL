using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// A temporary class for saving some Settings
    /// </summary>
    public class SettingsModel
    {
        // This could have been implemented much better
        // unfortunately there was no time to do so

        public int HTEPerSeconds { get; set; } = 1;
        public int AmountOfMaids { get; set; }       
        public int ElevatorDuration { get; set; }
        public int ElevatorCapicity { get; set; }
        public int StairsDuration { get; set; }
        public int CinemaDuration { get; set; }
        public int RestaurantCapicity { get; set; }
        public int RestaurantDuration { get; set; }
        public int EatingDuration { get; set; }
        public int FitnessCapicity { get; set; }
    }
}
