using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DataFeedsService.Feeds
{
    public class ApiHandler
    {
        public static async Task<HttpResponseMessage> GetResponseAsync(string BaseUrl, string reqParams)
        {
            using (HttpClient client = new HttpClient())
            {
                // New code:
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(reqParams);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else
                {
                    Debug.WriteLine(response.ReasonPhrase);
                    return null;
                }
            }
        } 
    }
}