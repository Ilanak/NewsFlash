using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Schema;

namespace DataFeedsService.faroo
{
    public class NewYorkTimesParser : IDataFeedApi
    {
        
        private const string urlTemplate =
            "http://api.nytimes.com/svc/news/v3/content/all/{0}/{1}.xml?limit={2}&api-key=d3535b3bef9c3a82b2f63f763111b679:8:72573066";

        private Dictionary<string, string> topicTranslator = new Dictionary<string, string>()
        {
            {"s1","t1"}
        };

        public Task<DataFeed[]> GetFeedsAsync(string topic, int maxResults, DateTime queryStartTime)
        {
            string nytTopic = topicTranslator[topic];
            int periodInHours = (DateTime.Now - queryStartTime).Hours;
            string url = string.Format(urlTemplate, nytTopic, periodInHours, maxResults);   


        }
    }
}