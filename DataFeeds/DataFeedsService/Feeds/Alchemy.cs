using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataFeedsService.Feeds
{
    public class Alchemy : IDataFeedApi
    {
        DataFeed[] results = new DataFeed[0];

        public async Task<DataFeed[]> GetFeedsAsync(string topic, int maxResults, DateTime startTime)
        {
            DataFeed item = new DataFeed();


            string start = "";
            string end = "";
            if (startTime == null)
            {
                startTime=new DateTime();
            }
            start = startTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).ToString();
            end = startTime.AddDays(-7).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).ToString();

            string ApiBaseUrl = "https://access.alchemyapi.com/calls/data/";
            string apiKey = "7adc0995828840783576e9d10754cc326542d34d";
            string returnValues = "enriched.url.title,enriched.url.url,enriched.url.publicationDate";
            string subject = "finance";
            string requestParameters =
                String.Format(
                    "/GetNews?apikey={0}&return={1}&start={2}&end={3}&q.enriched.url.taxonomy.taxonomy_.label={4}&count=25&outputMode=json",
                    apiKey,returnValues,start,end,subject);
            HttpResponseMessage response = await ApiHandler.GetResponseAsync(ApiBaseUrl, requestParameters);
  

            return response;
        }
    }
}