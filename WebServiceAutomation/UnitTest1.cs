using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;

namespace WebServiceAutomation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Step 1 Create a Http Client - this is in effect our Postman
            HttpClient httpClient = new HttpClient();

            
            
            httpClient.Dispose(); //Close the connection and releases the resource


        }
    }
}
