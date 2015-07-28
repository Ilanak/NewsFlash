using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using DataFeedsService.Feeds;
using Newtonsoft.Json.Linq;

namespace DataFeedsService.NewYorkTimes
{
    public class NewYorkTimesParser : IDataFeedApi
    {

        private const string domain = "http://api.nytimes.com/"; 
        private const string urlTemplate =
            "svc/news/v3/content/all/{0}/{1}.json?limit={2}&api-key=d3535b3bef9c3a82b2f63f763111b679:8:72573066";

        private readonly Dictionary<Topic, string> topicTranslator = new Dictionary<Topic, string>()
        {
            {Topic.Business,"business"},
            {Topic.Fashion, "fashion%20&%20style"},
            {Topic.Technology,"technology"},
            {Topic.Sports,"sports"},
            {Topic.WorldNews,"world"}
        };

        public async Task<DataFeed[]> GetFeedsAsync(Topic topic, int maxResults, DateTime queryStartTime)
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

                    JArray multimedaArr = (JArray) result["multimedia"];
                    feed = new DataFeed
                    {
                        Link = new Url((string) result["url"]),
                        Title = (string) result["title"],
                        PublishTime = DateTime.Parse((string) result["created_date"]),
                        Source = (string) result["source"],
                        Text = (string) result["abstract"],
                        Image = GetUrlOfLargestImage(multimedaArr)
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

        private Url GetUrlOfLargestImage(JArray multimedaArr)
        {
            string largestUrl = null;
            int largestSize = 0;
            if (multimedaArr == null || multimedaArr.Count == 0)
            {
                return null;
            }

            for (int i = 0; i < multimedaArr.Count; i++)
            {
                if ((string) multimedaArr[i]["type"] != "image")
                {
                    continue;
                }

                int currentSize = (int) multimedaArr[i]["height"] * (int) multimedaArr[i]["width"];
                if (currentSize > largestSize)
                {
                    largestSize = currentSize;
                    largestUrl = (string)multimedaArr[i]["url"];
                }
            }

            if (largestUrl == null)
            {
                return null;
            }

            return new Url(largestUrl);
        }
    }
}