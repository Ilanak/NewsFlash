using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFeedsTestClient.ServiceReference1;

namespace DataFeedsTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ServiceReference1.DataFeedsClient();
            var channel = client.ChannelFactory.CreateChannel();
            var feeds = channel.GetFeedsAsync("s1").Result;

            foreach (DataFeed feed in feeds)
            {
                Console.WriteLine(feed.Link);
            }
        }
    }
}
