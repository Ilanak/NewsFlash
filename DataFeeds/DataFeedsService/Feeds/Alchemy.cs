using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataFeedsService.Feeds
{
    public class Alchemy : IDataFeedApi
    {
        DataFeed[] results = new DataFeed[0];

        public Task<DataFeed[]> GetFeedsAsync(string topic, int maxResults, DateTime time)
        {
            DataFeed item = new DataFeed();
            string ApiBaseUrl = "http://api.feedzilla.com/";
            string requestParameters = "/v1/categories/26/subcategories/1303/articles.json";
            Task<HttpResponseMessage> response = ApiHandler.GetResponseAsync(ApiBaseUrl, requestParameters);



            return response;
        }
    }
}