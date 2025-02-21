using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MsTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod, TestCategory("Smoke")]
        public void TestMethod1()
        {
            Console.WriteLine("Test Method One");
        }

        [TestMethod]
        [Ignore]
        public void TestMethod2()
        {
            Console.WriteLine("Test Method Two");
        }

        [TestInitialize]
        public void Setup() 
        {
            Console.WriteLine("This is Test Setup");
                    
        }

        [TestCleanup]
        public void TearDown()
        { 
            Console.WriteLine("This is Test Clean up"); 
        }

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        { 
            Console.WriteLine("Class Setup");
        
        }

        [ClassCleanup]
        public static void ClassTearDown()
        {
            Console.WriteLine("Class Clean Up");

        }

        [AssemblyInitialize]
        public static void AssemblySetup(TestContext testContext)
        {
            Console.WriteLine("Assembly Set up");
        }

        [AssemblyCleanup]
        public static void AssemblyTeardown()
        { 
            Console.WriteLine("Assembly Clean up");
        
        }

    }

}
