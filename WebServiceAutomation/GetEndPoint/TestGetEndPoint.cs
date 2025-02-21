using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebServiceAutomation.Helper.Authentication;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.ResponseData;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;
using static System.Net.Mime.MediaTypeNames;

namespace WebServiceAutomation.GetEndPoint
{
    
    [TestClass]
        
    public  class TestGetEndPoint
    {
        //private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
        private string getUrl = "https://23700400-76a8-4d3c-be0f-3531a0cc016f.mock.pstmn.io/getUsers6"; /* Postman Mock Application returning in application/json */
        //private string getUrl2 = "https://23700400-76a8-4d3c-be0f-3531a0cc016f.mock.pstmn.io/getUsers99"; /* Postman Mock Application returning in application/xml */
        private string secureGetUrl = ""; /* Secure GET URL */


        [TestMethod]
                
        public void TestGetAllEndPoint()
        {
            //Step 1. To create the Http Client
            HttpClient httpClient = new HttpClient();


            //Step 2 and 3. Create the Request and execute it.
            httpClient.GetAsync(getUrl);

            //Close the connection
            httpClient.Dispose();

        }

        [TestMethod]

        public void TestGetAllEndPointWithUri()
        {
            //Step 1. To create the Http Client
            HttpClient httpClient = new HttpClient();

            //Step 2 and 3. Create the Request and execute it.
            Uri getUri = new Uri(getUrl);  //Uri object created from Url
            
            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUri);
            HttpResponseMessage httpResponseMessage  = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());

            //Status
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Staus Code => " +statusCode);
            Console.WriteLine("Staus Code => " + (int)statusCode);

