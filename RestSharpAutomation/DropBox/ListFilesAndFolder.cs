using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using RestSharpLatest.DropBox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.DropBox
{
    [TestClass]
    public class ListFilesAndFolder
    {
        public readonly string BaseUrl = "https://api.dropboxapi.com/2";
        public static string Token = "sl.CETbkPZuXqYbyiFz8WqN0E-Dy6EipY8_66F2kJjF5txnkluViHMeZGHOtPTCO3Jx-ZQBQKsOPALTKmR0OKIqMbC66Iqyu2XU14N0dOTVUJWKjoiDn-pxc9q-A367QB6T4uRem8rNa4mt";

        private static IClient client;
        private static IClient authClient;
        private static RestApiExecutor apiexecutor;
        private static RestClientOptions options;

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            var options = new RestClientOptions
            {

                ThrowOnAnyError = true
            };
            client = new TracerClient(options);
            authClient = new AuthenticationDecorator(client, new JwtAuthenticator(Token));
            apiexecutor = new RestApiExecutor();

            /* Example for using the HttpBasicAuthenticator for the client
             * authClient = new AuthenticationDecorator(client, new HttpAuthenticator("admin", "password"));
             */

        }


        [ClassCleanup]
        public static void TearDown()
        {
            authClient?.Dispose();
        }


        
        [TestMethod]
        public void GetAllFilesAndFolder()
        {
            var contextPath = "/files/list_folder";

            var requestBody = "{\"include_deleted\":false," +
                "\"include_has_explicit_shared_members\":false,\"include_media_info\":false," +
                "\"include_mounted_folders\":true,\"include_non_downloadable_files\":true," +
                "\"path\":\"\",\"recursive\":true}";


            var options = new RestClientOptions();
            options.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(Token);

            var client = new RestClient(options);

            var request = new RestRequest()
            {
                Resource = BaseUrl + contextPath,
                Method = Method.Post
            };

            request.AddStringBody(requestBody, DataFormat.Json);

            var response = client.ExecutePost<Root>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);


        }

        [TestMethod]
        public void GetAllFilesAndFolder_with_Framework()
        {
            // request body
            var contextPath = "/files/list_folder";

            var requestBody = "{\"include_deleted\":false," +
                "\"include_has_explicit_shared_members\":false,\"include_media_info\":false," +
                "\"include_mounted_folders\":true,\"include_non_downloadable_files\":true," +
                "\"path\":\"\",\"recursive\":true}";

            // Post request

            var postrequest = new PostRequestBuilder().WithUrl(BaseUrl + contextPath).WithBody(requestBody, PostRequestBuilder.RequestBodyType.STRING);

            // Request command 

            var command = new RequestCommand(postrequest, authClient);

            // set the command on api executor

            apiexecutor.SetCommand(command);

            // execute the request

            var response = apiexecutor.ExecuteRequest<Root>();

            // validate the response status
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);


        }

        [TestMethod]
        public void CreateFolder()
        {
            var contextPath = "/files/create_folder_v2";

            var requestBody = "{\"autorename\":true," +
                "\"path\":\"/TestFolder\"}";




            var options = new RestClientOptions();
            options.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(Token);

            var client = new RestClient(options);

            var request = new RestRequest()
            {
                Resource = BaseUrl + contextPath,
                Method = Method.Post
            };

            request.AddStringBody(requestBody, DataFormat.Json);

            var response = client.ExecutePost<Root>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);


        }
    }
}
