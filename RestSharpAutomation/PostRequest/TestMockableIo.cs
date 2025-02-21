using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestSharpLatest.PostRequest
{
    [TestClass]
    public class TestMockableIo
    {
        private readonly string PostUrl = "http://demo5805552.mockable.io";
        private static IClient _client;
        private static RestApiExecutor apiExecutor;
        private static RestClientOptions options;

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            var options = new RestClientOptions
            {

                ThrowOnAnyError = true
            };
            _client = new TracerClient(options);
            apiExecutor = new RestApiExecutor();

        }

        [TestMethod]
        public void  ParseAndValidateTheJson()
        {
            // Create the request

            var postRequest = new PostRequestBuilder().WithUrl(PostUrl);

            // Create the command

            var command = new RequestCommand(postRequest, _client);

            // Set the command

            apiExecutor.SetCommand(command);

            // Execute the command

            var response = apiExecutor.ExecuteRequest();


            // Capture the response
            
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var responseContent = response.GetResponseData();
            Debug.WriteLine(responseContent);

            // Parse the Given Json Document

            JObject jsonToValidate = JObject.Parse(responseContent);

            // Use the json path to query the json document
            // type caste the result of the query if necessary
            var loggedOut = (bool)jsonToValidate.SelectToken("$.responseContext.mainAppWebResponseContext.loggedOut");


            loggedOut.Should().BeTrue();

            var headerJsonObject = jsonToValidate.SelectToken("$.header");

            Header headers = JsonSerializer.Deserialize<Header>(headerJsonObject.ToString());

            headers.feedTabbedHeaderRenderer.Should().NotBeNull();

            
        }

        [ClassCleanup]
        public static void TearDown() 
        { 
            _client.Dispose();

        }

        public class Runs
        {
            public string text { get; set; }

        }
        public class Title
        {
            public IList<Runs> runs { get; set; }

        }
        public class FeedTabbedHeaderRenderer
        {
            public Title title { get; set; }

        }
        public class Header
        {
            public FeedTabbedHeaderRenderer feedTabbedHeaderRenderer { get; set; }

        }
        public class Application
        {
            public Header header { get; set; }

        }


    }
}
