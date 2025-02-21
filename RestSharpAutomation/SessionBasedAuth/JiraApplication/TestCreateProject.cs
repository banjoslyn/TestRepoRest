using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using RestSharpLatest.SessionBasedAuth.JiraApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RestSharpLatest.APIHelper.APIRequest.PostRequestBuilder;
using static RestSharpLatest.APIHelper.Client.JiraClient;

namespace RestSharpLatest.SessionBasedAuth.JiraApplication
{
    [TestClass]
    public class TestCreateProject
    {
        private static IJiraClient _jiraClient;
        private static string BaseUrl = "http://localhost:9091";
        private static RestApiExecutor _apiExecutor;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _jiraClient = new JiraClient(BaseUrl);
            _apiExecutor = new RestApiExecutor();
            _jiraClient.Login(new AdminJiraUser("brian", "admin@1234#"));
        }

        [ClassCleanup]
        public static void TearDown() 
        {
            _jiraClient.Logout();
            _jiraClient?.Dispose();
        }

        [TestMethod]
        public void VerifyCreateProject() 
        {
            // Create the request body using the model class

            var requestBody = new CreateProjectPayload();

            // Create the post request using PostRequestBuilder class

            var postRequest = new PostRequestBuilder().WithUrl("rest/api/2/project").WithBody(requestBody, RequestBodyType.JSON);

            // Create the Request Command and set the command on the Api executor

            var command = new RequestCommand(postRequest, _jiraClient);
            _apiExecutor.SetCommand(command);


            // Execute the request & validate the response status code

            var response = _apiExecutor.ExecuteRequest();

            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);


            // Extract the project key from the response

            var responseObject = JObject.Parse(response.GetResponseData());
            var projectKey = (string)responseObject.SelectToken("$.key");

            // Create the get  request using GetRequestBuilder class

            var getRequest = new GetRequestBuilder().WithUrl($"/rest/api/2/project/{projectKey}");

            // Create the Request Command and set the command on the Api executor
            command = new RequestCommand(getRequest, _jiraClient);
            _apiExecutor.SetCommand(command);

            // Execute the request & validate the response status code
            response = _apiExecutor.ExecuteRequest();

            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);


        }
    }
}
