using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.FileUpload
{
    [TestClass]
    public class TestMultipartFormData
     {
        private readonly string BasePath = "http://localhost:9191";
        private static RestApiExecutor apiExecutor;
        private static IClient client;
        private static RestClientOptions options;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            var options = new RestClientOptions
            {

                ThrowOnAnyError = true
            };
            client = new TracerClient(options);
            apiExecutor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void Teardown()
        {
            client?.Dispose();
        }


        [TestMethod]
        public void File_Upload()
        {
            // Create client
            var client = new RestClient(BasePath);
            
            // Create request
            var fileUploadRequest = new RestRequest()
            {
                Resource = "/normal/webapi/upload",
                Method = Method.Post
            };

            // Read and store the file content in a byte array
            var fileContent = File.ReadAllBytes(@"C:\Users\BrianJoslyn\Downloads\TestData.xlsx");

            // Call the Add file api and pass the byte array
            fileUploadRequest.AddFile("file", fileContent, "Testdata.xlsx", "multipart/form-data");
            
            // Send the request
            var response = client.Execute(fileUploadRequest);
            
            // Verify the response status code.
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }

        [TestMethod]
        public void File_Path_Upload()
        {
            // Create client
            var client = new RestClient(BasePath);

            // Create request
            var fileUploadRequest = new RestRequest()
            {
                Resource = "/normal/webapi/upload",
                Method = Method.Post
            };

            // File
            string fileName = @"C:\Users\BrianJoslyn\Downloads\Testdata1.xlsx";

            // Call the Add file api and pass fileName
            fileUploadRequest.AddFile("file", fileName, "multipart/form-data");

            // Send the request
            var response = client.Execute(fileUploadRequest);

            // Verify the response status code.
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            // Release the resource acquired by the client.
            client.Dispose();

        }
        [TestMethod]
        public void File_Upload_Using_Framework()
        {
            var fileContent = File.ReadAllBytes(@"C:\Users\BrianJoslyn\Downloads\TestData.xlsx");

            // Create Post Request for a File Upload
            var fileUploadRequest = new PostRequestBuilder().WithUrl(BasePath + "/normal/webapi/upload").WithFileUpload("file", fileContent, "TestData.xlsx");

            // Set the Command for the Post Request
            var command = new RequestCommand(fileUploadRequest, client);
            apiExecutor.SetCommand(command);

            // Execute the Command
            var response = apiExecutor.ExecuteRequest();

            // Verify the Response Status Code
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);


        }



    }
}