            //Content
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        
        }
        [TestMethod]

        public void TestGetAllEndPointWithInvalidUrl()
        {
            //Step 1. To create the Http Client
            HttpClient httpClient = new HttpClient();

            //Step 2 and 3. Create the Request and execute it.
            Uri getUri = new Uri(getUrl + "/random");  //Uri object created from Url

            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUri);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());

            //Status
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Staus Code => " + statusCode);
            Console.WriteLine("Staus Code => " + (int)statusCode);

            //Content
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();

        }
        [TestMethod]

        public void TestGetAllEndPointInJsonFormat()
        {
            //Step 1. To create the Http Client
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;
            requestHeaders.Add("Accept", "application/json");


            //Step 2 and 3. Create the Request and execute it.
            Uri getUri = new Uri(getUrl);  //Uri object created from Url

            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUri);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());

            //Status
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Staus Code => " + statusCode);
            Console.WriteLine("Staus Code => " + (int)statusCode);

            //Content
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();

        }

        [TestMethod]

        public void TestGetAllEndPointInXmlFormat()
        {
            //Step 1. To create the Http Client
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;
            requestHeaders.Add("Accept", "application/xml");


            //Step 2 and 3. Create the Request and execute it.
            Uri getUri = new Uri(getUrl);  //Uri object created from Url

            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUri);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());

            //Status
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Staus Code => " + statusCode);
            Console.WriteLine("Staus Code => " + (int)statusCode);

            //Content
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();

        }

        [TestMethod]

        public void TestGetAllEndPointInJsonFormatAcceptHeader()
        {
            MediaTypeWithQualityHeaderValue jsonHeader = new MediaTypeWithQualityHeaderValue("application/json");
            //Step 1. To create the Http Client
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;
            //requestHeaders.Add("Accept", "application/xml");
            requestHeaders.Accept.Add(jsonHeader);


            //Step 2 and 3. Create the Request and execute it.
            Uri getUri = new Uri(getUrl);  //Uri object created from Url

            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUri);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());

            //Status
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Staus Code => " + statusCode);
            Console.WriteLine("Staus Code => " + (int)statusCode);

            //Content
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();

        }
        [TestMethod]

        public void TestGetAllEndPointUsingSendAsync()
        { 
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.RequestUri = new Uri(getUrl);
            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.Headers.Add("Accept", "application/json");

            //Step 1. To create the Http Client
            HttpClient httpClient = new HttpClient();
            Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

            HttpResponseMessage httpResponseMessage = httpResponse.Result;

            //Status
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Staus Code => " + statusCode);
            Console.WriteLine("Staus Code => " + (int)statusCode);

            //Content
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();

        }
        [TestMethod]
        public void TestUsingMethod() 
        { 
        
            
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.RequestUri = new Uri(getUrl);
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/json");

                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {
                        Console.WriteLine(httpResponseMessage.ToString());

                        //Status
                        HttpStatusCode statusCode = httpResponseMessage.StatusCode;
                        //Console.WriteLine("Staus Code => " + statusCode);
                        //Console.WriteLine("Staus Code => " + (int)statusCode);

                        //Content
                        HttpContent responseContent = httpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        string data = responseData.Result;
                        //Console.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statusCode, responseData.Result);
                        Console.WriteLine(restResponse.ToString());
                    }

                }
            }
        }

        [TestMethod]
        public void TestDeserializationOfJsonResponse()
        {


            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.RequestUri = new Uri(getUrl);
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/json");

                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {
                        Console.WriteLine(httpResponseMessage.ToString());

                        //Status
                        HttpStatusCode statusCode = httpResponseMessage.StatusCode;
                        //Console.WriteLine("Staus Code => " + statusCode);
                        //Console.WriteLine("Staus Code => " + (int)statusCode);

                        //Content
                        HttpContent responseContent = httpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        string data = responseData.Result;
                        //Console.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statusCode, responseData.Result);
                        //Console.WriteLine(restResponse.ToString());

                        //List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseData);
                        List<JsonRootObjectBuilder> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObjectBuilder>>(data);
                        Console.WriteLine(jsonRootObject[0].ToString());
                        
                    }

                }
            }
        }

        [TestMethod]
        public void TestDeserializationOfXmlResponse()
        {


            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.RequestUri = new Uri(getUrl);
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/xml");

                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {
                        Console.WriteLine(httpResponseMessage.ToString());

                        //Status
                        HttpStatusCode statusCode = httpResponseMessage.StatusCode;
                        //Console.WriteLine("Staus Code => " + statusCode);
                        //Console.WriteLine("Staus Code => " + (int)statusCode);

                        //Content
                        HttpContent responseContent = httpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        string data = responseData.Result;
                        Console.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statusCode, responseData.Result);
                        Console.WriteLine(restResponse.ToString());

                        //Step 1
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(LaptopDetailss));

                        //Step 2
                        TextReader textReader = new StringReader(restResponse.ResponseData);

                        //Step 3
                        LaptopDetailss xmlData = (LaptopDetailss)xmlSerializer.Deserialize(textReader);
                        Console.WriteLine(xmlData.ToString());

                        //1st Assertion - Assert Status Code
                        Assert.AreEqual(200, restResponse.StatusCode);

                        //2nd Assertion - Assert Response Data
                        Assert.IsNotNull(restResponse.ResponseData);

                        //3rd Assertion 
                        Assert.IsTrue(xmlData.Laptop[0].Features.Feature.Contains("Windows 10 Home 64-bit English"), "Item NOT Found!");

                        //4th Assertion
                        Assert.AreEqual("Alienware", xmlData.Laptop[0].BrandName);



                    }

                }
            }
        }

        [TestMethod]
        public void GetUsingHelperMethod()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");

            RestResponse restResponse = HttpClientHelper.PerformGetRequest(getUrl, httpHeader);

           List<JsonRootObjectBuilder> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObjectBuilder>>(restResponse.ResponseData);

           Console.WriteLine(jsonRootObject[0].ToString());

            // List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJsonResponse<List<JsonRootObject>>(restResponse.ResponseData);
            //Assert.IsNotNull(jsonData);

            //Console.WriteLine(jsonData.ToString());

            List<JsonRootObjectBuilder> jsonData = ResponseDataHelper.DeserializeJsonResponse<List<JsonRootObjectBuilder>>(restResponse.ResponseData);
            Console.WriteLine(jsonData.ToString());




        }
        [TestMethod]
        public void TestSecureGetEndpoint()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");

            //httpHeader.Add("Authorization", "Basic YWRtaW46d2VsY29tZQ==");  //Header for Basic Authentication. https://www.base64decode.org

            string authHeader = Base64StringConverter.GetBase64String("admin", "welcome");
            authHeader = "Basic " + authHeader;
            httpHeader.Add("Authorization", authHeader);


            RestResponse restResponse = HttpClientHelper.PerformGetRequest(secureGetUrl, httpHeader);
            Assert.AreEqual(200, restResponse.StatusCode);

            //List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseData);
            //Console.WriteLine(jsonRootObject[0].ToString());

            
            List<JsonRootObjectBuilder> jsonData = ResponseDataHelper.DeserializeJsonResponse<List<JsonRootObjectBuilder>>(restResponse.ResponseData);
            Console.WriteLine(jsonData.ToString());

        }
    }

}
