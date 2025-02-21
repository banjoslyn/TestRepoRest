using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.APIResponse;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using RestSharpLatest.JsonWebToken.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.JsonWebToken
{
    [TestClass]
    public class TestGetSecureWithJwt
    {
        private static readonly string BaseUrl = "http://localhost:9191/";
        private static RestApiExecutor apiExecutor;
        private static IClient client;

        [ClassInitialize]
        public static void Setup(TestContext context) 
        {
            // Client Options
            var options = new RestClientOptions
            {
                // Set the custom Authenticator
                Authenticator = new JsonWebTokenAuthenticator(BaseUrl, new User()
                {
                    Username = "Brian_3",
                    Password = "passwordbj3"
                })
            };

            // Create the Client

            client = new TracerClient(options);

            apiExecutor = new RestApiExecutor();

        }

        [ClassCleanup]
        public static void Cleanup() 
        {
            client?.Dispose();    
        
        }

        [TestMethod]
        public void SecureGet_With_Jwt()
        {
            var getRequest = new GetRequestBuilder().WithUrl(BaseUrl + "auth/webapi/all");
            var command = new RequestCommand(getRequest, client);
            apiExecutor.SetCommand(command);

            var response = apiExecutor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            response = apiExecutor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

        }
        [TestMethod]
        public void MySecureGet_With_Jwt_RestClient()
        {
            // Client Options
            var options = new RestClientOptions
            {
                // Set the custom Authenticator
                Authenticator = new JsonWebTokenAuthenticator(BaseUrl, new User()
                {
                    Username = "Brian_4",
                    Password = "passwordbj4"
                })
            };

            // Create the Client
            var client = new RestClient(options);

            // Create the Request
            var request = new RestRequest()
            {
                // Set the Get end-point url
                Resource = BaseUrl + "auth/webapi/all",
                // Set the Http Method
                Method = Method.Get,
            };

            // Send the GET request
            var response = client.Execute(request);

            // Validate the status code
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //Sending again to demonstrate caching of the token.
            response = client.Execute(request);

            // Validate the status code
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            // Release the resource acquired by the client
            client?.Dispose();


        }
    }
}
