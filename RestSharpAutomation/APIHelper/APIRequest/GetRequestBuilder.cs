using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.APIRequest
{
    public class GetRequestBuilder : AbstractRequest
    {
        private readonly RestRequest _restRequest; //This RestRequest type variable will be passed to the AbstractRequest Class methods.

        public GetRequestBuilder()  //Constructor (same name as class) used for initializing the RestRequest type variable '_restRequest'
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Get
            };

        }

        public override RestRequest Build()
        {
            return _restRequest; 
        }

        //Method for URL
        public GetRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);  //Note: this 'WithUrl' is coming from the base class, AbstractRequest. Hover over to see.
            return this;
        }

        //Method for Request Headers
        public GetRequestBuilder WithHeader(Dictionary<string, string> headers)
        {
            WithHeader(headers, _restRequest); //Note: this 'WithHeader' is coming from the base class, AbstractRequest. Hover over to see.
            return this;
        }

        //QueryParameter
        public GetRequestBuilder WithQueryParameters(Dictionary<string, string> parameters)
        {
            WithQueryParameters(parameters, _restRequest);
            return this;
        }



    }
}
