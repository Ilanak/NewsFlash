using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsFlashUI
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] filePaths = Directory.GetFiles(@"C:\GitHubRepo\NewsFlash\NewsFlashUI\NewsFlashUI\Content", "*.jpg");
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