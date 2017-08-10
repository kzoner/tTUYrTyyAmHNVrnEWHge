using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdmin.Controls
{
    public partial class NotifyBox : System.Web.UI.UserControl
    {
        public event EventHandler NextClicked;

        private string m_CssClass = "";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string CssClass
        {
            get
            {
                return m_CssClass;
            }
            set
            {
                m_CssClass = value;
                lblNotifyMessage.CssClass = m_CssClass;
            }
        }

        public string Message
        {
            get
            {
                return lblNotifyMessage.Text;
            }
            set
            {
                lblNotifyMessage.Text = value;
                if (lblNotifyMessage.Text == "")
                {
                    imgIcon.Visible = false;
                    btnNext.Visible = false;
                }
                else
                {
                    imgIcon.Visible = true;
                    btnNext.Visible = true;
                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (NextClicked != null)
            {
                NextClicked(sender, EventArgs.Empty);
            }
        }
    }
}