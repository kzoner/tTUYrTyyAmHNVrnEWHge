using System;
using System.Data;
using System.Data.SqlClient;

using Inside.DataProviders;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders.DataAccess
{
    internal class MenuAdapter
    {

        private SQLDatabase m_db = null;

        protected SQLDatabase Database
        {
            get
            {
                if (m_db == null)
                {
                    m_db = new SQLDatabase(ConfigurationHelper.ReadKey(Constants.STR_AUTHDB_CONN_APPSETTING_KEY));
                }
                return m_db;
            }
        }

        /// <summary>
        /// Add new MenuID
        /// </summary>
        /// <param name="iResourceID"></param>
        /// <param name="strDisplayName"></param>
        /// <param name="iParentMenuID"></param>
        /// <returns>Return MenuID if success</returns>
        public int Create(int iResourceID, string strDisplayName, int iParentMenuID)
        {
            try
            {
                // create sql parameters
                SqlParameter prmDisplayName = new SqlParameter("@DisplayName", SqlDbType.NVarChar, 100);
                prmDisplayName.Direction = ParameterDirection.Input;

                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;

                SqlParameter prmParentID = new SqlParameter("@ParentMenuID", SqlDbType.Int, 4);
                prmParentID.Direction = ParameterDirection.Input;

                SqlParameter prmMenuID = new SqlParameter("@MenuID", SqlDbType.Int, 4);
                prmMenuID.Direction = ParameterDirection.Output;
                
                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;



                prmDisplayName.Value = strDisplayName;
                prmResourceID.Value = iResourceID;
                prmParentID.Value = iParentMenuID;

                Database.ExecuteNonQuery("UspCreateMenu", CommandType.StoredProcedure, prmDisplayName, prmResourceID, prmParentID, prmMenuID, prmErrorNumber, prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());

                if (errorNumber > 0)
                {
                    string errorMessage = prmErrorMessage.Value.ToString();
                    SecurityException customEx = new SecurityException(errorMessage);
                    throw customEx;
                }
                else
                {
                    int iMenuID = int.Parse(prmMenuID.Value.ToString());
                    return iMenuID;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menu"></param>
        public void Update(int iMenuID, string strDisplayName, int iResourceID, int iParentID)
        {
            try
            {
                // create sql parameters
                SqlParameter prmMenuID = new SqlParameter("@MenuID", SqlDbType.Int, 4);
                prmMenuID.Direction = ParameterDirection.Input;

                SqlParameter prmDisplayName = new SqlParameter("@DisplayName", SqlDbType.NVarChar, 100);
                prmDisplayName.Direction = ParameterDirection.Input;

                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;

                SqlParameter prmParentID = new SqlParameter("@ParentMenuID", SqlDbType.Int, 4);
                prmParentID.Direction = ParameterDirection.Input;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                prmMenuID.Value = iMenuID;
                prmDisplayName.Value = strDisplayName;
                prmResourceID.Value = iResourceID;
                prmParentID.Value = iParentID;

                Database.ExecuteNonQuery("UspUpdateMenu", CommandType.StoredProcedure, prmMenuID, prmDisplayName, prmResourceID, prmParentID, prmErrorNumber, prmErrorMessage);
                
                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());

                if (errorNumber > 0)
                {
                    string errorMessage = prmErrorMessage.Value.ToString();
                    SecurityException customEx = new SecurityException(errorMessage);
                    throw customEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMenuID"></param>
        /// <returns></returns>
        public int Remove(int iMenuID)
        {
            try
            {
                // create sql parameters
                SqlParameter prmMenuID = new SqlParameter("@MenuID", SqlDbType.Int, 4);
                prmMenuID.Direction = ParameterDirection.Input;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                prmMenuID.Value = iMenuID;

                Database.ExecuteNonQuery("UspRemoveMenu", CommandType.StoredProcedure, prmMenuID, prmErrorNumber, prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());
                return errorNumber;
                //if (errorNumber > 0)
                //{
                //    string errorMessage = prmErrorMessage.Value.ToString();
                //    SecurityException customEx = new SecurityException(errorMessage);
                //    throw customEx;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataTable GetMenuItemInfo(int menuID)
        {
            DataTable dtMenuItemInfo = new DataTable("MenuItemInfo");
            try
            {
                // create sql parameters
                SqlParameter prmMenuID = new SqlParameter("@MenuID", SqlDbType.Int, 4);
                prmMenuID.Direction = ParameterDirection.Input;
                prmMenuID.Value = menuID;

                // execute query
                using (DataSet ds = Database.FillDataSet("UspGetMenuItemInfo", CommandType.StoredProcedure, prmMenuID))
                {
                    if(ds != null &&  ds.Tables.Count > 0 && ds.Tables[0].Rows.Count >0)
                    {
                        dtMenuItemInfo = ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return dtMenuItemInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public MenuItemCollection GetMenuItemsByApplication(int applicationId)
        {
            try
            {
                MenuItemCollection collection = new MenuItemCollection();

                // create sql parameters
                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = applicationId;

                // execute query
                using (IDataReader dr = Database.ExecuteReader("UspGetMenuItemsByApplication", CommandType.StoredProcedure, prmApplicationID))
                {
                    while (dr.Read())
                    {
                        MenuItem mnuItem = Populate(dr);
                        collection.Add(mnuItem);
                    }
                }
                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUserName"></param>
        /// <param name="iApplicationID"></param>
        /// <returns></returns>
        public MenuItemCollection GetMenuByUser(string strUserName, int iApplicationID)
        {
            try
            {
                MenuItemCollection collection = new MenuItemCollection();

                // create sql parameters
                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = iApplicationID;


                SqlParameter prmUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                prmUserName.Direction = ParameterDirection.Input;
                prmUserName.Value = strUserName;

                // execute query
                using (IDataReader dr = Database.ExecuteReader("UspGetMenuByUser", CommandType.StoredProcedure, prmUserName, prmApplicationID))
                {
                    while (dr.Read())
                    {
                        MenuItem mnuItem = Populate(dr);
                        collection.Add(mnuItem);
                    }
                }
                return collection;
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
                // create sql parameters
                SqlParameter prmMenuID = new SqlParameter("@MenuID", SqlDbType.Int, 4);
                prmMenuID.Direction = ParameterDirection.Input;

                
                SqlParameter prmDirection = new SqlParameter("@Direction", SqlDbType.Int, 4);
                prmDirection.Direction = ParameterDirection.Input;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                prmMenuID.Value = iMenuID;
                prmDirection.Value = iDirection;

                Database.ExecuteNonQuery("UspMoveMenu", CommandType.StoredProcedure, prmMenuID, prmDirection, prmErrorNumber, prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());

                if (errorNumber > 0)
                {
                    string errorMessage = prmErrorMessage.Value.ToString();
                    SecurityException customEx = new SecurityException(errorMessage);
                    throw customEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private MenuItem Populate(IDataReader dr)
        {
            try
            {
                int menuID;
                string displayName;
                string path;
                string fileName;
                string link;
                int resourceID;
                int parentMenuID;
                int depth;
                string functionName;

                menuID = int.Parse(dr["MenuID"].ToString());
                displayName = dr["DisplayName"].ToString();
                path= dr["Path"].ToString();
                fileName = dr["FileName"].ToString();
                link = dr["Link"].ToString();
                resourceID = int.Parse(dr["ResourceID"].ToString());
                parentMenuID = int.Parse(dr["ParentMenuID"].ToString());
                depth = int.Parse(dr["Depth"].ToString());
                if (dr["FunctionName"] != null)
                {
                    functionName = dr["FunctionName"].ToString();
                }
                else
                {
                    functionName = "";
                }

                MenuItem mnuItem = new MenuItem(menuID, displayName, path, fileName, link,resourceID,parentMenuID, depth, functionName);

                return mnuItem;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
