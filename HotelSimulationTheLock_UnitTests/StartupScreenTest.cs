using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulationTheLock;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace HotelSimulationTheLock_UnitTests
{
    [TestClass]
    public class StartupScreenTest
    {
        [TestMethod]
        public void TestIfThePathRouteIsNotNull()
        {
            //arrange         
            StartupScreen test_startupscreen;

            //act       
            test_startupscreen = new StartupScreen();
           
         
            //assert
            Assert.IsNotNull(test_startupscreen._path);           
        }

        [TestMethod]
        public void TestIfJsonFileIsDesirialized()
        {
            //arrange         
            StartupScreen test_startupscreen;
            string test_path;        

            //act       
            test_startupscreen = new StartupScreen();            
            test_path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\..\HotelSimulationTheLock\Assets\Libraries\Hotel_reparatie.layout");
            test_startupscreen.layout = test_startupscreen.ReadLayoutJson(test_path);

          
            //assert
            Assert.IsNotNull(test_startupscreen.layout);
        }

        [TestMethod]
        public void TestIfPathIsNotFound()
        {
            //arrange         
            StartupScreen test_startupscreen;
            string test_path;

            //act       
            test_startupscreen = new StartupScreen();
            test_path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\HotelSimulationTheLock\Assets\Libraries\Hotel_reparatie.layout");
            test_startupscreen.layout = test_startupscreen.ReadLayoutJson(test_path);

            //assert
            Assert.IsNull(test_startupscreen.layout);
        }

        [TestMethod]
        public void TestIfLabelIsNotNullOnStartUp()
        {
            //arrange         
            StartupScreen test_startupscreen;

            //act       
            test_startupscreen = new StartupScreen();            


            //assert
            Assert.IsNotNull(test_startupscreen.maid_LB.Text);
        }

        [TestMethod]
        public void TestIfJsonFileIsEmptyOrNot()
        {
            //arrange         
            StartupScreen test_startupscreen;

            //act       
            test_startupscreen = new StartupScreen();

            //assert
            Assert.IsNotNull(test_startupscreen.ReadLayoutJson(Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\..\HotelSimulationTheLock\Assets\Libraries\Hotel_reparatie.layout")));
           
        }
    }
}
