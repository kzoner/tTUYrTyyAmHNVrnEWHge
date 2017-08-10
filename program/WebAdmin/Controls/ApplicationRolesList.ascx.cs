using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;
using System.Data;

namespace WebAdmin.Controls
{
    /// <summary>
    /// Control Liệt kê danh sách Role trong ứng dụng
    /// </summary>
    public partial class ApplicationRolesList : Base.BaseControl
    {

        const int numberofItemInColumn = 30; // So Role trong mot cot.

        protected override void OnInit(EventArgs e)
        {
            
           List<int> appIDs = CreateTabTitle(0);
            LoadRoles(appIDs);
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 
               
            }

        }

        
        protected List<int> CreateTabTitle( int activeIndex)
        {
            ApplicationCollection applications = GetApplication();

            DataTable dTable = new DataTable("Application");

            dTable.Columns.Add("ApplicationID");
            dTable.Columns.Add("ApplicationName");
            dTable.Columns.Add("Index");
            dTable.Columns.Add("TitleActive");
            dTable.Columns.Add("ContentActive");

            DataRow row;

            int count = applications.Count;

            int i = 0;

            List<int> appIDs = new List<int>();

            foreach (Inside.SecurityProviders.Application app in applications)
            {

                row = dTable.NewRow();

                appIDs.Add(app.ApplicationID);

                row["ApplicationID"] = app.ApplicationID;
                row["ApplicationName"] = app.Name;
                row["Index"] = i.ToString();

                //if (activeIndex >= 0)
                //{
                //    if (app.ApplicationID == activeIndex)
                //    {

                //        row["TitleActive"] = "class=\"active\"";
                //        row["ContentActive"] = "class=\"tab_contents tab_contents_active\"";

                //    }
                //    else
                //    {
                //        row["ContentActive"] = "class=\"tab_contents\"";
                //    }
                //}
                //else 
                if (i == activeIndex)
                {
                    row["TitleActive"] = "class=\"active\"";
                    row["ContentActive"] = "class=\"tab_contents tab_contents_active\"";
                }
                else
                {
                    row["ContentActive"] = "class=\"tab_contents\"";
                }

                dTable.Rows.Add(row);

                i++;
            }
            rptTabTitle.DataSource = dTable;
            rptTabTitle.DataBind();

            rptTabContent.DataSource = dTable;
            rptTabContent.DataBind();

            return appIDs;
            //LoadRoles(appIDs );
   
        }

        private void LoadRoles(List<int> appIDs  )
        {

            RoleManager roleMange = new RoleManager();
            RoleCollection roles = new RoleCollection();

            CheckBoxList cbList;
            ListItem listItem;
            
            int i = 0;
            foreach (int appID in appIDs)
            {
                 roles = roleMange.GetRoleInApplication(appID);

                cbList = (CheckBoxList)rptTabContent.Items[i].FindControl("cblRoles");
                foreach (Role role in roles)
                 {
                     listItem = new ListItem( role.RoleName , role.RoleID.ToString());
                     listItem.Enabled = true;
                     cbList.Items.Add(listItem);
                 }
                if (cbList.Items.Count > numberofItemInColumn)
                {
                    cbList.RepeatColumns = cbList.Items.Count / numberofItemInColumn + 1;
                }
                i++;
            }
            
        }

        int GetAppIndex(int[] selectedRoleID)
        {

            ApplicationManager appManager = new ApplicationManager();
            ApplicationCollection apps = new ApplicationCollection();
            
            apps = appManager.GetAllApplications();
            RoleManager roleManager = new RoleManager();
            RoleCollection roles = new RoleCollection();

            List<int> selectedRoleIDList = selectedRoleID.ToList();
            int index = 0;
            foreach (Inside.SecurityProviders.Application appItem in apps)
            {
                roles = roleManager.GetRoleInApplication(appItem.ApplicationID);
                foreach (Role roleItem in roles)
                {
                    if (selectedRoleIDList.IndexOf(roleItem.RoleID) >= 0)
                    {
                        return index;
                    }
                }
                index++; 
            }

            return index;
        }

        /// <summary>
        /// Gán hoặc lấy danh sách RoleID được chọn
        /// </summary>
        public int[] SelectedRoleID
        {
            
            get
            {
                List<int> roleIDs = new List<int>();
                CheckBoxList chkListRole;
                foreach (RepeaterItem rptItem in rptTabContent.Items)
                {
                    chkListRole = (CheckBoxList)rptItem.FindControl("cblRoles");

                    foreach (ListItem roleItem in chkListRole.Items)
                    {
                        if (roleItem.Selected)
                        {
                            roleIDs.Add(int.Parse(roleItem.Value));
                        }
                    }
                }

                return roleIDs.ToArray();

            }
            set 
            {
                if (value.Count<int>() > 0)
                {
                    int index = GetAppIndex(value);
                    List<int> appIDs = CreateTabTitle(index);
                    LoadRoles(appIDs);

                    CheckBoxList chkListRole;
                    List<int> roleList = value.ToList<int>();
                    int i = 0;
                    int activeIndex = 0;
                    foreach (RepeaterItem rptItem in rptTabContent.Items)
                    {
                        chkListRole = (CheckBoxList)rptItem.FindControl("cblRoles");

                        foreach (ListItem roleItem in chkListRole.Items)
                        {
                            if (roleList.IndexOf(int.Parse(roleItem.Value)) >= 0)
                            {
                                roleItem.Selected = true;
                                if (activeIndex == 0)
                                    activeIndex = i;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    List<int> appIDs = CreateTabTitle(0);
                    LoadRoles(appIDs);
                }
                //CreateTabTitle(activeIndex);
            }
        }

        /// <summary>
        /// Lay danh sach tat car ung dung
        /// </summary>
        /// <returns></returns>
        protected ApplicationCollection GetApplication()
        {
            ApplicationManager appManager = new ApplicationManager();
            
            return appManager.GetAllApplications();
        }

        /// <summary>
        ///Bo chon tat cac role
        /// </summary>
        public void Reset()
        {
            CreateTabTitle(0);
        }                
    }
}
