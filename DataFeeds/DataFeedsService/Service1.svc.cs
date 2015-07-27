using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DataFeedsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataFeeds" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataFeeds.svc or DataFeeds.svc.cs at the Solution Explorer and start debugging.
    public class DataFeeds : IDataFeeds
    {
        private const int MaxResults = 50;
        private static IDataFeedApi[] dataFeeds = new IDataFeedApi[] {};

        public DataFeed[] GetFeeds(string topic)
        {
            if (string.IsNullOrEmpty(topic))
            {
                throw new ArgumentNullException("topic");
            }

            var results = new List<DataFeed>();
            while (results.Count < MaxResults)
            {

            }

            return results.ToArray();
        }
    }
}
