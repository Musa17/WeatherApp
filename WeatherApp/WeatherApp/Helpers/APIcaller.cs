using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Helpers
{
    public class APIcaller
    {
        public static async Task<APIresponse> Get(string url, string authID = null)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(authID))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", authID);

                var request = await client.GetAsync(url);

                if (request.IsSuccessStatusCode)
                {
                    return new APIresponse { Response = await request.Content.ReadAsStringAsync() };
                }

                else
                {
                    return new APIresponse { ErrorMessage = request.ReasonPhrase };
                }
            }
        }
    }

    public class APIresponse
    {
        public bool Successful => ErrorMessage == null;
        public string ErrorMessage { get; set; }
        public string Response { get; set; }
    }
}
