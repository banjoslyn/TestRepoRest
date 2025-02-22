﻿using RestSharp;
using RestSharpLatest.APIHelper.APIResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.Command
{
    public class RequestCommand : ICommand
    {
        private readonly IClient _client;
        private readonly AbstractRequest _abstractRequest;

        public RequestCommand(AbstractRequest abstractRequest, IClient client)
        {
            _abstractRequest = abstractRequest;
            _client = client;
        }

        public byte[] DownloadData()
        {
            throw new NotImplementedException("Use the Download Command for File Download");
        }

        public Task<byte[]> DownloadDataAsync()
        {
            throw new NotImplementedException("Use the DownloadDataAsync Command for Parallel File Download");
        }

        public IResponse ExecuteRequest()
        {
            var client = _client.GetClient();
            var request = _abstractRequest.Build();
            var response = client.Execute(request);
            return new Response(response);
        }

        public IResponse<T> ExecuteRequest<T>()
        {
            var client = _client.GetClient();
            var request = _abstractRequest.Build();
            var response = client.Execute<T>(request);
            return new Response<T>(response);
        }
    }
}
