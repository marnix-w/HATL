using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting; 
using System.IO;
using System.Linq;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// An implementation of a combenation of a Factory design pattern and a bit of the MEF framework
    /// This is no a true factory nor is it a true implemtation of the MEF framework
    /// It combines the best of both to create a good "factory" for this project
    /// </summary>
    public class AreaFactory
    {
        // For this project we used a special variation on the Design pattern factory
        // this variation makes the simulation modulair
        // it implements the MEF framework to capture new types of areas and can directly work with them
        // if an area will be added they have to implement the IArea interface and by filling in the create area method 
        // it is compatable with the program. this way it is posible to add loads of new area's without having to deal with 
        // implementation in the program

        // For more information on the MEF framework go to
        // https://docs.microsoft.com/en-us/dotnet/framework/mef/
        // https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.composition?view=netframework-4.7.2

        // the container in wich the composition will be held
        private CompositionContainer _container;

        [ImportMany]
        private IEnumerable<Lazy<IArea, IAreaType>> AreaTypes;

        /// <summary>
        /// When a factory is made it will check the function for what rooms it can make
        /// </summary>
        public AreaFactory()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));

            // In development of this factory an error occured when we added an external DLL area that didnt fully implement IArea correctly 
            // This caused a mayor error in the project. after searching the web fot a fix we stumbled upon a stack overflow thread that solved our problem
            // https://stackoverflow.com/questions/4144683/handle-reflectiontypeloadexception-during-mef-composition
            // instead of reading all DLL files in at the same time. we now do each one induvidualy. then we force faulty DLL's to crash and catch the exception
            // en exclude them from the project.

            var files = Directory.EnumerateFiles(Directory.GetCurrentDirectory() + @"..\..\..\Extended_Areas", "*.dll", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                AssemblyCatalog newArea = new AssemblyCatalog(file);

                try
                {
                    // Forcing faulty DLL's to crash
                    // No idea why this causes them to crash this way
                    // would like to know
                    newArea.Parts.ToArray();
                }
                catch (System.Reflection.ReflectionTypeLoadException) // catching faulty DLL's
                {
                    // The given DLL does not implement IArea correctly please notify the creator
                    continue;
                }
                catalog.Catalogs.Add(newArea);
            }

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object                     
            _container.ComposeParts(this);
        }

        /// <summary>
        /// The Factory method that creates a new area based on the area type
        /// </summary>
        /// <param name="typeOfArea">A AreaType wich corisponds with the Area's exported metadata</param>
        /// <returns></returns>
        public IArea GetArea(string typeOfArea)
        {
            foreach (Lazy<IArea, IAreaType> i in AreaTypes)
            {
                // Truh this impematation it creates an object to return a newly created object
                // i didnt have the time to look further in to this but since the obeject isnt used
                // it will be collected thruh the GC and properly disposed.
                // One of the problems is that the constructor cannot initilize any assiciations wich
                // can be an issue in the future
                if (i.Metadata.AreaType.Equals(typeOfArea)) return i.Value.CreateArea();
            }           
            return null;

        }

    }
}