using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
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
            "svc/news/v3/content/all/{0}/{1}.xml?limit={2}&api-key=d3535b3bef9c3a82b2f63f763111b679:8:72573066";

        private Dictionary<string, string> topicTranslator = new Dictionary<string, string>()
        {
            {"s1","t1"}
        };

        public async Task<DataFeed[]> GetFeedsAsync(string topic, int maxResults, DateTime queryStartTime)
        {
            List<DataFeed> feeds = new List<DataFeed>(); 
            string nytTopic = topicTranslator[topic];
            int periodInHours = (DateTime.Now - queryStartTime).Hours;
            string url = string.Format(urlTemplate, nytTopic, periodInHours, maxResults);

            string response = (await ApiHandler.GetResponseAsync(domain,url)).Content.ToString();
            JObject jsonResponse = JObject.Parse(response);

            int resultsFeedNumber = (int) jsonResponse["num_results"];
            for (int i = 0; i < Math.Min(resultsFeedNumber, maxResults); i++)
            {
                var result = jsonResponse["result"][i];
                //result["multimedia"].Children()


                DataFeed feed = new DataFeed
                {
                    Link = new Url((string) result["url"]),
                    Title = (string) result["title"],
                    PublishTime = DateTime.Parse((string) result["published_date"]),
                    Source = (string) result["source"],
                    Image = new Url((string) result[""])
                };
                feeds.Add(feed);
            }

            return feeds.ToArray();
        }
    }
}