using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Inside.SecurityProviders.Bussiness;
using Inside.SecurityProviders;

namespace WebAdmin.Admin.Application.Create
{
    public partial class Default : Base.BasePage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "605C5B59-D58E-4149-8F67-C2ACE6DCCAAA";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Tạo ứng dụng");
                uctConfirmBox.Visible = false;
            }
            uctErrorBox.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ApplicationManager app = new ApplicationManager() ;

            try
            {

                if(app.Exists(txtAppName.Text.Trim()))
                {
                    uctErrorBox.Visible = true;
                    uctErrorBox.Message = "Ứng dụng đã tồn tại, vui lòng nhập tên khác !";
                    return;
                }

                app.Create(txtAppName.Text.Trim(), txtAppDescription.Text.Trim());
                uctConfirmBox.Visible = true;
                uctConfirmBox.ConfirmMessage = "Thêm ứng dụng thành công. <br/> Tiếp tục tạo ứng dụng mới không ?";
            }
             catch(Exception ex)
            {
                uctErrorBox.Visible = true;
                uctErrorBox.Message = "Lỗi hệ thống";
                this.SaveErrorLog(ex);
            }
        }

        public void ClickedYes(object sender, EventArgs e)
        {
            txtAppName.Text = "";
            txtAppDescription.Text = "";
            this.uctConfirmBox.Visible = false;
        }

        public void ClickedNo(object sender, EventArgs e)
        {
            Response.Redirect("../Manage/");
        }

    }
}
