using FluentAssertions;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharpLatest.JsonWebToken.Model;
using RestSharpLatest.SessionBasedAuth.JiraApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.Client
{
    public class JiraClient : IJiraClient
    {
        private RestClient _restClient;
        private readonly RestClientOptions _restClientOptions;

        public JiraClient(string baseUrl) //Constructor for the JiraClient, this is going to accept a string parameter which will be used for the base URL of the Jira application.
        {
            _restClientOptions = new RestClientOptions(baseUrl)
            {
                CookieContainer = new CookieContainer()
            };
            _restClient = new RestClient(_restClientOptions);
        }
        public void Dispose()
        {
            _restClient?.Dispose();
            _restClient = null;
        }

        public RestClient GetClient()
        {
            return _restClient;
        }

        public void Login(IJiraUser user)
        {
            // Send the post request to login to the Jira application
            //Note: The 'PostJson' API is used for sending the POST request. In this API we need to specify the type for the 
            //request payload 'IJiraUser' and the type for the response 'LoginResponse'. The first parameter to this method
            //is the context path of the login endpoint and the second is the request payload.
            var response = _restClient.PostJson<IJiraUser, LoginResponse>("rest/auth/1/session", user);

            // Add validation on the session information present in the response
            response.session.name.Should().NotBeNullOrEmpty();
            response.session.value.Should().NotBeNullOrEmpty();


            // Add the session information in the form of cookie to the client
            //Note: The 'AddCookie' API is used to add cookie information to the client, 'session name', 'session id', 'path' and 'domain' 
            //are required for the cookie and are present in the response.
            var cookie = new Cookie(response.session.name, response.session.value, response.session.path, response.session.domain);
            _restClientOptions.CookieContainer.Add(cookie);

        }

        public void Logout()
        {
            // Create a Rest Request
            var request = new RestRequest()
            {
                Resource = "/rest/auth/1/session"
            };

            // Send the delete request
            var response = _restClient.Delete(request);

            // Add the validation on the response status
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        }

        public class AdminJiraUser : IJiraUser
        {
            [JsonPropertyName("username")]
            public string username { get; set; }
            [JsonPropertyName("password")]
            public string password { get; set; }

            public AdminJiraUser(string username, string password) 
            {
                this.username = username;
                this.password = password;
            }
        }

    }
}
