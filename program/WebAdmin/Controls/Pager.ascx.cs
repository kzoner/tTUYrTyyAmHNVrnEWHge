using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdmin.Controls
{
    public partial class Pager : Base.BaseControl
    {
        private int selectedValue;
        public event EventHandler SelectChange;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public int SelectedPageIndex
        {
            get
            {
                return selectedValue;
            }
        }

        public void SetPageNumber(int totalRecords, int pageSize, int currentPage)
        {
            int pages = 1;

            if (totalRecords % pageSize > 0)
                pages = totalRecords / pageSize + 1;
            else
                pages = totalRecords / pageSize;

            this.ddlPageNumber.Items.Clear();


            for (int i = 1; i <= pages; i++)
            {
                ListItem newItem = new ListItem(i.ToString(), (i - 1).ToString());
                ddlPageNumber.Items.Add(newItem);
            }
            ddlPageNumber.SelectedValue = (currentPage).ToString();
        }

        public void SetPageNumber(int totalPage, int currentPage)
        {
            try
            {
                this.ddlPageNumber.Items.Clear();

                for (int i = 1; i <= totalPage; i++)
                {
                    ListItem newItem = new ListItem(i.ToString(), (i - 1).ToString());
                    ddlPageNumber.Items.Add(newItem);
                }
                ddlPageNumber.SelectedValue = (currentPage).ToString();
            }
            catch (Exception ex)
            {
                this.SaveErrorLog(ex);
            }
        }


        protected void ddlPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedValue = int.Parse(ddlPageNumber.SelectedValue);
            if (SelectChange != null)
            {
                SelectChange(sender, EventArgs.Empty);
            }
        }
        protected void btnFirst_Click(object sender, ImageClickEventArgs e)
        {
            selectedValue = 0;
            ddlPageNumber.SelectedValue = "0";

            if (SelectChange != null)
            {
                SelectChange(sender, EventArgs.Empty);
            }

        }
        protected void btnPre_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlPageNumber.Items.Count > 0)
            {
                selectedValue = int.Parse(ddlPageNumber.SelectedValue);
                if (selectedValue > 0)
                {
                    selectedValue = selectedValue - 1;
                    ddlPageNumber.SelectedValue = selectedValue.ToString();

                    if (SelectChange != null)
                    {
                        SelectChange(sender, EventArgs.Empty);
                    }
                }
            }
            else
                return;
        }
        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlPageNumber.Items.Count > 0)
            {
                selectedValue = int.Parse(ddlPageNumber.SelectedValue);
                if (selectedValue < ddlPageNumber.Items.Count - 1)
                {
                    selectedValue = selectedValue + 1;
                    ddlPageNumber.SelectedValue = selectedValue.ToString();

                    if (SelectChange != null)
                    {
                        SelectChange(sender, EventArgs.Empty);
                    }
                }
            }
            else
                return;
        }
        protected void btnLast_Click(object sender, ImageClickEventArgs e)
        {
            selectedValue = ddlPageNumber.Items.Count - 1;
            ddlPageNumber.SelectedValue = selectedValue.ToString();
            ddlPageNumber.SelectedValue = selectedValue.ToString();

            if (SelectChange != null)
            {
                SelectChange(sender, EventArgs.Empty);
            }

        }

    }
}