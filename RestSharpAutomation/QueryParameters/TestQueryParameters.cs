using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIModel.JsonApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Command;
using RestSharpLatest.APIModel.XmlApiModel;

namespace RestSharpLatest.QueryParameters
{
    [TestClass]
    public class TestQueryParameters
    {
        private readonly string PostUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/api/add";
        private readonly string QueryUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/api/query";
        private Random random = new Random();

        private static IClient _client;
        private static RestApiExecutor _executor;
        private static RestClientOptions options;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var options = new RestClientOptions
            {

                ThrowOnAnyError = true
            };
            _client = new TracerClient(options);
            _executor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _client.Dispose();

        }

        
        [TestMethod]
        public void TestQueryParametersWithJson()
        {
            // POST - Create a new entry in the test application
            // GET  - Retrieve the new entry using Query Parameters

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


            request = new RestRequest()
            {
                Resource = QueryUrl,
                Method = Method.Get
            };

            request.AddParameter("id", id);
            request.AddParameter("laptopName", "Alienware M17");

            //request.AddQueryParameter("id", id); // For PUT and POST requests use 'AddQueryParameter'

            var getresponse = client.ExecuteGet<JsonModel>(request);
            getresponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            getresponse.Data.Id.Should().Be(id);
            getresponse.Data.LaptopName.Should().Be("Alienware M17");



        }

        [TestMethod]
        public void TestGetWithQueryParams_Framework()
        {
            int id = random.Next(1000);
            var payload = new JsonModelBuilder().WithId(id).WithBrandName("Test Brandname").WithLaptopName("Test LaptopName").
                    WithFeatures(new System.Collections.Generic.List<string>() { "Feature1", "Feature2" }).Build();

            // Post Request
            var request = new PostRequestBuilder().WithUrl(PostUrl).WithBody<JsonModel>(payload, PostRequestBuilder.RequestBodyType.JSON);

            // Command
            var command = new RequestCommand(request, _client);

            // SetCommand
            _executor.SetCommand(command);

            // Execute the Request
            var response = _executor.ExecuteRequest();

            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            var getrequest = new GetRequestBuilder().WithUrl(QueryUrl).WithHeader(new Dictionary<string, string>() { { "Accept", "application/xml" } })
                .WithQueryParameters(new Dictionary<string, string>() { {"id", id.ToString() }, {"laptopName", "Test LaptopName" } });


            command = new RequestCommand(getrequest, _client);

            _executor.SetCommand(command);

            var getresponse = _executor.ExecuteRequest<Laptop>();
            getresponse.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            getresponse.GetResponse().Id.Should().Be(id.ToString());
            getresponse.GetResponse().LaptopName.Should().Be("Test LaptopName");


        }



    }
}
