using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdmin
{
    public partial class Home : WebAdmin.Base.BasePage
    {
        private void GetUserDetail(string UserName)
        {
            Inside.SecurityProviders.UserManager userMan = new Inside.SecurityProviders.UserManager();
            Inside.SecurityProviders.UserCollection users = userMan.FindUsersByUserName(UserName);
            if (users.Count > 0)
            {
                foreach (Inside.SecurityProviders.User user in users)
                {
                    if (user.UserName.ToLower() == User.Identity.Name.ToLower())
                    {
                        lblUserName.Text = user.FullName;
                        lblLastLogin.Text = user.LastLogin.ToString("dd/MM/yyyy HH:mm:ss");
                        lblLastIP.Text = user.LastIP.ToString();

                        break;
                    }
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "5CCC1AA0-BCD8-4744-9DE3-0F9B7550C088";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string UserName = User.Identity.Name;
            GetUserDetail(UserName);
        }
    }
}
