using System;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace DataFeedsService
{
    [ServiceContract]
    public interface IDataFeeds
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeeds?Topic={topic}")]
        Task<DataFeed[]> GetFeedsAsync(Topic topic);
    }

    [DataContract]
    public enum Topic
    {
        [EnumMember]
        Business,
        [EnumMember]
        Fashion,
        [EnumMember]
        Technology,
        [EnumMember]
        Sports,
        [EnumMember]
        WorldNews
    }


    [DataContract]
    public class DataFeed
    {
        [DataMember]
        public string Link { get; set; }

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
        public string Image { get; set; }
    }
}
