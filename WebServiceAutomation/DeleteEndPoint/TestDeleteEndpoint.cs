using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Helper.Authentication;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.ResponseData;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.XmlModel;

namespace WebServiceAutomation.DeleteEndPoint
{
    [TestClass]
    public class TestDeleteEndpoint
    {

        private string postUrl = "https://fbe8b02e-ba55-46c3-9675-80b9483937be.mock.pstmn.io/laptop-bag/webapi/api/add"; /* Postman Mock for application/xml */
        private string deleteUrl = "http://localhost:8080/laptop-bag/webapi/api/delete";
        private string secureDeleteUrl = "";


        private RestResponse restResponse;

        private string xmlMediatype = "application/xml";
        private Random random = new Random();

        [TestMethod]

        public void TestDelete()
        {
            /*
             * Use POST Request to add a new record
             * Use DELETE Request to delete the newly created record, Expect 200 OK Status
             * Use DELETE Request to delete the same recprd again, Expect 404 Not Found Status
             */

            int id = random.Next(1000);
            id = 1; //Fake id as we are using a mock service for POST Request.

            AddRecord(id);

            using(HttpClient httpclient = new HttpClient())
            {
                Task<HttpResponseMessage> httpResponseMessage = httpclient.DeleteAsync(deleteUrl +id);

                HttpStatusCode httpStatusCode = httpResponseMessage.Result.StatusCode;
                Assert.AreEqual(HttpStatusCode.OK, httpStatusCode);

                httpResponseMessage = httpclient.DeleteAsync(deleteUrl + id);
                httpStatusCode = httpResponseMessage.Result.StatusCode;
                Assert.AreEqual(404, (int)httpStatusCode);


            }



        }

        public void AddRecord(int id)
        {

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

            Laptop xmlObj = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseData);
            Assert.IsNotNull(xmlObj);


        }

        [TestMethod]
        public void TestDeleteUsingHelperClass() 
        {
            int id = random.Next(1000);
            id = 1; //Fake id as we are using a mock service for POST Request.

            AddRecord(id);

            restResponse = HttpClientHelper.PerformDeleteRequest(deleteUrl + id);
            Assert.AreEqual(200, restResponse.StatusCode);

            restResponse = HttpClientHelper.PerformDeleteRequest(deleteUrl + id);
            Assert.AreEqual(404, restResponse.StatusCode);

        }

        [TestMethod]
        public void TestSecureDeleteUsingHelperClass() 
        {
            int id = random.Next(1000);
            id = 1; //Fake id as we are using a mock service for POST Request.

            string auth = Base64StringConverter.GetBase64String("admin", "welcome");
            auth = "Basic " + auth;

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Authorization", auth }
            };

            AddRecord(id);

            restResponse = HttpClientHelper.PerformDeleteRequest(secureDeleteUrl + id, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            restResponse = HttpClientHelper.PerformDeleteRequest(secureDeleteUrl + id, headers);
            Assert.AreEqual(404, restResponse.StatusCode);


        }
    }
}
