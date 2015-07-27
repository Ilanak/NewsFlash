using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DataFeedsService
{
    [ServiceContract]
    public interface IDataFeeds
    {
        [OperationContract]
        DataFeed[] GetFeeds(string topic);
    }

    [DataContract]
    public class DataFeed
    {
        [DataMember]
        public Url Link { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string[] Keywords { get; set; }

        [DataMember]
        public DateTime PublishTime { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public Url Image { get; set; }
    }
}
