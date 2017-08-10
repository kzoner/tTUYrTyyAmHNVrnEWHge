using System;
using System.Data;
using System.Data.SqlClient;

using Inside.DataProviders;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders.DataAccess
{
    internal class ResourceAdpater
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
        /// Create new resource
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="application"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public Resource Create(string resourceTypeCode, string path, string fileName, string link, int applicationId, string resourceName, bool status)
        {
            try
            {
                // create sql parameters
                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;

                SqlParameter prmPath = new SqlParameter("@Path", SqlDbType.VarChar, 250);
                prmPath.Direction = ParameterDirection.Input;

                SqlParameter prmFileName = new SqlParameter("@FileName", SqlDbType.VarChar, 250);
                prmFileName.Direction = ParameterDirection.Input;

                SqlParameter prmLink = new SqlParameter("@Link", SqlDbType.VarChar, 250);
                prmLink.Direction = ParameterDirection.Input;

                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;

                SqlParameter prmResourceName = new SqlParameter("@ResourceName", SqlDbType.NVarChar, 250);
                prmResourceName.Direction = ParameterDirection.Input;

                SqlParameter prmStatus = new SqlParameter("@Status", SqlDbType.Bit, 1);
                prmStatus.Direction = ParameterDirection.Input;

                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Output;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // asign param value
                prmResourceTypeCode.Value = resourceTypeCode;
                prmPath.Value = path;
                prmFileName.Value = fileName;
                prmLink.Value = link;
                prmApplicationID.Value = applicationId;
                if (string.IsNullOrEmpty(resourceName))
                {
                    prmResourceName.Value = DBNull.Value;
                }
                else
                {
                    prmResourceName.Value = resourceName;
                }
                prmStatus.Value = status;

                // execute query
                Database.ExecuteNonQuery("UspCreateResource", CommandType.StoredProcedure
                    , prmResourceTypeCode
                    , prmPath
                    , prmFileName
                    , prmLink
                    , prmApplicationID
                    , prmResourceName
                    , prmStatus
                    , prmResourceID
                    , prmErrorNumber
                    , prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());

                switch (errorNumber)
                {
                    case 0:
                        int resourceId = int.Parse(prmResourceID.Value.ToString());
                        Resource resource = new Resource(resourceId, resourceTypeCode, path, fileName, link, applicationId, resourceName, status);
                        return resource;

                    default:
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
        /// <param name="resourceTypeCode"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="link"></param>
        /// <param name="applicationId"></param>
        /// <param name="resourceName"></param>
        /// <param name="status"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Resource Create(string resourceTypeCode, string path, string fileName, string link, int applicationId, string resourceName, bool status, string token)
        {
            try
            {
                // create sql parameters
                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;

                SqlParameter prmPath = new SqlParameter("@Path", SqlDbType.VarChar, 250);
                prmPath.Direction = ParameterDirection.Input;

                SqlParameter prmFileName = new SqlParameter("@FileName", SqlDbType.VarChar, 250);
                prmFileName.Direction = ParameterDirection.Input;

                SqlParameter prmLink = new SqlParameter("@Link", SqlDbType.VarChar, 250);
                prmLink.Direction = ParameterDirection.Input;

                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;

                SqlParameter prmResourceName = new SqlParameter("@ResourceName", SqlDbType.NVarChar, 250);
                prmResourceName.Direction = ParameterDirection.Input;

                SqlParameter prmStatus = new SqlParameter("@Status", SqlDbType.Bit, 1);
                prmStatus.Direction = ParameterDirection.Input;

                SqlParameter prmToken = new SqlParameter("@Token", SqlDbType.VarChar, 50);
                prmToken.Direction = ParameterDirection.Input;

                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Output;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // asign param value
                prmResourceTypeCode.Value = resourceTypeCode;
                prmPath.Value = path;
                prmFileName.Value = fileName;
                prmLink.Value = link;
                prmApplicationID.Value = applicationId;
                if (string.IsNullOrEmpty(resourceName))
                {
                    prmResourceName.Value = DBNull.Value;
                }
                else
                {
                    prmResourceName.Value = resourceName;
                }
                prmStatus.Value = status;

                if (string.IsNullOrEmpty(token))
                {
                    prmToken.Value = DBNull.Value;
                }
                else
                {
                    prmToken.Value = token;
                }

                // execute query
                Database.ExecuteNonQuery("UspCreateResource", CommandType.StoredProcedure
                    , prmResourceTypeCode
                    , prmPath
                    , prmFileName
                    , prmLink
                    , prmApplicationID
                    , prmResourceName
                    , prmStatus
                    , prmToken
                    , prmResourceID
                    , prmErrorNumber
                    , prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());

                switch (errorNumber)
                {
                    case 0:
                        int resourceId = int.Parse(prmResourceID.Value.ToString());
                        Resource resource = new Resource(resourceId, resourceTypeCode, path, fileName, link, applicationId, resourceName, status, token);
                        return resource;

                    default:
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
        /// Update resource info
        /// </summary>
        /// <param name="resource"></param>
        public void Update(Resource resource)
        {
            try
            {
                // create sql parameters
                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;

                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;

                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;

                SqlParameter prmResourceName = new SqlParameter("@ResourceName", SqlDbType.NVarChar, 250);
                prmResourceName.Direction = ParameterDirection.Input;

                SqlParameter prmPath = new SqlParameter("@Path", SqlDbType.NVarChar, 250);
                prmPath.Direction = ParameterDirection.Input;

                SqlParameter prmFileName = new SqlParameter("@FileName", SqlDbType.NVarChar, 50);
                prmFileName.Direction = ParameterDirection.Input;

                SqlParameter prmLink = new SqlParameter("@Link", SqlDbType.NVarChar, 250);
                prmLink.Direction = ParameterDirection.Input;

                SqlParameter prmStatus = new SqlParameter("@Status", SqlDbType.Bit, 1);
                prmStatus.Direction = ParameterDirection.Input;

                SqlParameter prmToken = new SqlParameter("@Token", SqlDbType.VarChar, 50);
                prmToken.Direction = ParameterDirection.Input;


                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // asign param value
                prmResourceID.Value = resource.ResourceID;
                prmResourceTypeCode.Value = resource.ResourceTypeCode;
                prmApplicationID.Value = resource.ApplicationID;                
                if (string.IsNullOrEmpty(resource.ResourceName))
                {
                    prmResourceName.Value = DBNull.Value;
                }
                else
                {
                    prmResourceName.Value = resource.ResourceName;
                }

                if (string.IsNullOrEmpty(resource.Path))
                {
                    prmPath.Value = DBNull.Value;
                }
                else
                {
                    prmPath.Value = resource.Path;
                }

                if (string.IsNullOrEmpty(resource.FileName))
                {
                    prmFileName.Value = DBNull.Value;
                }
                else
                {
                    prmFileName.Value = resource.FileName;
                }

                if (string.IsNullOrEmpty(resource.Link))
                {
                    prmLink.Value = DBNull.Value;
                }
                else
                {
                    prmLink.Value = resource.Link;
                }

                prmStatus.Value = resource.Status;

                if (string.IsNullOrEmpty(resource.Token))
                {
                    prmToken.Value = DBNull.Value;
                }
                else
                {
                    prmToken.Value = resource.Token;
                }

                // execute query
                Database.ExecuteNonQuery("UspUpdateResource", CommandType.StoredProcedure
                    , prmResourceID
                    , prmResourceTypeCode
                    , prmApplicationID
                    , prmResourceName
                    , prmPath
                    , prmFileName
                    , prmLink
                    , prmStatus
                    , prmToken
                    , prmErrorNumber
                    , prmErrorMessage);

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
        /// Remove resource by ID
        /// </summary>
        /// <param name="resource"></param>
        public void Remove(int resourceId)
        {
            try
            {
                // create sql parameters
                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // asign param value
                prmResourceID.Value = resourceId;

                // execute query
                Database.ExecuteNonQuery("UspRemoveResource", CommandType.StoredProcedure
                    , prmResourceID
                    , prmErrorNumber
                    , prmErrorMessage);

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
        /// Get resource by ID
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public Resource GetResource(int resourceId)
        {
            try
            {
                // create sql parameters
                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;
                prmResourceID.Value = resourceId;

                // execute query
                using (IDataReader dr = Database.ExecuteReader("UspGetResource", CommandType.StoredProcedure, prmResourceID))
                {
                    while (dr.Read())
                    {
                        Resource resource = Populate(dr);

                        return resource;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all resources
        /// </summary>
        /// <returns></returns>
        public ResourceCollection GetAllResources()
        {
            try
            {  
                ResourceCollection collection = new ResourceCollection();

                // execute query
                using (IDataReader dr = Database.ExecuteReader("UspGetAllResources", CommandType.StoredProcedure))
                {
                    while (dr.Read())
                    {
                        Resource resource = Populate(dr);

                        collection.Add(resource);
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
        /// Get all resources which do not reference to any menuitem
        /// </summary>
        /// <returns></returns>
        public ResourceCollection GetAllOrphanResource(int ApplicationID)
        {
            try
            {
                ResourceCollection collection = new ResourceCollection();

                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = ApplicationID;
                // execute query
                using (IDataReader dr = Database.ExecuteReader("UspGetAllOrphanResources", CommandType.StoredProcedure, prmApplicationID))
                {
                    while (dr.Read())
                    {
                        Resource resource = Populate(dr);

                        collection.Add(resource);
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
        /// Find Resources by Application ID
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public ResourceCollection FindResourcesByApplication(int applicationId)
        {
            try
            {
                ResourceCollection collection = new ResourceCollection();

                // create sql parameters
                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = applicationId;

                // execute query
                using (IDataReader dr = Database.ExecuteReader("UspFindResourcesByApplication", CommandType.StoredProcedure, prmApplicationID))
                {
                    while (dr.Read())
                    {
                        Resource resource = Populate(dr);

                        collection.Add(resource);
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
        /// Find resource by resource name
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public ResourceCollection FindResourcesByName(string name)
        {
            try
            {
                ResourceCollection collection = new ResourceCollection();

                // create sql parameters
                SqlParameter prmResourceName = new SqlParameter("@ResourceName", SqlDbType.NVarChar, 250);
                prmResourceName.Direction = ParameterDirection.Input;
                prmResourceName.Value = name;

                // execute query
                using (IDataReader dr = Database.ExecuteReader("UspFindResourcesByName", CommandType.StoredProcedure, prmResourceName ))
                {
                    while (dr.Read())
                    {
                        Resource resource = Populate(dr);

                        collection.Add(resource);
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
        /// Find resource by Path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ResourceCollection FindResourcesByPath(string path)
        {
            try
            {
                ResourceCollection collection = new ResourceCollection();

                // create sql parameters
                SqlParameter prmResourcePath = new SqlParameter("@Path", SqlDbType.NVarChar, 250);
                prmResourcePath.Direction = ParameterDirection.Input;
                prmResourcePath.Value = path;

                // execute query
                using (IDataReader dr = Database.ExecuteReader("UspFindResourcesByPath", CommandType.StoredProcedure, prmResourcePath))
                {
                    while (dr.Read())
                    {
                        Resource resource = Populate(dr);

                        collection.Add(resource);
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
        /// Update resource status
        /// </summary>
        /// <param name="resourceID">Resource ID</param>
        /// <param name="status">New Status</param>
        public void UpdateResourceStatus(int resourceID, bool status)
        {
            try
            {
                // create sql parameters
                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;                

                SqlParameter prmStatus = new SqlParameter("@Status", SqlDbType.Bit, 1);
                prmStatus.Direction = ParameterDirection.Input;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // asign param value
                prmResourceID.Value = resourceID;
                prmStatus.Value = status;

                // execute query
                Database.ExecuteNonQuery("UspUpdateResourceStatus", CommandType.StoredProcedure
                    , prmResourceID                    
                    , prmStatus
                    , prmErrorNumber
                    , prmErrorMessage);

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
        /// Lấy tất cả user / role liên quan đến resource (kể cả deny)
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public DataTable GetAllRolesUsersByResource(int resourceID)
        {
            try
            {
                DataTable dtRet = new DataTable();

                // create sql parameters
                SqlParameter prmResourceID = new SqlParameter("@ResourceId", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;
                prmResourceID.Value = resourceID;

                // execute query
                using (IDataReader dr = Database.ExecuteReader("UspGetAllRolesUsersByResource", CommandType.StoredProcedure, prmResourceID))
                {
                    dtRet.Load(dr);
                }

                return dtRet;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        /// <summary>
        /// Lấy danh sách user / role theo resourceID
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public DataTable GetUsersAndRolesByResourceID(int resourceID)
        {
            try
            {
                DataTable dtRet = new DataTable();

                // create sql parameters
                SqlParameter prmResourceID = new SqlParameter("@ResourceId", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;
                prmResourceID.Value = resourceID;

                // execute query
                using (IDataReader dr = Database.ExecuteReader("UspGetUsersAndRolesByResource", CommandType.StoredProcedure, prmResourceID))
                {
                    dtRet.Load(dr);
                }

                return dtRet;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Lấy danh sách user / role có quyền truy xuất resource
        /// </summary>
        /// <param name="iResourceID"></param>
        /// <returns></returns>
        public DataTable GetRolesUsersByResource(int iResourceID)
        {
            try
            {
                DataTable dtRet = new DataTable();

                // create sql parameters
                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;
                prmResourceID.Value = iResourceID;

                // execute query
                using (IDataReader dr = Database.ExecuteReader("UspGetRolesUsersByResource", CommandType.StoredProcedure, prmResourceID))
                {
                    dtRet.Load(dr);
                }

                return dtRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Generate a new Token
        /// </summary>
        /// <returns>A new token string</returns>
        public string GenerateToken()
        {
            try
            {
                string newToken = "";

                // execute query
                using (IDataReader dr = Database.ExecuteReader("UspGenerateToken", CommandType.StoredProcedure))
                {
                    while (dr.Read())
                    {
                        newToken = dr.GetString(0);
                    }
                }

                return newToken;
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
        private Resource Populate(IDataReader dr)
        {
            try
            {
                int resourceId;
                string resourceTypeCode;
                string path;
                string fileName;
                string link;
                int applicationId;
                string resourceName;
                bool status;
                string token;

                resourceId = int.Parse(dr["ResourceID"].ToString());
                resourceTypeCode = dr["ResourceTypeCode"].ToString();
                path = dr["Path"].ToString();
                fileName = dr["FileName"].ToString();
                link = dr["Link"].ToString();
                applicationId = int.Parse(dr["ApplicationID"].ToString());
                resourceName = dr["ResourceName"].ToString();
                status = bool.Parse(dr["Status"].ToString());
                token = dr["Token"].ToString();

                Resource resource = new Resource(resourceId, resourceTypeCode, path, fileName, link, applicationId, resourceName, status, token);

                return resource;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                
    }
}
