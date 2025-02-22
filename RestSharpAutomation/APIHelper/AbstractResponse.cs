﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper
{
    public abstract class AbstractResponse : IResponse
    {
        private readonly RestResponse _restResponse;

        public AbstractResponse(RestResponse restResponse) //Constructor used for intializing the local RestResponse variable with the calling RestResponse variable.
        {
            _restResponse = restResponse;

        }
        public Exception GetException()
        {
            return _restResponse.ErrorException;
        }

        public HttpStatusCode GetHttpStatusCode()
        {
            return _restResponse.StatusCode;
        }

        public abstract string GetResponseData();
        
    }

    public abstract class AbstractResponse<T> : IResponse<T>
    {
        private readonly RestResponse<T> _restResponse;

        public AbstractResponse(RestResponse<T> restResponse)  //Constructor used for intializing the local RestResponse<T> variable with the calling RestResponse<T> variable.
        {
            _restResponse = restResponse;

        }
        public Exception GetException()
        {
            return _restResponse.ErrorException;
        }

        public HttpStatusCode GetHttpStatusCode()
        {
            return _restResponse.StatusCode;
        }

        public abstract T GetResponse();
    }
}
