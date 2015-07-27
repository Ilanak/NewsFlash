using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Schema;
using DataFeedsService.Feeds;
using Newtonsoft.Json.Linq;

namespace DataFeedsService.faroo
{
    public class NewYorkTimesParser : IDataFeedApi
    {

        private const string domain = "http://api.nytimes.com/"; 
        private const string urlTemplate =
            "svc/news/v3/content/all/{0}/{1}.json?limit={2}&api-key=d3535b3bef9c3a82b2f63f763111b679:8:72573066";

        private Dictionary<string, string> topicTranslator = new Dictionary<string, string>()
        {
            {"s1","arts"}
        };

        public async Task<DataFeed[]> GetFeedsAsync(string topic, int maxResults, DateTime queryStartTime)
        {
            if (!topicTranslator.ContainsKey(topic) || maxResults < 0 || queryStartTime >= DateTime.Now)
            {
                return null;
            }

            int periodInHours = (int) Math.Ceiling((DateTime.Now - queryStartTime).TotalHours);
            string url = string.Format(urlTemplate, topicTranslator[topic], periodInHours, maxResults);

            string response = (await ApiHandler.GetResponseAsync(domain,url));
            if (response == null)
            {
                return null;
            }

            JObject jsonResponse;
            int resultsFeedNumber;
            try
            {
                jsonResponse = JObject.Parse(response);
                resultsFeedNumber = (int) jsonResponse["num_results"];
            }
            catch (Exception e)
            {
                return null;
            }

            return GetDataFeedsFromResponse(jsonResponse, resultsFeedNumber,maxResults);
        }

        private DataFeed[] GetDataFeedsFromResponse(JObject jsonResponse, int resultsFeedNumber, int maxResults)
        {
            List<DataFeed> feeds = new List<DataFeed>();

            for (int i = 0; i < Math.Min(resultsFeedNumber, maxResults); i++)
            {
                DataFeed feed;

                try
                {
                    var result = jsonResponse["results"][i];

                    feed = new DataFeed
                    {
                        Link = new Url((string) result["url"]),
                        Title = (string) result["title"],
                        PublishTime = DateTime.Parse((string) result["created_date"]),
                        Source = (string) result["source"],
                        Text = (string) result["abstract"]
                        //Image = new Url((string) result[""])
                    };
                }
                catch(Exception e)
                {
                    continue;
                }
                feeds.Add(feed);
            }

            return feeds.ToArray();
        }

    }
}