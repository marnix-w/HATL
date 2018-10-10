using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    public class SettingsModel
    {
        public int HTEPerSeconds { get; set; } = 1;
        public int AmountOfMaids { get; set; }       
        public int ElevatorDuration { get; set; }
        public int ElevatorCapicity { get; set; }
        public int StairsDuration { get; set; }
        public int CinemaDuration { get; set; }
        public int RestaurantCapicity { get; set; }
        public int EatingDuration { get; set; }
        public int FitnessCapicity { get; set; }
    }
}
