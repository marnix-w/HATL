using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.IO;

namespace HotelSimulationTheLock
{
    public interface IAreaData
    {
        string AreaType { get; }
    }

    public class ExtendedRoomsModel
    {
        private CompositionContainer _container;

        [Import(typeof(AreaFactory))]
        public AreaFactory AreaFactory { get; set; }
        
        public ExtendedRoomsModel()
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


    }
}
