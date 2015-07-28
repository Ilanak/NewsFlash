using System;
using System.ServiceModel;
using DataFeedsService;

namespace DataFeedsTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IDataFeeds> factory = new ChannelFactory<IDataFeeds>("DataFeeds");
            var proxy = factory.CreateChannel();
            var feeds = proxy.GetFeedsAsync(Topic.Sports).Result;
            ((IDisposable)proxy).Dispose();


            foreach (DataFeed feed in feeds)
            {
                Console.WriteLine(feed.Link);
            }
        }
    }
}
