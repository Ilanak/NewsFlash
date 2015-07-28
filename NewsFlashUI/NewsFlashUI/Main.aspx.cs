using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestSharp;
using Newtonsoft;
using Newtonsoft.Json;

namespace NewsFlashUI
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // DataFeedsService.svc/GetFeeds?Topic=s1
            var client = new RestClient("http://datafeeds.cloudapp.net/");

            var request = new RestRequest("DataFeedsService.svc/GetFeeds", Method.GET);
            request.AddParameter("Topic", "s1");

            var result = client.Execute(request);


            var feeds = JsonConvert.DeserializeObject(result.Content);

            string[] filePaths = new string[0]; // = Directory.GetFiles(@"C:\GitHubRepo\NewsFlash\NewsFlashUI\NewsFlashUI\Content", "*.jpg");
            for (int i = 0; i < filePaths.Length; i++){
                string fileName = Path.GetFileNameWithoutExtension(filePaths[i]);
                string[] words = fileName.Split(new char[] {' '});
                string filePath = Path.Combine("Content/", Path.GetFileName(filePaths[i]));
                for (int j = 0; j < words.Length; j++)
                {
                    ASPxImageSlider1.Items.Add(filePath, string.Empty, "http://www.cnn.com", "Arts", words[j]);
                   
                }
                ASPxImageSlider1.Items.Add("Content/Black.png");
                ASPxImageSlider1.Items.Add("Content/Black.png");
                
                
            }
        }
    }
}