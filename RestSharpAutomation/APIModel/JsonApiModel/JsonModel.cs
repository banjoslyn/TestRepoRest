using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestSharpLatest.APIModel.JsonApiModel
{
    public class JsonModel
    {
        [JsonPropertyName("BrandName")]    //JsonPropertyName attribute is used to change the names of properties when they are serialized to JSON.
        public string BrandName { get; set; }

        [JsonPropertyName("Features")] //JsonPropertyName attribute is used to change the names of properties when they are serialized to JSON.
        public Features Features { get; set; }

        [JsonPropertyName("Id")] //JsonPropertyName attribute is used to change the names of properties when they are serialized to JSON.
        public int Id { get; set; }

        [JsonPropertyName("LaptopName")] //JsonPropertyName attribute is used to change the names of properties when they are serialized to JSON.
        public string LaptopName { get; set; }

    }
}
