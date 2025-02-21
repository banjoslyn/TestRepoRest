using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using RestSharpLatest.APIModel.JsonApiModel;
using RestSharpLatest.APIModel.XmlApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.DeleteRequest
{
    [TestClass]
    public class TestDeleteRequest
    {
        // POST - Create an entry in the test application
        // DELETE - Delete the entry
        // GET - Fetch the entry, 404 should be returned.

        private readonly string PostUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/api/add";
        private readonly string DeleteUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/api/delete/";
        private readonly string GetUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/api/find/";
        private Random random = new Random();
        private static IClient Client;
        private static RestApiExecutor apiExecutor;
        private static RestClientOptions options;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            var options = new RestClientOptions
            {

                ThrowOnAnyError = true
            };
            Client = new TracerClient(options);
            apiExecutor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void Teardown()
        {
            Client?.Dispose();
        }


        [TestMethod]
        public void TestDeleteRequestWithJson()
        {
            int id = random.Next(1000);
            string jsonData = "{" +
                                    "\"BrandName\": \"Alienware\", " +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\", " +
                                    "\"Windows 10 Home 64-bit English\", " +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\", " +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " + id + "," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";

            // POST - Create the entry in the test application

            // Create the Client

            RestClient client = new RestClient();

            // Create the Request

            RestRequest request = new RestRequest()
            {
                Resource = PostUrl,
                Method = Method.Post
            };


            // Add the Body to the Request

            request.AddStringBody(jsonData, DataFormat.Json);


            // Send the Post Request
            var response = client.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);




            // DELETE  - Delete the created entry 

            request = new RestRequest()
            {
                Resource = DeleteUrl + id,
                Method = Method.Delete
            };

            request.AddHeader("Accept", "text/plain");

            response = client.Delete(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);


            // GET  - Fetch the entry, 404 should be returned.

            request = new RestRequest()
            {
                Resource = GetUrl + id,
                Method = Method.Get
            };

            response = client.ExecuteGet(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);


        }


        [TestMethod]

        public void TestDeleteRequestWithFramework()
        {
            int id = random.Next(1000);
            string newId = id.ToString();

            var xmlrequestBody = new XmlModelBuilder().WithId(newId).WithBrandName("Test BrandName").
                WithLaptopName("Test Laptopname").WithFeatures(new List<string>() { "one", "two" }).Build();

            var postrequest = new PostRequestBuilder().WithUrl(PostUrl).WithBody(xmlrequestBody, PostRequestBuilder.RequestBodyType.XML);

            var command = new RequestCommand(postrequest, Client);
            apiExecutor.SetCommand(command);

            var response = apiExecutor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            var deleterequest = new DeleteRequestBuilder().WithUrl(DeleteUrl + id).WithDefaultHeader();

            command = new RequestCommand(deleterequest, Client);
            apiExecutor.SetCommand(command);
            response = apiExecutor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);


            var getrequest = new GetRequestBuilder().WithUrl(GetUrl + id);
            command = new RequestCommand(getrequest, Client);
            apiExecutor.SetCommand(command);
            response = apiExecutor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.NotFound);








        }

        }
    }
