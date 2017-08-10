using System;
using System.Data;
using System.Data.SqlClient;

using Inside.DataProviders;
using Inside.SecurityProviders;    
namespace Inside.SecurityProviders.DataAccess
{
    internal class ResourceTypeAdapter
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
        /// 
        /// </summary>
        /// <param name="resourceTypeCode"></param>
        /// <param name="name"></param>        
        /// <returns></returns>
        public ResourceType Create(string resourceTypeCode, string name)
        {
            try
            {
                // create sql parameters
                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;
                prmResourceTypeCode.Value = resourceTypeCode;

                SqlParameter prmName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
                prmName.Direction = ParameterDirection.Input;
                prmName.Value = name;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure
                Database.ExecuteNonQuery("UspCreateResourceType", CommandType.StoredProcedure
                    , prmResourceTypeCode
                    , prmName
                    , prmErrorNumber
                    , prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());

                switch (errorNumber)
                {
                    case 0:
                        ResourceType resType = new ResourceType(resourceTypeCode, name);
                        return resType;

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
        /// <param name="resourceType"></param>
        public void Update(ResourceType resourceType)
        {
            try
            {
                // create sql parameters
                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;
                prmResourceTypeCode.Value = resourceType.ResourceTypeCode;

                SqlParameter prmName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
                prmName.Direction = ParameterDirection.Input;
                prmName.Value = resourceType.Name;
                
                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure
                Database.ExecuteNonQuery("UspUpdateResourceType", CommandType.StoredProcedure
                    , prmResourceTypeCode
                    , prmName                    
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
        /// 
        /// </summary>
        /// <param name="resourceType"></param>
        public void Remove(ResourceType resourceType)
        {
            try
            {
                // create sql parameters
                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;
                prmResourceTypeCode.Value = resourceType.ResourceTypeCode;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure
                Database.ExecuteNonQuery("UspRemoveResourceType", CommandType.StoredProcedure
                    , prmResourceTypeCode
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

        public ResourceType GetResourceType(string resourceTypeCode)
        {
            try
            {
                // create sql parameters
                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;
                prmResourceTypeCode.Value = resourceTypeCode;

                // Execute procedure
                using (IDataReader dr = Database.ExecuteReader("UspGetResourceType", CommandType.StoredProcedure, prmResourceTypeCode))
                {
                    if (dr.Read())
                    {
                        ResourceType resourceType = Populate(dr);

                        return resourceType;
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
        /// 
        /// </summary>
        /// <returns></returns>
        public ResourceTypeCollection GetAllResourceTypes()
        {
            try
            {
                // create sql parameters
                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;
                prmResourceTypeCode.Value = DBNull.Value;

                ResourceTypeCollection collection = new ResourceTypeCollection();

                // Execute procedure
                using (IDataReader dr = Database.ExecuteReader("UspGetResourceType", CommandType.StoredProcedure, prmResourceTypeCode))
                {
                    while (dr.Read())
                    {
                        ResourceType resourceType = Populate(dr);

                        collection.Add(resourceType);
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
        /// <param name="dr"></param>
        /// <returns></returns>
        private ResourceType Populate(IDataReader dr)
        {
            try
            {
                string resourceTypeCode = dr["ResourceTypeCode"].ToString();
                string name = dr["Name"].ToString();
                string description = dr["Description"].ToString();

                ResourceType resourceType = new ResourceType(resourceTypeCode, name);

                return resourceType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
