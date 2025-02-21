using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace RestSharpLatest.GetRequest
{
    [TestClass]
    public class TestApiHelperGet
    {

        private IClient _client;
        private readonly string getUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/api/all";

        //"https://23700400-76a8-4d3c-be0f-3531a0cc016f.mock.pstmn.io/getLaptopMulti3"; -- Postman Mock URL

        private RestApiExecutor executor;

        public void Setup() 
        { 
            _client = new DefaultClient();
            executor = new RestApiExecutor();
        }

        [TestMethod]
        public void GetRequestWithApiHelper() 
        {
            var headers = new Dictionary<string, string>()
            {
                {"Accept", "application/json" }
            };
            AbstractRequest abstractRequest = new GetRequestBuilder().WithUrl(getUrl).WithHeader(headers);

            ICommand getCommand = new RequestCommand(abstractRequest, _client);

            executor.SetCommand(getCommand);
            var response = executor.ExecuteRequest();

            Assert.IsNotNull(response);

            //Fluent Assertion
            response.GetHttpStatusCode().Should().Be(HttpStatusCode.OK);

        }

        public void TearDown()
        {

            _client.Dispose(); 
        
        }

    }
}
