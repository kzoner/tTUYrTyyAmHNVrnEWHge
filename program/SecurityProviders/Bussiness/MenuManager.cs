using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Inside.SecurityProviders;
using Inside.SecurityProviders.DataAccess;
using System.Data;
namespace Inside.SecurityProviders
{
    public class MenuManager
    {

        private MenuAdapter m_menuAdapter = null;

        private MenuAdapter Adapter
        {
            get
            {
                if (m_menuAdapter == null)
                {
                    m_menuAdapter = new MenuAdapter();
                }
                return m_menuAdapter;
            }
        }
        
       /// <summary>
       /// 
       /// </summary>
       /// <param name="iResourceID"></param>
       /// <param name="strDisplayName"></param>
       /// <param name="iParentMenuID"></param>
       /// <returns></returns>
        public int Create(int iResourceID, string strDisplayName, int iParentMenuID)
        {
            return Adapter.Create(iResourceID, strDisplayName, iParentMenuID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menu"></param>
        public void Update(int iMenuID, string strDisplayName, int iResourceID, int iParentID)
        {
            Adapter.Update(iMenuID, strDisplayName, iResourceID, iParentID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMenuID"></param>
        /// <returns></returns>
        public int Remove(int iMenuID)
        {
            return Adapter.Remove(iMenuID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menu"></param>
        public void RemoveMenuTree(MenuItem menu)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataTable GetMenuItem(int menuID)
        {
            return Adapter.GetMenuItemInfo(menuID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public MenuItemCollection GetApplicationMenus(int applicationId)
        {
            try
            {
                return this.Adapter.GetMenuItemsByApplication(applicationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public MenuItemCollection GetUserMenus(int iApplicationID, string strUserName)
        {
            try
            {
                return this.Adapter.GetMenuByUser(strUserName, iApplicationID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Move menu item up or down
        /// </summary>
        /// <param name="iMenuID">ID of the menu item</param>
        /// <param name="iDirection">1:Up , 0:Down</param>
        public void MoveMenu(int iMenuID, int iDirection)
        {
            try
            {
                Adapter.MoveMenu(iMenuID, iDirection);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}
