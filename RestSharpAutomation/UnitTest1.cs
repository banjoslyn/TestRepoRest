using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;

namespace RestSharpLatest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            /** Steps we will be following
             * 1. Create the client
             * 2. Create the request
             * 3. Send the request
             * 4. Capture the response
             * 5. Add the verification on the response.
             * **/

            //Create the client
            RestClient client = new RestClient();

            
            //Create the request
            RestRequest request = new RestRequest();




        }
    }
}
