using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper
{
    public abstract class AbstractRequest
    {
        public abstract RestRequest Build(); //Purpose of this method is to create an instance of RestRequest and return it to the caller.

        //Method for URL
        protected virtual void WithUrl(string url, RestRequest restRequest)
        {
            restRequest.Resource = url;
        }

        //Method for Header
        protected virtual void WithHeader(Dictionary<string, string> header, RestRequest restRequest) 
        //'protected' keyword means the method can be accessed only by inheritance.
        //'virtual' keyword allows the method to be overriden in a derivative class.
        {
            foreach (string key in header.Keys)

            {
                restRequest.AddOrUpdateHeader(key, header[key]);
            }

        }

        //Method for QueryParameter
        protected virtual void WithQueryParameters(Dictionary<string, string> parameters, RestRequest restRequest)
        //'protected' keyword means the method can be accessed only by inheritance.
        //'virtual' keyword allows the method to be overriden in a derivative class.
        {
            foreach (string key in parameters.Keys)

            {
                restRequest.AddParameter(key, parameters[key]);
            }

        }

        //Method for URL Segments

    }
}
