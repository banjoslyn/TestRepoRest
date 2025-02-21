using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharpLatest.GetRequest
{
    [TestClass]
    public class TestGetWithFramework
    {

        private static IClient client;  //Defined as 'static' as it will be initialized by a static method, 'SetUp'.
        private static RestApiExecutor executor; //Defined as 'static' as it will be initialized by a static method, 'SetUp'.
        private static readonly string getUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/api/all";

        //"https://23700400-76a8-4d3c-be0f-3531a0cc016f.mock.pstmn.io/getLaptopMulti3";

        private static readonly string getUrlXml = "https://laptopbag.onrender.com/laptop-bag/webapi/api/all";

        //"https://23700400-76a8-4d3c-be0f-3531a0cc016f.mock.pstmn.io/getLaptopMultiXml";

        [ClassInitialize] //ClassInitialize identifies a method that contains code that must be used before any of the tests in the TestClass have run 
                          //and to allocate resources to be used by the TestClass. In this case, we have chosen to call the method 'SetUp'.

#pragma warning disable IDE0060
        public static void SetUp(TestContext testContext) 
        {
            client = new DefaultClient();
            executor = new RestApiExecutor();
        }
#pragma warning restore IDE0060

        [TestMethod]
        public void GetRequest()
        {
            AbstractRequest request = new GetRequestBuilder().WithUrl(getUrl); //Create the Request using the GetRequestBuilder class to set the URL.
            ICommand getCommand = new RequestCommand(request, client); //Create the Command by providing the request and the client.
            executor.SetCommand(getCommand); //Set the Command using the exector.
            var response = executor.ExecuteRequest(); //Execute the Request and store the Response in a variable.

            response.GetHttpStatusCode().Should().Be(HttpStatusCode.OK);
            Debug.WriteLine(response.GetResponseData());

            //Sending the Request to another endpoint.
            request = new GetRequestBuilder().WithUrl("http://www.google.com");
            getCommand = new RequestCommand(request, client);
            executor.SetCommand(getCommand);
            response = executor.ExecuteRequest();
            Debug.WriteLine("");
            Debug.WriteLine(response.GetResponseData());

        }

        [TestMethod]
        public void GetRequestWithDeserialization()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };

            AbstractRequest request = new GetRequestBuilder().WithUrl(getUrl).WithHeader(headers); //Create the Request using the GetRequestBuilder class to set the URL.
            ICommand getCommand = new RequestCommand(request, client); //Create the Command by providing the request and the client.
            executor.SetCommand(getCommand); //Set the Command using the exector.
            var response = executor.ExecuteRequest<LaptopDetailss>(); //Execute the Request and store the Response in a variable.

            response.GetHttpStatusCode().Should().Be(HttpStatusCode.OK);
            response.GetResponse().Laptop.ForEach((item) => {
                Debug.WriteLine($"Response ID is - {item.Id}");
            });

            Laptop rootObject = response.GetResponse().Laptop.Find((item) =>  //Filters the response data for the a laptop with an Id of 3.
            {
                return "1".Equals(item.Id, System.StringComparison.OrdinalIgnoreCase);
            });

            rootObject.BrandName.Should().NotBeEmpty();          //Uses FluentAssertion package which is much simpler to use than out of box Assert class.
            rootObject.BrandName.Should().Be("Alienware");      //Uses FluentAssertion package which is much simpler to use than out of box Assert class.

            rootObject.Features.Feature.Should().NotBeEmpty(); //Uses FluentAssertion package which is much simpler to use than out of box Assert class.
            rootObject.Features.Feature.Should().Contain("Windows 10 Home 64-bit English"); //Uses FluentAssertion package which is much simpler to use than out of box Assert class.

            

        }

        [ClassCleanup] //ClassCleanup identifies a method that contains code that must be used after all tests in the TestClass have run 
        //and to free resources obtained by the TestClass. In this case, we have chosen to call the method 'Teardown'.

        public static void Teardown() 
        { 
            client?.Dispose();
        }
    }
}
