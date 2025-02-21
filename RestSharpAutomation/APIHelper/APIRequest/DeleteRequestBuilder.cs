using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.APIRequest
{
    public class DeleteRequestBuilder : AbstractRequest
    {
        private readonly RestRequest _restRequest; //This RestRequest type variable will be passed to the AbstractRequest Class methods.

        public DeleteRequestBuilder()  //Constructor (same name as class) used for initializing the RestRequest type variable '_restRequest'
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Delete
            };

        }

        public override RestRequest Build()
        {
            return _restRequest; 
        }

        //Method for URL
        public DeleteRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);  //Note: this 'WithUrl' is coming from the base class, AbstractRequest. Hover over to see.
            return this;
        }

        //Method for Request Headers
        public DeleteRequestBuilder WithDefaultHeader()
        {
            WithHeader(null, _restRequest); //Note: this 'WithHeader' is coming from the base class, AbstractRequest. Hover over to see.
            return this;
        }

        protected override void WithHeader(Dictionary<string, string> header, RestRequest restRequest)
        {
            restRequest.AddOrUpdateHeader("Accept","text/plain");
        }

        //QueryParameter
        public DeleteRequestBuilder WithQueryParameters(Dictionary<string, string> parameters) 
        {
            WithQueryParameters(parameters, _restRequest);
            return this;
        }



    }
}
