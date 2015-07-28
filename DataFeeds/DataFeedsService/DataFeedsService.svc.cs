using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Bing;
using DataFeedsService.NewYorkTimes;
using OpenTextSummarizer;

namespace DataFeedsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataFeeds" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataFeeds.svc or DataFeeds.svc.cs at the Solution Explorer and start debugging.
    public class DataFeeds : IDataFeeds
    {
        private const int QueryPeriodInHours = 24;
        private const int MaxResults = 50;
        private readonly static IDataFeedSource[] DataFeedSources = 
        {
            new NewYorkTimesParser(),
            //new Alchemy(), 
        };

        public async Task<DataFeed[]> GetFeedsAsync(Topic topic)
        {
            DateTime queryStartTime = DateTime.UtcNow - TimeSpan.FromHours(QueryPeriodInHours);
            DataFeed[][] allFeeds = await Task.WhenAll(DataFeedSources.Select(feed => feed.GetFeedsAsync(topic, MaxResults, queryStartTime)));
            IEnumerable<IGrouping<string, DataFeed>> groupedFeeds =
                allFeeds
                    .Where(feed => feed != null)
                    .SelectMany(feed => feed)
                    .Where(feed => feed != null)
                    .GroupBy(feed => feed.Link);

            IEnumerable<DataFeed> results = groupedFeeds
                .Select(feedGroup => feedGroup.First())
                .Select(NormalizeFeed)
                .Select(EnrichWithConcepts);

            results = Task.WhenAll(results.Select(EnrichWithImagesAsync)).Result;
            return results.ToArray();
        }

        private DataFeed NormalizeFeed(DataFeed feed)
        {
            feed.Title = HttpUtility.HtmlDecode(feed.Title);
            feed.Text = HttpUtility.HtmlDecode(feed.Text);
            return feed;
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
            catch (Exception)
            {
            }
            
            return feed;
        }

        private async Task<DataFeed> EnrichWithImagesAsync(DataFeed feed)
        {
            const string key = "ZvD71NLFp4EFiActP24HNCNHey1r4m64bwMQPTh8AZg";

            if (!string.IsNullOrEmpty(feed.Image))
            {
                return feed;
            }

            if (feed.Concepts == null)
            {
                return null;
            }

            var bingContainer = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/Search/v1/"))
            {
                Credentials = new NetworkCredential(key, key)
            };

            var imagesQuery = bingContainer.Image(string.Join(" ", feed.Concepts), null, null, "Moderate", null, null, "Size:Large");
            IEnumerable<ImageResult> imagesResults = await 
                Task<IEnumerable<ImageResult>>.Factory.FromAsync(imagesQuery.BeginExecute, imagesQuery.EndExecute, null);

            ImageResult image = imagesResults.FirstOrDefault();
            if (image != null)
            {
                feed.Image = image.MediaUrl;
                return feed;
            }

            return null;
        }
    }
}
