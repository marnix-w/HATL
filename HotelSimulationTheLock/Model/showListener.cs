using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    class showListener : HotelEventListener
    {
        public List<string> DumpData = new List<string>();

        public void Notify(HotelEvent evt)
        {
            Console.WriteLine("#- Begin Event Dump -#");
            Console.WriteLine($"Message = {evt.Message}");
            Console.WriteLine($"Time = {evt.Time}");
            Console.WriteLine($"Type = {evt.EventType}");

            if (!(evt.Data is null))
            {
                foreach (var item in evt.Data)
                {
                    Console.WriteLine($"Data.value = {item.Value}");
                    Console.WriteLine($"Data.Key = {item.Key}");

                    DumpData.Add($"{item.Value}");
                    DumpData.Add($"{item.Key}");
                }

            }
            Console.WriteLine("#- End Event Dump -#");
            Console.WriteLine();
        }

        public void DumpMessage(Simulation messageOutput)
        {
        
        }

    }
}
