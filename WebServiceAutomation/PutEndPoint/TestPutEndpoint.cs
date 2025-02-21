using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Helper.Authentication;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.ResponseData;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;

namespace WebServiceAutomation.PutEndPoint
{
    [TestClass]
    public class TestPutEndpoint
    {
        //Post create a record
        //Put to update the record
        //Get using the Id to verify the update

        private string postUrlxml = "https://fbe8b02e-ba55-46c3-9675-80b9483937be.mock.pstmn.io/laptop-bag/webapi/api/add"; /* Postman Mock for application/xml */
        private string postUrljson = ""; /* Postman Mock for application/json */
        private string getUrlxml = "https://fbe8b02e-ba55-46c3-9675-80b9483937be.mock.pstmn.io/laptop-bag/webapi/api/find808"; /* Postman Mock Application returning in application/xml */
        private string getUrljson = "https://fbe8b02e-ba55-46c3-9675-80b9483937be.mock.pstmn.io/laptop-bag/webapi/api/find909"; /* Postman Mock Application returning in application/json */
        private string putUrlxml = "https://8e15e728-a75a-4c93-96f7-d5289aac4448.mock.pstmn.io/web/api/update";
        private string putUrljson = ""; /* Postman Mock for application/json */
        private string putUrl = "http://localhost:8080/laptop-bag/webapi/api/update";

        private string securePostUrl = "";
        private string secureGetUrl = "";
        private string securePutUrl = "";


        private RestResponse restResponse;
        private RestResponse restResponseForGet;

        private string jsonMediatype = "application/json";
        private string xmlMediatype = "application/xml";
        private Random random = new Random();


        [TestMethod]

        public void TestPutEndpointXml()
        {
            int id = random.Next(1000); //Used for the POST URL
            id = 1;

            int id2 = random.Next(1000); //Used for the PUT URL
            id2 = 808;

            string xmlData = "<Laptop>" +
                                 "<BrandName>Alienware</BrandName>" +
                                 "<Features>" +
                                     "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                     "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                     "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                     "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>"+
                                 "</Features>" +
                                 "<Id>" + id + "</Id>" +
                                 "<LaptopName>Alienware M17</LaptopName>" +
                          "</Laptop>";

            string xmlPutData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                        "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                        "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                        "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                        "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                        "<Feature>1 TB of SSD</Feature>" +
                                    "</Features>" +
                                    "<Id>" + id2 + "</Id>" +
                                    "<LaptopName>Alienware M17</LaptopName>" +
                             "</Laptop>";


            //Header for POST Request
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };

            //POST Request
            restResponse = HttpClientHelper.PerformPostRequest(postUrlxml, xmlData, xmlMediatype, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            //PUT Request
            using (HttpClient httpClient = new HttpClient()) 
            { 
                HttpContent httpContent = new StringContent(xmlPutData, Encoding.UTF8, xmlMediatype);

                Task<HttpResponseMessage> httpResponseMessage = httpClient.PutAsync(putUrlxml, httpContent);

                restResponse = new RestResponse((int)httpResponseMessage.Result.StatusCode,httpResponseMessage.Result.Content.ReadAsStringAsync().Result);

                Assert.AreEqual(200, restResponse.StatusCode);

            }

            //GET Request
            restResponse = HttpClientHelper.PerformGetRequest(getUrlxml, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            Laptop xmlObj = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseData);
            Assert.IsNotNull(xmlObj);
            Assert.IsTrue(xmlObj.Features.Feature.Contains("1 TB of SSD"), "Item Not found");

            

        }

        [TestMethod]
        public void TestPutUsingJsonData()
        {
            int id = random.Next(1000); //Used for the POST URL
            id = 1;

            int id2 = random.Next(1000); //Used for the PUT URL
            id2 = 909;

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
                                    "\"Id\": " + id + "," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";

            string jsonPutData =  "{" +
                                    "\"BrandName\": \"Alienware\", " +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\", " +
                                    "\"Windows 10 Home 64-bit English\", " +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\", " +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "\"10TB Of SSD Storage\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " + id + "," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";


            //Header for POST Request
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/json" }
            };

