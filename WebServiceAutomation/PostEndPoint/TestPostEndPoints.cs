using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebServiceAutomation.Helper.Authentication;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.ResponseData;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;

namespace WebServiceAutomation.PostEndPoint
{
    [TestClass]
    public class TestPostEndPoints
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";

        private string securePostUrl = "";
        private string secureGetUrl = "";


        private RestResponse restResponse;
        private RestResponse restResponseForGet;

        private string jsonMediatype = "application/json";
        private string xmlMediatype = "application/xml";
        private Random random = new Random();

        [TestMethod]
        public void TestPostEndpointWithJson()
        {
            //Method - POST -- use the PostAsync method of the HttpClient Class 
            //Body along with Request -- use HttpContent Class
            //Header - info about data format

            int id = random.Next(1000);
            
            string jsonData = "{" +
                                    "\"BrandName\": \"Alienware\", " +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\", " +
                                    "\"Windows 10 Home 64-bit English\", " +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\", " +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": +id \"," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";
            
            
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Accept", jsonMediatype);

                HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, jsonMediatype);
                Task<HttpResponseMessage> postResponse = httpClient.PostAsync(postUrl, httpContent);

                HttpResponseMessage response = postResponse.Result;
                Assert.IsNotNull(response);

                HttpStatusCode statusCode = response.StatusCode;
                Assert.AreEqual(HttpStatusCode.OK, statusCode);

                HttpContent content = response.Content;
                Assert.IsNotNull(content);
                string responseData = content.ReadAsStringAsync().Result;

                restResponse = new RestResponse((int)statusCode,responseData);

                Assert.AreEqual(200, restResponse.StatusCode);

                Assert.IsNotNull(restResponse.ResponseData, "Response Data is null/empty");

                Task<HttpResponseMessage> getResponse = httpClient.GetAsync(getUrl + id);

                restResponseForGet = new RestResponse((int)getResponse.Result.StatusCode,getResponse.Result.Content.ReadAsStringAsync().Result);

                JsonRootObjectBuilder jsonRootObject = JsonConvert.DeserializeObject<JsonRootObjectBuilder>(restResponseForGet.ResponseData);

