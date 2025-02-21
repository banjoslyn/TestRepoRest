using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.Command
{
    public class DownloadRequestCommand : ICommand
    {
        private readonly IClient _client;
        private readonly AbstractRequest _abstractRequest;

        public DownloadRequestCommand(AbstractRequest abstractRequest, IClient client)
        {
            _abstractRequest = abstractRequest;
            _client = client;
        }

        public byte[] DownloadData()
        {
            // Get the client
            var client = _client.GetClient();

            // Build the request
            var request = _abstractRequest.Build();

            // Call the download api on the client
            var data = client.DownloadData(request);
            return data;

        }

        public Task<byte[]> DownloadDataAsync()
        {
            // Get the client
            var client = _client.GetClient();

            // Build the request
            var request = _abstractRequest.Build();

            // Call the download api on the client
            return client.DownloadDataAsync(request);

        }

        public IResponse ExecuteRequest()
        {
            throw new NotImplementedException("Use Request Command for executing the Request");
        }

        public IResponse<T> ExecuteRequest<T>()
        {
            throw new NotImplementedException("Use Request Command for executing the Request");
        }
    }
}
