using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using static System.Net.WebRequestMethods;
using System.Net;
using FluentAssertions;
using WebServiceAutomation.Model.XmlModel;
using WebServiceAutomation.Model.JsonModel;

namespace RestSharpLatest.GetRequest
{
    [TestClass]
    public class TestGetRequest
    {
        private readonly string getUrl = "https://laptopbag.onrender.com/laptop-bag/webapi/api/all";
        //"https://23700400-76a8-4d3c-be0f-3531a0cc016f.mock.pstmn.io/getLaptopMulti3"; //Mock URL for returning 2 laptop items in Json format
        // "https://23700400-76a8-4d3c-be0f-3531a0cc016f.mock.pstmn.io/getUsers6" - Mock URL for returning a single laptop item in Json format

        //  - The Real McCoy!

        private readonly string getUrlXml = "https://23700400-76a8-4d3c-be0f-3531a0cc016f.mock.pstmn.io/getLaptopMultiXml";
        // "https://23700400-76a8-4d3c-be0f-3531a0cc016f.mock.pstmn.io/getUsers99"; - Mock URL for returning a single laptop item in Xml format

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GetRequest()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);

            RestResponse response = client.ExecuteGet(getRequest);

            // Debug Class - System.Diagnotics - Class used to write debug messages.


            // Status Code
            Debug.WriteLine($"Response Status Code - {response.StatusCode}");

            // Error Message
            Debug.WriteLine($"Error Message - {response.ErrorMessage}");

