using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using DataFeedsService.faroo;
using OpenTextSummarizer;

namespace DataFeedsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataFeeds" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataFeeds.svc or DataFeeds.svc.cs at the Solution Explorer and start debugging.
    public class DataFeeds : IDataFeeds
    {
        private const int QueryPeriodInHours = 24;
        private const int MaxResults = 50;
        private readonly static IDataFeedApi[] dataFeeds = 
        {
            new NewYorkTimesParser(), 
        };

        public async Task<DataFeed[]> GetFeedsAsync(string topic)
        {
            if (string.IsNullOrEmpty(topic))
            {
                throw new ArgumentNullException("topic");
            }

            DateTime queryStartTime = DateTime.UtcNow - TimeSpan.FromHours(QueryPeriodInHours);
            DataFeed[][] allFeeds = await Task.WhenAll(dataFeeds.Select(feed => feed.GetFeedsAsync(topic, MaxResults, queryStartTime)));
            IEnumerable<IGrouping<Url, DataFeed>> groupedFeeds =
                allFeeds
                    .SelectMany(feed => feed)
                    .GroupBy(feed => feed.Link);

            return groupedFeeds
                .Select(feedGroup => feedGroup.First())
                .Select(EnrichWithConcepts)
                .ToArray();
        }

        private DataFeed EnrichWithConcepts(DataFeed feed)
        {
            try
            {
                var args = new SummarizerArguments
                {
                    DictionaryLanguage = "en",
                    DisplayLines = 1,
                    InputString = feed.Title
                };

                var summary = Summarizer.Summarize(args);
                feed.Concepts = summary.Concepts.ToArray();
            }
            catch (Exception e)
            {
                
            }


            return feed;
        }
    }
}
