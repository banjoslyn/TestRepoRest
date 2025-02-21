using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp.Authenticators;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharpLatest.DropBox.Model;
using FluentAssertions;
using System.IO;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace RestSharpLatest.DropBox
{
    [TestClass]
    public class TestDownloadFile
    {

        public readonly string BaseUrl = "https://content.dropboxapi.com/2";
        public static string Token = "sl.u.AFfYsBWKZV30qEDRzOEPa6UuUFKPiTiIwypU_gsVghc13iJvAXfq48SdHW24VF7LjYlFNRbEHQwQEvWOtsbGUK7Ar6gEiDBICabMxCbqHFe__tGx7vNdnHxPbnkyqrVE3jdlrYfcwteUjoW3d_eyEppIkIgqeCYgCFoAFK2e8Ohgh1oX4gL-VvAgJm13IsmJQ2Zy7xmi8NTKitmLTz2hpkDxXqaEeD85nXikvwogzbIWsu6ObFAFaDlL1-R0a0iicrMySLekNyaVqu_WDdoDsIUOzXcnob3H6HnGO7yCHE-5iaUAxcDVu1wApl5C9rtrDy6cGMZzvrntX7t18Qa2YwxVWcfQBMEjEuClGGankVCVy5ehkDapFBKPNWy44NyQpNjhNuTY3DdznCsII-dEqgLvi1P4j03_QBOcScZU1tS4dsDDinywGBLUq5KrVA0PyoTM5W3QSWMDMoBg4NXNfHrgo-JION56vnJLyPM3DqhjxqeK0iro-TQHQVIiO4WotGado37BZQRoSOSaJ9nRKKnVZ6KPwWw8nRllAh6Yeb_vo4ulCJjYGnTrEV5Fqx14i1GFGhFBoAoFs_zkDsXO_gP29tnpXDvQ9x1qHlzncx0bJ-sh_Itbxw_INLbUoAIvXnqzQhlLwrXN4vvRCMgbpAhq3nirbjfItGGIaXExnkQbWQ-1DGycUu5Knh9hPzLlD3RRReq0NKfbb6DdqexLGFj0U9QI6K9wI9qn_JYYEGSAFlvhjpNJnf9arawbI9SLdWiBhZH86KGzTGNQXyUvGwpr9YEC1oVQCQNWPX-blPoF-fc3oiFeKOEC9xZvw--ZW3GBKzzwlhxKkj-NfBzNlOoIPYzeDZ5mA4iPLgM8EmYh2J2UH4KxSGlHYJmg6yVQdicsPuZOS_hH3jFJmgWuarGNjZNXxQQ7qmA77RXlEXegKSatPDkoqHdQhDcT8kzZgJCHBPCfb7oXIomcPz8cPm8KYtnuY3DfDDnIbPEpfHhzOGYXiw4bZtXzN9HSJQtjdB9lwQtQnc9HkmjKPGpexY7uJGoz72hNv-6O55RsvkgMsgWw7_PZu2NAYmdt2mrM6ELEdHH4rC1bpmxgjtsHoOzUayfHbgyjCjt6X0S5TpddYw0KIHzGgYV6KAueDxuosxXrSIeV0tiSlZMzIJdchL4RtgnYI20Olb8mxGXpctBwTyx7g4yzgX77Mrk8UhJRmmAo3XyOW_O9LXMvxVeq9GUTbCz_jjA4Njz8l9bIj7daNN3FQnA7TuNj_3biprF88bDfIqXeAg5w21oXwZqwlm_kuEQ_knhXUuGieOqtHtZkx7JbzkPONO86gA0ea-FvEm_pirKQTPR0_YK02c5CmNHMMtcfx0xy8PFcIRzpKexdD_pVC91PolRCMFWUbAotGLzx47UbCzY1pDJzg7QagJJZQE2hdBbdz71J-xxi_fQGJg";
        //Above Token is for the DropBox application MyClientBANJ

        private static IClient client;
        private static IClient authClient;
        private static RestApiExecutor apiexecutor;
        private static RestClientOptions options;

        

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            var options = new RestClientOptions
            {

                ThrowOnAnyError = true
            };

            client = new TracerClient(options);
            authClient = new AuthenticationDecorator(client, new JwtAuthenticator(Token));
            apiexecutor = new RestApiExecutor();

            /* Example for using the HttpBasicAuthenticator for the client
             * authClient = new AuthenticationDecorator(client, new HttpAuthenticator("admin", "password"));
             */

        }


        [ClassCleanup]
        public static void TearDown()
        {
            authClient?.Dispose();
        }

        [TestMethod]
        public void DownloadFile()
        {
            var contextPath = "/files/download";

            var fileName = "WIN_20240705_13_30_21_Pro.jpg";
            var location = "{\"path\":\"/" + fileName + "\"}";


            var options = new RestClientOptions();
            options.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(Token);

            var client = new RestClient(options);

            var request = new RestRequest()
            {
                Resource = BaseUrl + contextPath,
                Method = Method.Post
            };

            request.AddHeader("Dropbox-API-Arg", location);

            var data = client.DownloadData(request);    //DownloadData is the RestSharp RestClient method for downloading a file. 
            
            
            File.WriteAllBytes(fileName, data);
            client.Dispose();


        }

        [TestMethod]
        public void DownloadData_Parallel()
        {
            var contextPath = "/files/download";

            var fileNameOne = "WIN_20240705_13_30_21_Pro.jpg";
            var locationOne = "{\"path\":\"/" + fileNameOne + "\"}";

            var fileNameTwo = "RestSharp_API_Framework_Usage.png";
            var locationTwo = "{\"path\":\"/" + fileNameTwo + "\"}";


            var options = new RestClientOptions();
            options.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(Token);

            var client = new RestClient(options);



            var requestDownloadOne = new RestRequest()
            {
                Resource = BaseUrl + contextPath,
                Method = Method.Post
            };

            requestDownloadOne.AddHeader("Dropbox-API-Arg", locationOne);


            var requestDownloadTwo = new RestRequest()
            {
                Resource = BaseUrl + contextPath,
                Method = Method.Post
            };

            requestDownloadTwo.AddHeader("Dropbox-API-Arg", locationTwo);

            var task1 = client.DownloadDataAsync(requestDownloadOne); /* A separate thread, to the thread being used for the test itself, is created for this task.*/
            var task2 = client.DownloadDataAsync(requestDownloadTwo); /* Another separate thread, to the thread being used for the test itself and 
                                                                       * the thread of task1, is created for task2.*/

            Task.WaitAll(task1, task2); /* The main thread, being used for the test itself, will wait for the trheads used for task1 and task2 to finish running. */


            File.WriteAllBytes(fileNameOne, task1.Result);
            File.WriteAllBytes(fileNameTwo, task2.Result);

            File.Exists(fileNameOne).Should().BeTrue();
            File.Exists(fileNameTwo).Should().BeTrue();



        }

    }
}