            /*

            //POST Request
            restResponse = HttpClientHelper.PerformPostRequest(postUrljson, jsonData, jsonMediatype, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            //PUT Request
            using (HttpClient httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(jsonPutData, Encoding.UTF8, jsonMediatype);

                Task<HttpResponseMessage> httpResponseMessage = httpClient.PutAsync(putUrljson, httpContent);

                restResponse = new RestResponse((int)httpResponseMessage.Result.StatusCode, httpResponseMessage.Result.Content.ReadAsStringAsync().Result);

                Assert.AreEqual(200, restResponse.StatusCode);

            }
            */

            //GET Request
            restResponse = HttpClientHelper.PerformGetRequest(getUrljson, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            JsonRootObjectBuilder jsonObj = ResponseDataHelper.DeserializeJsonResponse<JsonRootObjectBuilder>(restResponse.ResponseData);
            Assert.IsNotNull(jsonObj);
            //Assert.IsTrue(jsonObj.Features.Feature.Contains("10TB Of SSD Storage"), "Item Not found");

        }

        [TestMethod] 
        public void TestPutWithHelperClass_Xml()
        {
            int id = random.Next(1000); //Used for the POST URL
            id = 1;

            int id2 = random.Next(1000); //Used for the PUT URL
            id2 = 808;

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

            string xmlPutData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                        "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                        "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                        "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                        "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                        "<Feature>1 TB of SSD</Feature>" +
                                    "</Features>" +
                                    "<Id>" + id2 + "</Id>" +
                                    "<LaptopName>Alienware M17</LaptopName>" +
                             "</Laptop>";


            //Header for POST Request
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };

            //POST Request
            restResponse = HttpClientHelper.PerformPostRequest(postUrlxml, xmlData, xmlMediatype, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            //PUT Request
            restResponse = HttpClientHelper.PerformPutRequest(putUrlxml, xmlPutData, xmlMediatype, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            //GET Request
            restResponse = HttpClientHelper.PerformGetRequest(getUrlxml, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            Laptop xmlObj = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseData);
            Assert.IsNotNull(xmlObj);
            Assert.IsTrue(xmlObj.Features.Feature.Contains("1 TB of SSD"), "Item Not found");
        }

        [TestMethod]
        public void TestPutWithHelperClass_Json()
        {
            int id = random.Next(1000); 
            id = 909;

            string jsonPutData = "{" +
                                    "\"BrandName\": \"Alienware\", " +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\", " +
                                    "\"Windows 10 Home 64-bit English\", " +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\", " +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "\"10TB Of SSD Storage\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " + id + "," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";


            //Header for Requests
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/json" }
            };

            /*

            //PUT Request
            RestResponse restResponse = HttpClientHelper.PerformPutRequest(postUrljson, jsonPutData, jsonMediatype, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            */
            

            //GET Request
            restResponse = HttpClientHelper.PerformGetRequest(getUrljson, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            JsonRootObjectBuilder jsonObj = ResponseDataHelper.DeserializeJsonResponse<JsonRootObjectBuilder>(restResponse.ResponseData);
            Assert.IsNotNull(jsonObj);
            //Assert.IsTrue(jsonObj.Features.Feature.Contains("10TB Of SSD Storage"), "Item Not found");
        }

        [TestMethod]
        public void TestSecurePutWithHelpereClass_Xml() 
        {
            int id = random.Next(1000); //Used for the POST URL
            id = 1;

            int id2 = random.Next(1000); //Used for the PUT URL
            id2 = 808;

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

            string xmlPutData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                        "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                        "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                        "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                        "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                        "<Feature>1 TB of SSD</Feature>" +
                                    "</Features>" +
                                    "<Id>" + id2 + "</Id>" +
                                    "<LaptopName>Alienware M17</LaptopName>" +
                             "</Laptop>";


            string auth = Base64StringConverter.GetBase64String("admin", "welcome");
            auth = "Basic " +auth;
            
            //Header for POST Request
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" },
                {"Authorization", auth }
            };

            //POST Request
            restResponse = HttpClientHelper.PerformPostRequest(securePostUrl, xmlData, xmlMediatype, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            //PUT Request
            restResponse = HttpClientHelper.PerformPutRequest(securePutUrl, xmlPutData, xmlMediatype, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            //GET Request
            restResponse = HttpClientHelper.PerformGetRequest(secureGetUrl, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            Laptop xmlObj = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseData);
            Assert.IsNotNull(xmlObj);
            Assert.IsTrue(xmlObj.Features.Feature.Contains("1 TB of SSD"), "Item Not found");

        }







    }
}
