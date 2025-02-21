using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace RestSharpLatest.SessionBasedAuth.JiraApplication.Model
{
    public class LoginResponse
    {
        [JsonPropertyName("session")]
        public Session session { get; set; }
        [JsonPropertyName("loginInfo")]
        public LoginInfo loginInfo { get; set; }

    }
}
