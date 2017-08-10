using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdmin.Controls
{
    public partial class MessageBox : System.Web.UI.UserControl
    {
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
                lblErrorMessage.CssClass = m_CssClass;
            }
        }

        public string Message
        {
            get
            {
                return lblErrorMessage.Text;
            }
            set
            {
                lblErrorMessage.Text = value;
                if (lblErrorMessage.Text == "")
                    imgIcon.Visible = false;
                else
                    imgIcon.Visible = true;
            }
        }
    }
}