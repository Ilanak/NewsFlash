using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DataFeedsService.Feeds
{
    public class Alchemy : IDataFeedSource
    {
        
        private readonly Dictionary<Topic, string> topicTranslator = new Dictionary<Topic, string>()
                {
                    {Topic.Business,"finance"},
                    {Topic.Fashion, "styl%20and%20fashion"},
                    {Topic.Technology," technology%20and%20computing"},
                    {Topic.Sports,"sports"},
                    {Topic.World,"news"},
                    {Topic.Food,"food%20and%20drink"} 
                };

        public async Task<DataFeed[]> GetFeedsAsync(Topic topic, int maxResults, DateTime startTime)
        {

            List<DataFeed> feeds = new List<DataFeed>();
            string start = "";
            string end = "";
            start = "now-" + Math.Max( (int) (DateTime.Now - startTime).TotalDays,1)+ "d";
            end = "now";

            string ApiBaseUrl = "https://access.alchemyapi.com/calls/data/";
            string apiKey = "7adc0995828840783576e9d10754cc326542d34d";
            string returnValues = "enriched.url.title,enriched.url.url,enriched.url.publicationDate";
            string subject = topicTranslator[topic];
            string requestParameters =
                String.Format(
                    "GetNews?apikey={0}&return={1}&start={2}&end={3}&q.enriched.url.taxonomy.taxonomy_.label={4}&count=25&outputMode=json",
                    apiKey,returnValues,start,end,subject);
            string response = await ApiHandler.GetResponseAsync(ApiBaseUrl, requestParameters);

            DataFeed[] feed = GenerateResponseFromString(response, maxResults);

            return feed;
        }

        private string getSource(string title)
        {
            char[] seperators = {'|', '-'};
            foreach (char c in seperators)
            {
                if (title.IndexOf(c) != -1)
                {
                    return title.Split(c)[title.Split(c).Length - 1];
                }
            }
            return "Alchemy";
        }
        private DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(Convert.ToDouble(unixTimeStamp)).ToLocalTime();
            return dtDateTime;
        }
        private DataFeed[] GenerateResponseFromString(string response,int maxResults)
        {

            List<DataFeed> feeds = new List<DataFeed>();
            JObject jsonResponse = JObject.Parse(response);
            if (jsonResponse["status"].ToString() == "ERROR") return null;
            JArray x = (JArray) jsonResponse["result"]["docs"];
            int resultsFeedNumber = x.Count;
            for (int i = 0; i < Math.Min(resultsFeedNumber, maxResults); i++)
            {
                var result = jsonResponse["result"]["docs"][i];
                var title = (string)result["source"]["enriched"]["url"]["title"];
                DataFeed feed = new DataFeed
                {
                    Link =(string)result["source"]["enriched"]["url"]["url"],
                    Title = title,
                    PublishTime = UnixTimeStampToDateTime((string)result["timestamp"]),
                    Source = getSource(title),
                    Image = null
                };
                feeds.Add(feed);
            }

            return feeds.ToArray();
        }
    }
}