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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model.JsonModel;

namespace RestSharpLatest.PostRequest
{
    [TestClass]
    public class TestPostRequest
    {
        private string postUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/api/add";
        // "https://fbe8b02e-ba55-46c3-9675-80b9483937be.mock.pstmn.io/laptop-bag/webapi/api/add";
        private readonly string SecureUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/secure/all";
        private Random random = new Random();
        private static IClient _client;
        private static IClient _clientTracer;
        private static IClient authClient;
        private static RestApiExecutor _executor;
        private static RestClientOptions options;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var options = new RestClientOptions
            {

                ThrowOnAnyError = true
            };
            _client = new DefaultClient();
            _clientTracer = new TracerClient(options);
            authClient = new BasicAuthDecorator(_clientTracer);
            _executor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown() 
        { 
            _client.Dispose(); 
        
        }

        [TestMethod]
        public void TestPostRequestWithStringBody()
        {
            int id = random.Next(100);
            //id = 1; //Needed as using a mock service
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
                                    "\"Id\": " +id + "," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";

            // Create the Client

            RestClient client = new RestClient();

            // Create the Request

            RestRequest request = new RestRequest() 
            { 
                Resource = postUrl,
                Method = Method.Post
            };


            // Add the Body to the Request

            request.AddStringBody(jsonData, DataFormat.Json);


            // Send the Request
            RestResponse response = client.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.Content);

        }

