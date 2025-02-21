using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.Client
{
    [TestClass]
    public class BasicAuthDecorator : IClient
    {
        private readonly IClient _client;
        public BasicAuthDecorator(IClient client)
        {
            _client = client;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public RestClient GetClient()
        {
            var options = new RestClientOptions();
            options.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("admin", "Welcome");




            //Creates a new Client based on the Client Implementation (Default/Tracer) passed to the Decorator.

            //1. Invoke _client.GetClient() API 
            var newClient = _client.GetClient();

            //2. Add the auth configuration
            //    Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("admin", "Welcome");

            //newClient.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("admin", "Welcome");


            //3. return the new client
            return newClient;




        }
    }
}
