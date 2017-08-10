using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdmin
{
    public partial class ContentPage : System.Web.UI.MasterPage
    {
        public void SetFormTitle(string s)
        {
            lblFormTitle.Text = s;
        }
    }
}
