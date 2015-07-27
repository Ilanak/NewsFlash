using System;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.ServiceModel;

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
        public string Text { get; set; }

        [DataMember]
        public string[] Concepts { get; set; }
        
        [DataMember]
        public DateTime PublishTime { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public Url Image { get; set; }
    }
}
