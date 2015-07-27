using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFeedsService
{
    interface IDataFeedApi
    {
        DataFeed[] GetFeeds(string topic, int maxResults);
    }
}
