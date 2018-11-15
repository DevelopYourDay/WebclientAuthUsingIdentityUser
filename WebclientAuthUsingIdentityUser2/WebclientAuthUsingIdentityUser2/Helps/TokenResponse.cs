using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebclientAuthUsingIdentityUser.Helpers
{
    public class TokenResponse
    {
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("access_token")]
        public string access_token { get; set; }
        [JsonProperty("expiration")]
        public string expiration { get; set; }
        [JsonProperty("refresh_token")]
        public string refresh_token { get; set; }
    }
}