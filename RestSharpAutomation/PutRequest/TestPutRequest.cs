using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
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

namespace RestSharpLatest.PutRequest
{
    [TestClass]
    public class TestPutRequest
    {

        // POST - Create the entry in the test application
        // PUT  - Update the created entry
        // GET  - Fetch the entry and verify it

        private readonly string PostUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/api/add";
        private readonly string PutUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/api/update";
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
        public void TestPutRequestWithFramework_XML()
        {
            // POST - Create the entry in the test application
            // PUT  - Update the created entry
            // GET  - Fetch the entry and verify it
            // 

            int id = random.Next(1000);
            string newId = id.ToString();
            
            var xmlBody = new XmlModelBuilder().WithId(newId).WithLaptopName("Alienware M17").WithBrandName("Alienware").WithFeatures(new List<string>() 
            { "8th Generation Intel® Core™ i5 - 8300H", "Windows 10 Home 64 - bit English"}).Build();

            var postrequest = new PostRequestBuilder().WithUrl(PostUrl)
                                                      .WithBody(xmlBody, PostRequestBuilder.RequestBodyType.XML)
                                                      .WithHeaders(new Dictionary<string, string>() { { "Accept", "application/xml" } });

            var command = new RequestCommand(postrequest, Client);
            apiExecutor.SetCommand(command);

            var response = apiExecutor.ExecuteRequest<Laptop>();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);


            // PUT Request
            var putrequestBody = new XmlModelBuilder().WithId(newId).WithLaptopName("Alienware M17").WithBrandName("Alienware").WithFeatures(new List<string>()
            { "8th Generation Intel® Core™ i5 - 8300H", "Windows 10 Home 64 - bit English", "Updated Feature"}).Build();

            var putrequest = new PutRequestBuilder().WithUrl(PutUrl)
                .WithBody(putrequestBody, PostRequestBuilder.RequestBodyType.XML)
                .WithHeaders(new Dictionary<string, string>() { { "Accept", "application/xml" } });

            command = new RequestCommand(putrequest, Client);
            apiExecutor.SetCommand(command);

            response = apiExecutor.ExecuteRequest<Laptop>();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);


            // GET Request
            var getrequest = new GetRequestBuilder().WithUrl(GetUrl).WithHeader(new Dictionary<string, string>() { { "Accept", "application/xml" } });

            command = new RequestCommand(getrequest, Client);
            apiExecutor.SetCommand(command);
            response = apiExecutor.ExecuteRequest<Laptop>(); 
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponse().Features.Feature.Should().Contain("Updated Feature");


        }

        [TestMethod]
        public void TestPutRequestWithJson()
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
            var response = client.ExecutePost<JsonModel>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            
            
            
            // PUT  - Update the created entry 

            jsonData = "{" +
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
                                    "\"LaptopName\": \"Alienware SDK\"" +
                                "}";

            request = new RestRequest()
            {
                Resource = PutUrl,
                Method = Method.Put
            };

            // Add the Body to the Request

            request.AddStringBody(jsonData, DataFormat.Json);


            // Send the Put Request
            response = client.ExecutePut<JsonModel>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);



            // GET  - Fetch the entry and verify it

            request = new RestRequest()
            {
                Resource = GetUrl + id,
                Method = Method.Get
            };


            // Send the Get Request
            response = client.ExecuteGet<JsonModel>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Data.LaptopName.Should().Be("Alienware SDK");


        }

        [TestMethod]
        public void TestPutRequestWithXml()
        {
            int id = random.Next(1000);
            string xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                        "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                        "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                        "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                        "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                    "</Features>" +
                                    "<Id>" + id + "</Id>" +
                                    "<LaptopName>Alienware M17</LaptopName>" +
                             "</Laptop>";

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

            request.AddStringBody(xmlData, DataFormat.Xml);
            


            // Send the Post Request
            var response = client.ExecutePost<Laptop>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);




            // PUT  - Update the created entry 

            xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                        "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                        "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                        "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                        "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                        "<Feature>New Feature</Feature>" +
                                    "</Features>" +
                                    "<Id>" + id + "</Id>" +
                                    "<LaptopName>Alienware SDK2</LaptopName>" +
                             "</Laptop>";

            request = new RestRequest()
            {
                Resource = PutUrl,
                Method = Method.Put
            };

            // Add the Body to the Request

            request.AddStringBody(xmlData, DataFormat.Xml);


            // Send the Put Request
            response = client.ExecutePut<Laptop>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);



            // GET  - Fetch the entry and verify it

            request = new RestRequest()
            {
                Resource = GetUrl + id,
                Method = Method.Get
            };

            // Add the request header to accept the response in XML
            request.AddHeader("Accept", "application/xml");

            // Send the Get Request
            response = client.ExecuteGet<Laptop>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            
            // Validate the new value in Feature property.
            response.Data.Features.Feature.Contains("New Feature");

            // Validate the new value in Laptopname
            response.Data.LaptopName.Should().NotBeNull();
            response.Data.LaptopName.Contains("New Feature");

            client.Dispose();


        }

    }
}