        [TestMethod]
        public void TestPostRequestWithJsonObject() 
        {
            int id = random.Next(100);


            // ------------ Old Model ---
            /*var payload = new JsonRootObjectBuilder().WithId(id).WithBrandName("Test BrandName").WithLaptopName("Test LaptopName").
                WithFeatures(new System.Collections.Generic.List<string>() { "Feature1", "Feature2"}).Build(); */

            var payload = new JsonModelBuilder().WithId(id).WithBrandName("Test BrandName").WithLaptopName("Test LaptopName").
                WithFeatures(new System.Collections.Generic.List<string>() { "Feature1", "Feature2" }).Build();

            // Create the Client

            RestClient client = new RestClient();

            // Create the Request

            RestRequest request = new RestRequest()
            {
                Resource = postUrl,
                Method = Method.Post
            };


            // Add the Body to the Request

            // --- Old Model --- request.AddJsonBody<JsonRootObject>(payload);

            request.AddJsonBody<JsonModel>(payload);
            var response = client.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.Content);


        }

        [TestMethod]
        public void TestPostRequestWithFrameworkJson() 
        {
            int id = random.Next(100);
            var payload = new JsonModelBuilder().WithId(id).WithBrandName("Test Brandname").WithLaptopName("Test LaptopName").
                    WithFeatures(new System.Collections.Generic.List<string>() { "Feature1", "Feature2"}).Build();

            // Post Request
            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<JsonModel>(payload, PostRequestBuilder.RequestBodyType.JSON);

            // Command
            var command = new RequestCommand(request, _client);

            // SetCommand
            _executor.SetCommand(command);

            // Execute the Request
            var response = _executor.ExecuteRequest();

            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("Test LaptopName");

        }

        [TestMethod]
        public void TestPostRequestWithFramework_String()
        {
            int id = random.Next(100);

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

            // Post Request
            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<string>(jsonData, PostRequestBuilder.RequestBodyType.STRING);

            // Command
            var command = new RequestCommand(request, _client);

            // SetCommand
            _executor.SetCommand(command);

            // Execute the Request
            var response = _executor.ExecuteRequest();

            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("8th Generation Intel® Core™ i5-8300H");

            var responseType = _executor.ExecuteRequest<JsonModel>();
            responseType.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var data = responseType.GetResponse();
            data.Features.Feature.Should().Contain("8th Generation Intel® Core™ i5-8300H");

        }

        [TestMethod]
        public void TestPostRequestWithXML_String()
        {
            int id = random.Next(100);
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

            // Create the Client
            RestClient client = new RestClient();

            // Create the Request
            RestRequest request = new RestRequest()
            {
                Resource = postUrl,
                Method = Method.Post
            };

            request.AddStringBody(xmlData, DataFormat.Xml);
            request.AddHeader("Accept", "application/xml");

            var response = client.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.Content);
            client.Dispose();

        }

        [TestMethod]
        public void TestPostRequestWithDe_serialization_XML_body()
        {
            int id = random.Next(100);
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

            // Create the Client
            RestClient client = new RestClient();

            // Create the Request
            RestRequest request = new RestRequest()
            {
                Resource = postUrl,
                Method = Method.Post
            };

            request.AddStringBody(xmlData, DataFormat.Xml);
            request.AddHeader("Accept", "application/xml");

            var response = client.ExecutePost<Laptop>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.Content);
            client.Dispose();

            // Validate the BrandName property.
            response.Data.BrandName.Should().NotBeNull();
            // Validate the LaptopName property.
            response.Data.LaptopName.Should().NotBeNull();

            // Release the resource acquired by the client.
            client?.Dispose();

        }


        [TestMethod]
        public void TestPostRequestWithXmlObject()
        {
            int id = random.Next(100);
            string newId = id.ToString();


            var payload = new XmlModelBuilder().WithId(newId).WithBrandName("XmlTest BrandName").WithLaptopName("XmlTest LaptopName").
                WithFeatures(new System.Collections.Generic.List<string>() { "XmlFeature1", "XmlFeature2" }).Build();

            // Create the Client

            RestClient client = new RestClient();

            // Create the Request

            RestRequest request = new RestRequest()
            {
                Resource = postUrl,
                Method = Method.Post
            };


            // Add the Body to the Request

            // --- Old Model --- request.AddJsonBody<JsonRootObject>(payload);

            request.AddXmlBody<Laptop>(payload);
            request.AddHeader("Accept", "application/xml");
            var response = client.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.Content);


        }

        [TestMethod]
        public void TestPostRequestWithFramework_XML()
        {
            int id = random.Next(100);
            string newId = id.ToString();
            var xmlPayload = new XmlModelBuilder().WithId(newId).WithBrandName("Test Brandname").WithLaptopName("Test LaptopName").
                    WithFeatures(new System.Collections.Generic.List<string>() { "Feature1", "Feature2" }).Build();

            // Post Request
            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<Laptop>(xmlPayload, PostRequestBuilder.RequestBodyType.XML).
                WithHeaders(new Dictionary<string, string>() { { "Accept", "application/xml"} });

            // Command
            var command = new RequestCommand(request, _client);

            // SetCommand
            _executor.SetCommand(command);

            // Execute the Request
            var response = _executor.ExecuteRequest();

            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("Test LaptopName");

        }

        [TestMethod]
        public void TestPostRequestWithFrameworkXML_String()
        {
            int id = random.Next(100);

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

            // Post Request
            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<string>(xmlData, PostRequestBuilder.RequestBodyType.STRING, ContentType.Xml).
                WithHeaders(new Dictionary<string, string> () { { "Accept", "application/xml"} });

            // Command
            var command = new RequestCommand(request, _client);

            // SetCommand
            _executor.SetCommand(command);

            // Execute the Request
            var response = _executor.ExecuteRequest();

            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("8th Generation Intel® Core™ i5 - 8300H");

            var responseType = _executor.ExecuteRequest<Laptop>();
            responseType.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var data = responseType.GetResponse();
            data.Features.Feature.Should().Contain("8th Generation Intel® Core™ i5 - 8300H");

        }

        [TestMethod]
        public void TestSecurePostRequestWithFrameworkJson()
        {
            int id = random.Next(1000);
            var payload = new JsonModelBuilder().WithId(id).WithBrandName("Test Alienware Brandname").WithLaptopName("Test Alienware LaptopName").
                    WithFeatures(new System.Collections.Generic.List<string>() { "8th Generation Intel® Core™ i5-8300H", "Windows 10 Home 64-bit English" }).Build();

            // Post Request
            var request = new PostRequestBuilder().WithUrl(SecureUrl).WithBody<JsonModel>(payload, PostRequestBuilder.RequestBodyType.JSON);

            // Command
            var command = new RequestCommand(request, authClient);

            // SetCommand
            _executor.SetCommand(command);

            // Execute the Request
            var response = _executor.ExecuteRequest();

            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("Test Alienware LaptopName");

        }
    }
}
