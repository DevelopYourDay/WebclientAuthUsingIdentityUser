using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebclientAuthUsingIdentityUser.Helpers
{
    public static class WebApiHttpClient
    {
        public const string WebApiBaseAddress = "http://webapicoreidentity.azurewebsites.net/";
        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebApiBaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var session = HttpContext.Current.Session;
            if (session["token"] != null)
            {
                TokenResponse tokenResponse = getToken();
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", tokenResponse.access_token);
            }
            return client;
        }

        public static void storeToken(TokenResponse token)
        {
            var session = HttpContext.Current.Session;
            session["acessToken"] = token.access_token;
            session["refreshToken"] = token.refresh_token;
        }

        public static TokenResponse getToken()
        {
            var session = HttpContext.Current.Session;
            return (TokenResponse)session["acessToken"];
        }

        public static TokenResponse getRefreshToke()
        {
            var session = HttpContext.Current.Session;
            return (TokenResponse)session["refreshToken"];
        }
    }
}