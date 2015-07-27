﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using DataFeedsService.faroo;

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
            var results = new List<DataFeed>();
            while (results.Count < MaxResults)
            {
                int currentNumberOfResults = results.Count;
                int maxResults = MaxResults - currentNumberOfResults;
                int maxResultsPerFeed = (maxResults / dataFeeds.Length) + 1;
                var feeds = (await Task.WhenAll(dataFeeds.Select(feed => feed.GetFeedsAsync(topic, maxResultsPerFeed, queryStartTime))))
                    .SelectMany(f => f)
                    .Where(f => !results.Any(r => r.Link.Equals(f.Link)))
                    .Take(maxResults);

                results.AddRange(feeds);
                if (results.Count == currentNumberOfResults)
                {
                    break;
                }
            }

            return results.ToArray();
        }
    }
}
