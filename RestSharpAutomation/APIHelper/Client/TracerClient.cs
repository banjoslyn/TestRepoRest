﻿using HttpTracer;
using RestSharp;
using RestSharpLatest.APIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.Client
{
    public class TracerClient : IClient
    {
        private readonly RestClientOptions _restClientOptions;
        private RestClient _restClient;

        public TracerClient(RestClientOptions options)
        {
            _restClientOptions = options;

        }
        
        public void Dispose()
        {
            _restClient?.Dispose();
        }

        public RestClient GetClient()
        {
            //_restClientOptions.ConfigureMessageHandler = TraceConfig;
            /* 
            _restClientOptions.ConfigureMessageHandler = (handler) =>
            {
              var tracer = new HttpTracerHandler(handler, HttpMessageParts.All);
              return tracer;
            };*/

            _restClientOptions.ConfigureMessageHandler = (handler) =>
            {
                return new HttpTracerHandler(handler, HttpMessageParts.All);
            };

            _restClientOptions.ThrowOnDeserializationError = true;
            _restClient = new RestClient(_restClientOptions);

            return _restClient;

        }

        private HttpMessageHandler TraceConfig(HttpMessageHandler handler)
        {
            var tracer = new HttpTracerHandler(handler, HttpMessageParts.All);
            return tracer;
        
        }
    
    }
}
