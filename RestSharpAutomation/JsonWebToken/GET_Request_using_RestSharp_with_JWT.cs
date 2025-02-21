using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpLatest.JsonWebToken.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.JsonWebToken
{
    public class GET_Request_using_RestSharp_with_JWT
    {
        [TestClass]
        public class Assisgnment_Get_with_JWT
        {
            private static readonly string BaseUrl = "http://localhost:9191/";
            // Use the JSON Web Token Authenticator with RestSharp Client
            [TestMethod]
            public void Secure_Get_With_Jwt_RestClient()
            {
                // Client Options
                var options = new RestClientOptions
                {
                    // Set the custom Authenticator
                    Authenticator = new JsonWebTokenAuthenticator(BaseUrl, new User()
                    {
                        Id = 2,
                        Username = "Brian_1",
                        Password = "passwordbj1"
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

                // Release the resource acquired by the client
                client?.Dispose();

            }


        }
    }
}