             //   Assert.IsNotNull(jsonRootObject);
             //   Assert.AreEqual(id, jsonRootObject.Id);
             //   Assert.AreEqual("Alienware", jsonRootObject.BrandName);

            }
        }

        [TestMethod]
        public void TestPostWithXmlData()
        {
            int id = random.Next(1000);
            id = 1;
            string xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                        "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                        "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                        "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                        "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                    "</Features>" +
                                    "<Id>" +id + "</Id>" +
                                    "<LaptopName>Alienware M17</LaptopName>" +
                             "</Laptop>";

            
            using (HttpClient httpClient = new HttpClient()) 
            
            {
                httpClient.DefaultRequestHeaders.Add("Accept", xmlMediatype);

                HttpContent httpContent = new StringContent(xmlData, Encoding.UTF8, xmlMediatype);

                Task<HttpResponseMessage> httpResponseMessage = httpClient.PostAsync(postUrl, httpContent);

                restResponse = new RestResponse((int)httpResponseMessage.Result.StatusCode, httpResponseMessage.Result.Content.ReadAsStringAsync().Result);

                Assert.AreEqual(200, restResponse.StatusCode);

                Assert.IsNotNull(restResponse.ResponseData, "Respaonse Data is null/empty");

                Console.WriteLine(restResponse.ToString());

                httpResponseMessage = httpClient.GetAsync(getUrl + id);

                if (!httpResponseMessage.Result.IsSuccessStatusCode)
                {
                    Assert.Fail("The HTTP Response was not successful");
                }

                restResponse = new RestResponse((int)httpResponseMessage.Result.StatusCode, httpResponseMessage.Result.Content.ReadAsStringAsync().Result);

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Laptop));
                TextReader textReader = new StringReader(restResponse.ResponseData);
                Laptop xmlobj = (Laptop)xmlSerializer.Deserialize(textReader);

                Assert.IsTrue(xmlobj.Features.Feature.Contains("8GB, 2x4GB, DDR4, 2666MHz"), "Feature not present in the list for Laptop with Id of " +id);







            }
        }


        [TestMethod]
        public void TestPostEndpointUsingSendAsyncJson()
        {
            int id = random.Next(1000);
            id = 12;

            string jsonData = "{" +
                                    "\"BrandName\": \"Alienware\", " +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\", " +
                                    "\"Windows 10 Home 64-bit English\", " +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\", " +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " +id + "," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";

            using (HttpClient httpClient = new HttpClient())
            {
                
                using(HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.Method = HttpMethod.Post;
                    httpRequestMessage.RequestUri = new Uri(postUrl);
                    httpRequestMessage.Content = new StringContent(jsonData,Encoding.UTF8, jsonMediatype);

                    Task<HttpResponseMessage> httpResponseMessage = httpClient.SendAsync(httpRequestMessage);

                    restResponse = new RestResponse((int)httpResponseMessage.Result.StatusCode, httpResponseMessage.Result.Content.ReadAsStringAsync().Result);
                    Assert.AreEqual(200, restResponse.StatusCode);

                }
                


            }
        }

        [TestMethod]
        public void TestPostEndpointUsingSendAsyncXml()
        {
            int id = random.Next(1000);
            id = 1;
            string xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                        "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                        "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                        "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                        "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                    "</Features>" +
                                    "<Id>" + id + "</Id>" +
                                    "<LaptopName>Alienware M17</LaptopName>" +
                             "</Laptop>";

            using (HttpClient httpClient = new HttpClient())
            {

                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.Method = HttpMethod.Post;
                    httpRequestMessage.RequestUri = new Uri(postUrl);
                    httpRequestMessage.Content = new StringContent(xmlData, Encoding.UTF8, xmlMediatype);

                    Task<HttpResponseMessage> httpResponseMessage = httpClient.SendAsync(httpRequestMessage);

                    restResponse = new RestResponse((int)httpResponseMessage.Result.StatusCode, httpResponseMessage.Result.Content.ReadAsStringAsync().Result);
                    Assert.AreEqual(200, restResponse.StatusCode);

                }

            }
        }

        [TestMethod]
        public void PostUsingHelperMethod()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");

            string jsonData = "{" +
                                    "\"BrandName\": \"Alienware\", " +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\", " +
                                    "\"Windows 10 Home 64-bit English\", " +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\", " +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": 1," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";

            HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, jsonMediatype);

            RestResponse restResponse = HttpClientHelper.PerformPostRequest(postUrl, httpContent, httpHeader);

            List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseData);

            Console.WriteLine(jsonRootObject[0].ToString());

        }

        [TestMethod]
        public void TestPostUsingHelperClass() 
        {
            int id = random.Next(1000);
            
            string xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                        "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                        "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                        "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                        "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                    "</Features>" +
                                    "<Id>" + id + "</Id>" +
                                    "<LaptopName>Alienware M17</LaptopName>" +
                             "</Laptop>";

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept","application/xml" }
            };

            restResponse = HttpClientHelper.PerformPostRequest(postUrl, xmlData, xmlMediatype, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            //HttpContent httpContent = new StringContent(xmlData, Encoding.UTF8, xmlMediatype);
            //HttpClientHelper.PerformPostRequest(postUrlxml, httpContent, headers);

            //Laptop xmlResponseData = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseData);
            //Assert.IsNotNull(xmlResponseData);

            //Console.WriteLine(xmlResponseData);

            Laptop xmlDataResponse = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseData);
            Console.WriteLine(xmlDataResponse.ToString());




        }

        [TestMethod]
        public void TestSecurePostUsingHelperMethod() 
        
        {
            int id = random.Next(1000);

            string xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                        "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                        "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                        "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                        "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                    "</Features>" +
                                    "<Id>" + id + "</Id>" +
                                    "<LaptopName>Alienware M17</LaptopName>" +
                             "</Laptop>";

            string auth = Base64StringConverter.GetBase64String("admin", "welcome");
            auth = "Basic " + auth;

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept","application/xml" },
                {"Authorization", auth }
            };

            //POST Request
            restResponse = HttpClientHelper.PerformPostRequest(securePostUrl, xmlData, xmlMediatype, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            //POST Request - Alternative
            //HttpContent httpContent = new StringContent(xmlData, Encoding.UTF8, xmlMediatype);
            //HttpClientHelper.PerformPostRequest(postUrlxml, httpContent, headers);

            //GET Request
            restResponse = HttpClientHelper.PerformGetRequest(secureGetUrl +id, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            Laptop xmlDataResponse = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseData);
            Console.WriteLine(xmlDataResponse.ToString());
            Assert.AreEqual("Alienware M17", xmlDataResponse.LaptopName);

        }
    }



}
