using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.APIResponse
{
    public class Response : AbstractResponse
    {
        private readonly RestResponse _restResponse;

        public Response(RestResponse restResponse) : base(restResponse)  //Using contructor chaining we can pass the value from the derived class to the parent class.
            //It is the responsibility of the derived class to provide the value for the variable that is being injected in the constructor of the superclass.
            //In this case 'base' refers to the AbstractResponse class.
        {
            _restResponse = restResponse;
        }

        public override string GetResponseData()
        {
            return _restResponse.Content;
        }
    }

    public class Response<T> : AbstractResponse<T>
    {
        private readonly RestResponse<T> _restResponse;
        public Response(RestResponse<T> restResponse) : base(restResponse)
        {
            _restResponse = restResponse;
        }
        public override T GetResponse()
        {
            return _restResponse.Data; //Use the Data property to get the de-serialized value of the Response body.
        }
    }
}
