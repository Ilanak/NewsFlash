using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Data.Mask;
using DevExpress.XtraPrinting.Native;
using RestSharp;
using Newtonsoft;
using Newtonsoft.Json;

namespace NewsFlashUI
{
    public partial class Main : System.Web.UI.Page
    {
        private Dictionary<string, List<DataFeed>> conceptsDic= new Dictionary<string, List<DataFeed>>(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                // DataFeedsService.svc/GetFeeds?Topic=s1
                var client = new RestClient("http://datafeeds.cloudapp.net/");

                var request = new RestRequest("DataFeedsService.svc/GetFeeds", Method.GET);
                request.AddParameter("Topic", "Business");

                var result = client.Execute(request);

            
               var feeds = JsonConvert.DeserializeObject<DataFeed[]>(result.Content);

                /*feeds.ForEach(f =>
            {
                foreach (var VARIABLE in concepts)
                {
                    
                }
                conceptsDic.ContainsKey(f.)
            });*/


                foreach (var feed in feeds)
                {

                    string[] words = feed.Title.Split(' ');
                    //string filePath = feed.Image.ToString();
                    for (int j = 0; j < words.Length; j++)
                    {
                        //ASPxImageSlider1.Items.Add(filePath, string.Empty, feed.Link.ToString(), "Business", words[j]);

                    }
                    ASPxImageSlider1.Items.Add("Content/Black.png");
                    ASPxImageSlider1.Items.Add("Content/Black.png");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
    }

    public class DataFeed
    {
        public string Link { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string[] Concepts { get; set; }

        public DateTime PublishTime { get; set; }

        public string Source { get; set; }

        public string Image { get; set; }
    }
}