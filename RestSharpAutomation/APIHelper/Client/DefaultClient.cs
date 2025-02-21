using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.Client
{
    public class DefaultClient : IClient //This class will implement the interface IClient
    {
        private RestClient _client; 
        private readonly RestClientOptions _restClientOptions; //A RestSharp class with methods and properties to provide additional configuration to the client.

        public DefaultClient()  //This is the Constructor for the class.
        {
            _restClientOptions = new RestClientOptions(); //Initialising the _resClientOptions variable.
        }

        public void Dispose()
        {
            _client?.Dispose(); //Perform a null check then invoke the Dispose method coming from the IDisposable Interface.
        }

        public RestClient GetClient()
        {
            _restClientOptions.ThrowOnDeserializationError = true; //By setting this property to true, if there is a problem the RestSharp framework an error will be thrown
                                                                   //and execution stopped.
            _client = new RestClient(_restClientOptions); 
            return _client;
        }
    }
}