            // Exception
            Debug.WriteLine($"Exception - {response.ErrorException}");
        }
        [TestMethod]
        public void GetRequestPrintResponseContent()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);
            RestResponse response = client.ExecuteGet(getRequest);

            if (response.IsSuccessful)
            {
                // Status Code
                Debug.WriteLine($"Response Status Code - {response.StatusCode}");

                // Content
                Debug.WriteLine($"Response Status Code - {response.Content}");
            }
            else

            {
                // Error Message
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");

                // Exception
                Debug.WriteLine($"Exception - {response.ErrorException}");

            }



        }

        [TestMethod]
        public void GetRequestPrintResponseContentInXml()
        {

            /* Expected XML format of Response.
             * <?xml version="1.0" encoding="UTF-8" standalone="yes"?> 
                <laptopDetailss>
                    <Laptop>
                        <BrandName>Alienware</BrandName>
                        <Features> 
                                    <Feature>8th Generation Intel® Core™ i5-8300H</Feature>
                                    <Feature>Windows 10 Home 64-bit English</Feature>
                                    <Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>
                                    <Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>
                        </Features>
                        <Id>1</Id>
                        <LaptopName>Alienware M17</LaptopName>
                    </Laptop>
                </laptopDetailss>  */

            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrlXml);

            //Add header
            //getRequest.AddHeader("Accept", "application/xml"); - NOT RECOMMENDED
            //The Accept header is set by RestSharp automatically based on registered serializers.
            //By default, both XML and JSON are supported. Only change the Accept header if you need something else like binary streams, or plain text.

            //Similarly, getRequest.AddHeader("Content-Type", "application/json") is not recommended.
            //The Content-Type header is the content header, not the request header. It might be different per individual part of the body when using
            //multipart-form data, for example. RestSharp automatically sets the correct content-type header automatically, based on your body format,
            //so don't override it.

            client.AcceptedContentTypes = new string[] { "application/xml" };  //Recommendation is to use the Client to set this.


            RestResponse response = client.ExecuteGet(getRequest);

            if (response.IsSuccessful)
            {
                // Status Code
                Debug.WriteLine($"Response Status Code - {response.StatusCode}");

                // Content
                Debug.WriteLine($"Response Status Code - {response.Content}");
            }
            else

            {
                // Error Message
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");

                // Exception
                Debug.WriteLine($"Exception - {response.ErrorException}");

            }

        }

        [TestMethod]
        public void GetRequestPrintResponseContentInJson()
        {
       
            
            /* Expected format of the Json Response
             * [ 
                    {
                        "BrandName": "Alienware",
                        "Features": { 
                            "Feature": [ 
                                            "8th Generation Intel® Core™ i5-8300H",
                                            "Windows 10 Home 64-bit English",
                                            "NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6",
                                            "8GB, 2x4GB, DDR4, 2666MHz"
                                       ]
                    },
                    "Id": 1,
                    "LaptopName": "Alienware M17"
                    }
                    ]
            */


            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);

            //Add header
            //getRequest.AddHeader("Accept", "application/xml"); - NOT RECOMMENDED
            //The Accept header is set by RestSharp automatically based on registered serializers.
            //By default, both XML and JSON are supported. Only change the Accept header if you need something else like binary streams, or plain text.

            //Similarly, getRequest.AddHeader("Content-Type", "application/json") is not recommended.
            //The Content-Type header is the content header, not the request header. It might be different per individual part of the body when using
            //multipart-form data, for example. RestSharp automatically sets the correct content-type header automatically, based on your body format,
            //so don't override it.

            client.AcceptedContentTypes = new string[] { "application/json" };  //Recommendation is to use the Client to set this.
            //application/json is the default and does not really need to be specified.


            RestResponse response = client.ExecuteGet(getRequest);

            if (response.IsSuccessful)
            {
                // Status Code
                Debug.WriteLine($"Response Status Code - {response.StatusCode}");

                // Content
                Debug.WriteLine($"Response Status Code - {response.Content}");
            }
            else

            {
                // Error Message
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");

                // Exception
                Debug.WriteLine($"Exception - {response.ErrorException}");

            }

        }

        [TestMethod]
        public void GetRequestWithJsonAndDeserialize()
        {


            /* Expected format of the Json Response
             * [ 
                    {
                        "BrandName": "Alienware",
                        "Features": { 
                            "Feature": [ 
                                            "8th Generation Intel® Core™ i5-8300H",
                                            "Windows 10 Home 64-bit English",
                                            "NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6",
                                            "8GB, 2x4GB, DDR4, 2666MHz"
                                       ]
                    },
                    "Id": 1,
                    "LaptopName": "Alienware M17"
                    }
                    ]
            */


            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);
            //RestResponse response = client.ExecuteGet(getRequest);

            RestResponse<List<JsonRootObject>> response = client.ExecuteGet<List<JsonRootObject>>(getRequest);
            //RestResponse<List<JsonModelBuilder>> response = client.ExecuteGet<List<JsonModelBuilder>>(getRequest);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response.StatusCode.Should().Be(HttpStatusCode.OK); //Uses FluentAssertion package which is much simpler to use than out of box Assert class.

            

            if (response.IsSuccessful)
            {
                // Status Code
                Debug.WriteLine($"Response Status Code - {response.StatusCode}");

                // Content
                Debug.WriteLine($"Response Content Size - {response.Data.Count}");

                response.Data.ForEach((item) => {
                    Debug.WriteLine($"Response ID is - {item.Id}");
                    
                 });

                JsonRootObject jsonRootObject = response.Data.Find((item) =>  //The Find() method is being used with a lamda filter criteria
                                                                              //to return a laptop object with an Id of 1.
                //JsonModelBuilder jsonRootObject = response.Data.Find((item) =>  //The Find() method is being used with a lamda filter criteria
                                                                                  //to return a laptop object with an Id of 1.
                {
                    return item.Id == 1;
                }) ;

                Assert.AreEqual("Alienware",jsonRootObject.BrandName);

                jsonRootObject.BrandName.Should().NotBeEmpty();          //Uses FluentAssertion package which is much simpler to use than out of box Assert class.
                jsonRootObject.BrandName.Should().Be("Alienware");      //Uses FluentAssertion package which is much simpler to use than out of box Assert class.

                Assert.IsTrue(jsonRootObject.Features.Feature.Contains("Windows 10 Home 64-bit English"), "Element not found");

                jsonRootObject.Features.Feature.Should().NotBeEmpty(); //Uses FluentAssertion package which is much simpler to use than out of box Assert class.
                jsonRootObject.Features.Feature.Should().Contain("Windows 10 Home 64-bit English"); //Uses FluentAssertion package which is much simpler to use than out of box Assert class.

            }
            else

            {
                // Error Message
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");

                // Exception
                Debug.WriteLine($"Exception - {response.ErrorException}");

            }

        }

        [TestMethod]
        public void GetRequestWithXmlAndDeserialize()
        {

            /* Expected XML format of Response.
             * <?xml version="1.0" encoding="UTF-8" standalone="yes"?> 
                <laptopDetailss>
                    <Laptop>
                        <BrandName>Alienware</BrandName>
                        <Features> 
                                    <Feature>8th Generation Intel® Core™ i5-8300H</Feature>
                                    <Feature>Windows 10 Home 64-bit English</Feature>
                                    <Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>
                                    <Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>
                        </Features>
                        <Id>1</Id>
                        <LaptopName>Alienware M17</LaptopName>
                    </Laptop>
                </laptopDetailss>  */

            RestClient client = new RestClient();
            //RestRequest getRequest = new RestRequest(getUrlXml); //Mock server URL

            RestRequest getRequest = new RestRequest(getUrl);

            client.AcceptedContentTypes = new string[] { "application/xml" };  //Recommendation is to use the Client to set this.


            RestResponse<LaptopDetailss> response = client.ExecuteGet<LaptopDetailss>(getRequest);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            if (response.IsSuccessful)
            {
                // Status Code
                Debug.WriteLine($"Response Status Code - {response.StatusCode}");

                // Content
                Debug.WriteLine($"Response Content Size - {response.Data}");

                response.Data.Laptop.ForEach((item) => {
                    Debug.WriteLine($"Response ID is - {item.Id}");
                });

                Laptop rootObject = response.Data.Laptop.Find((item) =>  //Lamda used as filter criteria for Find() method on the response
                                                                         //data for the a laptop with an Id of 1.
                {
                    return "1".Equals(item.Id, System.StringComparison.OrdinalIgnoreCase);
                });

                Assert.AreEqual("Alienware", rootObject.BrandName);

                rootObject.BrandName.Should().NotBeEmpty();          //Uses FluentAssertion package which is much simpler to use than out of box Assert class.
                rootObject.BrandName.Should().Be("Alienware");      //Uses FluentAssertion package which is much simpler to use than out of box Assert class.

                Assert.IsTrue(rootObject.Features.Feature.Contains("Windows 10 Home 64-bit English"), "Element not found");

                rootObject.Features.Feature.Should().NotBeEmpty(); //Uses FluentAssertion package which is much simpler to use than out of box Assert class.
                rootObject.Features.Feature.Should().Contain("Windows 10 Home 64-bit English"); //Uses FluentAssertion package which is much simpler to use than out of box Assert class.

            }
            else

            {
                // Error Message
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");

                // Exception
                Debug.WriteLine($"Exception - {response.ErrorException}");

            }

        }

        [TestMethod]
        public void SendRequestWithExecuteApi()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest()
            {
                Method = Method.Get,
                Resource = getUrl
            };

            var response = client.Execute<List<JsonRootObject>>(getRequest);
            var content = response.Data;

            var jsonObject = content.Find((item) =>
            {
                return 1 == item.Id;
            });

            jsonObject.BrandName.Should().NotBeNullOrEmpty();
            jsonObject.Id.Should().Be(1);

            jsonObject.Features.Feature.Should().Contain("Windows 10 Home 64-bit English");




        }

        [TestMethod]
        public void SendRequestInXmlWithExecuteApi()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest()
            {
                Method = Method.Get,
                Resource = getUrlXml
            };

            var response = client.Execute<LaptopDetailss>(getRequest);
            // Validate the status code.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Extract the List of objects.
            var content = response.Data.Laptop;

            var xmlObject = content.Find((item) =>
            {
                return "1".Equals(item.Id, StringComparison.OrdinalIgnoreCase);
            });

            // Validate the BrandName property.
            xmlObject.BrandName.Should().NotBeNull();

            // Validate the LaptopName property.
            xmlObject.LaptopName.Should().NotBeNull();


        }



    }
}
