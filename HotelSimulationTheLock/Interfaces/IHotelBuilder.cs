using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    public interface IHotelBuilder
    {
        List<IArea> BuildHotel<T>(T file, SettingsModel settings);

        List<IMovable> BuildMovable(SettingsModel settings, Hotel hotel);
    }
}
