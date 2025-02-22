﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.SchemaValidation.Schema
{
    [TestClass]
    public class ValidateSchema
    {
        private Random random = new Random();

        [TestMethod]
        public void TestRequestBodyValidation()
        {
            int id = random.Next(1000);
            string jsonData = "{" +
                                    "\"BrandName\": \"Alienware\"," +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                                    "\"Windows 10 Home 64-bit English\"," +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " + id + "," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";

            string jsonDataInvalid = "{" +
                                    "\"BrandName\": \"Alienware\"," +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                                    "\"Windows 10 Home 64-bit English\"," +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " + id + "" +
                                "}";

            /*
             * Step 1 - Parse the given JSON schema
             * Step 2 - Parse the given JSON object
             * Step 3 - Call the 'isValid' for the validation
             * 
             */

            //string jsonSchema = File.ReadAllText(@"\Users\BrianJoslyn\source\repos\WebServiceAutomation\RestSharpAutomation\bin\Debug\Schema Validation\Schema\RequestBodySchema.txt");
            string jsonSchema = File.ReadAllText(@"SchemaValidation\Schema\RequestBodySchema.txt");

            JSchema jSchema = JSchema.Parse(jsonSchema);

            //JToken jtoken = JToken.Parse(jsonData);
            JToken jtoken = JToken.Parse(jsonDataInvalid);

            bool valid = jtoken.IsValid(jSchema);

            Console.WriteLine(valid);

            jtoken.IsValid(jSchema, out IList<ValidationError> errors);

            foreach (ValidationError err in errors)
            {
                Console.WriteLine(err.Message);
            }



        }
    }
}
