using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Inside.SecurityProviders;
namespace WebAdmin.ChecklistNagios
{
    /// <summary>
    /// Summary description for checklist
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Adminchecklist : Base.BaseWebservice
    {

        [WebMethod]
        public string InsideGate_Admin_CheckListNagios()
        {
            UserManager userManager = new UserManager();
            UserCollection users = new UserCollection();
            try
            {
                users = userManager.GetAllUsers();
                if (users.Count <= 0)
                    return "1|Khong co du lieu";
                return "0|Hoat dong tot";
            }
            catch (Exception ex)
            {
                SaveErrorLog(ex);
                return "2|Bi Loi";
            }
        }

        
    }
}
