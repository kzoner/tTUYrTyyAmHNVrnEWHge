using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdmin.Controls
{
    public partial class ConfirmBox : System.Web.UI.UserControl
    {
        public event EventHandler YesClicked;
        public event EventHandler NoClicked;
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string ConfirmMessage
        {
            get
            {
                return lblConfirmMessage.Text;
            }
            set
            {
                lblConfirmMessage.Text = value;
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            if (YesClicked != null)
            {
                YesClicked(sender, EventArgs.Empty);
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (NoClicked != null)
            {
                NoClicked(sender, EventArgs.Empty);
            }
        }
    }
}