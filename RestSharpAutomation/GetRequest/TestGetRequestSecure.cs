using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.GetRequest
{
    [TestClass]
    public class TestGetRequestSecure
    {
        private readonly string SecureUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/secure/all";
        private static IClient _client;
        private static IClient authClient;
        private static RestApiExecutor apiExecutor;
        private static RestClientOptions options;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            var options = new RestClientOptions
            {

                ThrowOnAnyError = true
            };
            _client = new TracerClient(options);
            authClient = new BasicAuthDecorator(_client);
            apiExecutor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _client?.Dispose();
        }

        [TestMethod]
        public void TestGetRequestWithBasicAuth()
        {

            var options = new RestClientOptions();
            options.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("admin", "Welcome");
            var restClient = new RestClient(options);


            var getRequest = new RestRequest()
            {
                Method = Method.Get,
                Resource = SecureUrl
            };


            var response = restClient.ExecuteGet(getRequest);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [TestMethod]
        public void TestSecureGetUsingDecorator() 
        {
            //1. Create the request using the Get Request builder class
            var getRequest = new GetRequestBuilder().WithUrl(SecureUrl);

            //2. Create the request command
            var command = new RequestCommand(getRequest, authClient);

            //3. Set the command on the API executor
            apiExecutor.SetCommand(command);

            //4. Execute command
            var response = apiExecutor.ExecuteRequest();

            //5. Capture the response
            //6. Add the validation on the response status code
            response.GetHttpStatusCode().Should().Be(HttpStatusCode.OK);


        }

    }
}
