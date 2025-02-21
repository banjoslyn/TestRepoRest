using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebServiceAutomation.Model.XmlModel;
using WebServiceAutomation.Model;

namespace WebServiceAutomation.Helper.ResponseData
{
    public class ResponseDataHelper
    {
        //Json - Helper method which will be static so the class name can be used to access this method
        //Xml -  Helper method which will be static so the class name can be used to access this method

        //Currently we are deserializing our Laptop model
        //In the future this model may change and be slightly different i.e. LaptopHybrid
        //Again this new model may itself change  i.e. LaptopHybrid2
        //Our Helper methods must be generic in order to cope with any model it is given to deserialize
        //We will be the concept of Generic Methods, these are methods written with a single method declaration
        //and can be called with arguments of different Types

        public static T DeserializeJsonResponse<T>(string responseData) where T : class 
        //T represents the Type this method will operate on, the 'where' clause enforces the Type to be a class
        //meaning whenever this method is called a single class Type must be passed. 
        { 
            return JsonConvert.DeserializeObject<T>(responseData);
        }

        /**
        The above method can be called from our code like: 
        ResponseDataHelper.DeserializeJsonResponse<Laptop>(responseData) where Laptop is the class model to deserialize.

        At runtime, the Compiler will treat the above code as:

        public static Laptop DeserializeJsonResponse<Laptop>(string responseData) where Laptop : class
        { 
            return JsonConvert.DeserializeObject<Laptop>(responseData);
        } 

        Similarly for ResponseDataHelper.DeserializeJsonResponse<LaptopHybrid>(responseData) where LaptopHybrid is the class model to deserialize.
        At runtime, the Compiler will treat the above code as:

        public static LaptopHybrid DeserializeJsonResponse<LaptopHybrid>(string responseData) where LaptopHybrid : class
        { 
            return JsonConvert.DeserializeObject<LaptopHybrid>(responseData);
        } 


        **/
        public static T DeserializeXmlResponse<T>(string responseData) where T : class
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StringReader(responseData);
            return (T)xmlSerializer.Deserialize(textReader);
            
        }

        /**
        The above method can be called from our code like: 
        ResponseDataHelper.DeserializeXmlResponse<Laptop>(responseData) where Laptop is the class model to deserialize.

        At runtime, the Compiler will treat the above code as:

        public static Laptop DeserializeXmlResponse<Laptop>(string responseData) where Laptop : class
        { 
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Laptop));
            TextReader textReader = new StringReader(responseData);
            return (Laptop)xmlSerializer.Deserialize(textReader);
        } 

        Similarly for ResponseDataHelper.DeserializeXmlResponse<LaptopHybrid>(responseData) where LaptopHybrid is the class model to deserialize.
        At runtime, the Compiler will treat the above code as:

        public static Laptop DeserializeXmlResponse<LaptopHybrid>(string responseData) where LaptopHybrid : class
        { 
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LaptopHybrid));
            TextReader textReader = new StringReader(responseData);
            return (LaptopHybrid)xmlSerializer.Deserialize(textReader);
        }

        **/

    }

}
