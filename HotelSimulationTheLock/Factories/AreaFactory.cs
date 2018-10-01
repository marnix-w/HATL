using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    public interface IAreaData
    {
        string AreaType { get; }
    }

   
    public class AreaFactory 
    {
        private CompositionContainer _container;

        [ImportMany]
        IEnumerable<Lazy<IArea, IAreaData>> AreaTypes;

        public AreaFactory()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog(Directory.GetCurrentDirectory() + @"..\..\..\Extended_Areas"));

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                _container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        public IArea GetArea(string typeOfArea, Point position, int capacity, Point dimension, int clasification)
        {
            foreach (Lazy<IArea, IAreaData> i in AreaTypes)
            {
                if (i.Metadata.AreaType.Equals(typeOfArea)) return i.Value.CreateArea(position, capacity, dimension, clasification);
            }

            //Error handeling
            Debug.WriteLine("Error there was no requested ruum");

            return null;

        }
      
    }
}
