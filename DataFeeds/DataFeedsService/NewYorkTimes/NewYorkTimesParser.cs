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
            "svc/news/v3/content/all/{0}/{1}.json?limit={2}&api-key=d3535b3bef9c3a82b2f63f763111b679:8:72573066";

        private Dictionary<string, string> topicTranslator = new Dictionary<string, string>()
        {
            {"s1","arts"}
        };

        public async Task<DataFeed[]> GetFeedsAsync(string topic, int maxResults, DateTime queryStartTime)
        {
            //check maxResult and period
            
            List<DataFeed> feeds = new List<DataFeed>();
            string nytTopic;
            
            try
            {
                nytTopic = topicTranslator[topic];
            }
            catch (Exception e)
            {
                return null;
            }

            int periodInHours = (int) Math.Floor((DateTime.Now - queryStartTime).TotalHours);
            string url = string.Format(urlTemplate, nytTopic, periodInHours, maxResults);

            string response = (await ApiHandler.GetResponseAsync(domain,url));
            if (response == null)
            {
                return null;
            }

            //string content = httpResponse.Content.ContentToString();
            //httpResponse.GetResponseStream();
                //Stream receiveStream = response.GetResponseStream();
            //StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            //txtBlock.Text = readStream.ReadToEnd();
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