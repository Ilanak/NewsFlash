using System;
using System.Net.Http;

namespace DataFeedsService.Feeds
{
    public class Alchemy : IDataFeedApi
    {
        DataFeed[] results = new DataFeed[0];

        public DataFeed[] GetFeeds(string topic, int maxResults, DateTime time)
        {
            DataFeed item = new DataFeed();
            string ApiBaseUrl = "http://api.feedzilla.com/";
            string requestParameters = "/v1/categories/26/subcategories/1303/articles.json";
            //HttpResponseMessage response = ApiHandler.GetResponseAsync(ApiBaseUrl, requestParameters);



            return results;
        }
    }
}