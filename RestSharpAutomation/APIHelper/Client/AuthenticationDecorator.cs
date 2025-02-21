using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.Client
{
    public class AuthenticationDecorator : IClient
    {
        private readonly IClient _client;
        private readonly AuthenticatorBase _authenticator;
        public AuthenticationDecorator(IClient client, AuthenticatorBase authenticator)
        {
            _client = client;
            _authenticator = authenticator;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public RestClient GetClient()
        {
            // Invoke _client.GetClient() API to get the existing client
            var existingClient = _client.GetClient();

            // Create new RestClientOptions and set the Authenticator
            var options = new RestClientOptions
            {
                Authenticator = _authenticator,
                // Copy any setting from the existing client to the new client, if needed
                /*
                 * BaseUrl (Uri): The base URL for requests made by the client.
                    MaxTimeout (int): The maximum timeout value for requests.
                    Timeout (int): The timeout value for requests.
                    FollowRedirects (bool): Whether the client should follow HTTP redirects.
                    Proxy (IWebProxy): The proxy information for the client.
                    RemoteCertificateValidationCallback (RemoteCertificateValidationCallback): A callback function for custom certificate validation.
                    Authenticator (IAuthenticator): The authenticator used to authenticate requests.
                    CookieContainer (CookieContainer): The container for handling cookies.
                    UserAgent (string): The user agent string sent with requests.
                    MaxRedirects (int): The maximum number of redirects to follow.
                    ClientCertificates (X509CertificateCollection): The client certificates used for HTTPS requests.
                    Pipelined (bool): Indicates whether to use pipelined HTTP.
                    AutomaticDecompression (DecompressionMethods): Indicates whether to automatically decompress response content.
                    ThrowOnAnyError (bool): Indicates whether to throw an exception on any HTTP error response.
                 */
                BaseUrl = existingClient.Options.BaseUrl,
                UserAgent = existingClient.Options.UserAgent,
            };

            // Create a new RestClient with the options
            var newClient = new RestClient(options);

            // Return the new client which should be the same as the Client passed in with the Authenticator added to it.
            return newClient;




        }
    }
}
