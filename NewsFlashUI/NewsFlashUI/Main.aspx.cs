using System;
using System.Collections.Generic;
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
            ASPxImageSlider1.Items.Add("/Content/food1.jpg");
            ASPxImageSlider1.Items.Add("/Content/food2.jpg");
            ASPxImageSlider1.Items.Add("/Content/food3.jpg");
            ASPxImageSlider1.Items.Add("/Content/food4.jpg");
        }
    }
}