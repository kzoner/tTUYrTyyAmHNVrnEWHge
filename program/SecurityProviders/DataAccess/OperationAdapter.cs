using System;
using System.Data;
using System.Data.SqlClient;
using Inside.DataProviders;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders.DataAccess
{
    internal class OperationAdapter
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
        /// <param name="operationCode"></param>
        /// <param name="resourceTypeCode"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public Operation Create(int operationCode, string resourceTypeCode, string description)
        {
            try
            {
                // create sql param
                SqlParameter prmOperationCode = new SqlParameter("@OperationCode", SqlDbType.Int, 4);
                prmOperationCode.Direction = ParameterDirection.Input;
                prmOperationCode.Value = operationCode;

                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;
                prmResourceTypeCode.Value = resourceTypeCode;

                SqlParameter prmDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 250);
                prmDescription.Direction = ParameterDirection.Input;
                if (string.IsNullOrEmpty(description))
                {
                    prmDescription.Value = DBNull.Value;
                }
                else
                {
                    prmDescription.Value = description;
                }

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // execute stored
                Database.ExecuteNonQuery("UspCreateOperation", CommandType.StoredProcedure
                    , prmOperationCode
                    , prmResourceTypeCode
                    , prmDescription
                    , prmErrorNumber
                    , prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());

                switch (errorNumber)
                {
                    case 0:
                        Operation operation = new Operation(operationCode, resourceTypeCode, description);
                        return operation;

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
        /// <param name="operation"></param>
        public void Update(Operation operation)
        {
            try
            {
                // create sql param
                SqlParameter prmOperationCode = new SqlParameter("@OperationCode", SqlDbType.Int, 4);
                prmOperationCode.Direction = ParameterDirection.Input;
                prmOperationCode.Value = operation.OperationCode;

                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;
                prmResourceTypeCode.Value = operation.ResourceTypeCode;

                SqlParameter prmDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 250);
                prmDescription.Direction = ParameterDirection.Input;
                if (string.IsNullOrEmpty(operation.Description))
                {
                    prmDescription.Value = DBNull.Value;
                }
                else
                {
                    prmDescription.Value = operation.Description;
                }

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // execute stored
                Database.ExecuteNonQuery("UspUpdateOperation", CommandType.StoredProcedure
                    , prmOperationCode
                    , prmResourceTypeCode
                    , prmDescription
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
        /// <param name="operation"></param>
        public void Remove(Operation operation)
        {
            try
            {
                // create sql param
                SqlParameter prmOperationCode = new SqlParameter("@OperationCode", SqlDbType.Int, 4);
                prmOperationCode.Direction = ParameterDirection.Input;
                prmOperationCode.Value = operation.OperationCode;

                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;
                prmResourceTypeCode.Value = operation.ResourceTypeCode;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // execute stored
                Database.ExecuteNonQuery("UspRemoveOperation", CommandType.StoredProcedure
                    , prmOperationCode
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="category"></param>
        public Operation GetOperation(int operationCode, string resourceTypeCode)
        {
            try
            {
                // create sql param
                SqlParameter prmOperationCode = new SqlParameter("@OperationCode", SqlDbType.Int, 4);
                prmOperationCode.Direction = ParameterDirection.Input;
                prmOperationCode.Value = operationCode;

                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;
                prmResourceTypeCode.Value = resourceTypeCode;

                // execute stored
                using (IDataReader dr = Database.ExecuteReader("UspGetOperation", CommandType.StoredProcedure, prmOperationCode, prmResourceTypeCode))
                {
                    if (dr.Read())
                    {
                        Operation operation = Populate(dr);
                        return operation;
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
        /// <param name="resource"></param>
        public OperationCollection FindOperationsByResource(int resourceId)
        {
            try
            {
                OperationCollection collection = new OperationCollection();

                // create sql param
                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;
                prmResourceID.Value = resourceId;

                // execute stored
                using (IDataReader dr = Database.ExecuteReader("UspFindOperationsByResource", CommandType.StoredProcedure, prmResourceID))
                {
                    while (dr.Read())
                    {
                        Operation operation = Populate(dr);
                        collection.Add(operation);
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
        /// <param name="resourceTypeCode"></param>
        /// <returns></returns>
        public OperationCollection FindOperationsByResourceType(string resourceTypeCode)
        {
            try
            {
                OperationCollection collection = new OperationCollection();

                // create sql param
                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 50);
                prmResourceTypeCode.Direction = ParameterDirection.Input;
                prmResourceTypeCode.Value = resourceTypeCode;

                // execute stored
                using (IDataReader dr = Database.ExecuteReader("UspFindOperationsByResourceType", CommandType.StoredProcedure, prmResourceTypeCode))
                {
                    while (dr.Read())
                    {
                        Operation operation = Populate(dr);
                        collection.Add(operation);
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
        /// <param name="category"></param>
        /// <returns></returns>
        public OperationCollection FindOperationsByCategory(int operationCode)
        {
            try
            {
                OperationCollection collection = new OperationCollection();

                // create sql param
                SqlParameter prmOperationCode = new SqlParameter("@OperationCode", SqlDbType.Int, 4);
                prmOperationCode.Direction = ParameterDirection.Input;
                prmOperationCode.Value = operationCode;

                // execute stored
                using (IDataReader dr = Database.ExecuteReader("UspFindOperationsByResource", CommandType.StoredProcedure, prmOperationCode))
                {
                    while (dr.Read())
                    {
                        Operation operation = Populate(dr);
                        collection.Add(operation);
                    }
                }

                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Operation Populate(IDataReader dr)
        {
            try
            {
                int operationCode = int.Parse(dr["OperationCode"].ToString());
                string resourceTypeCode = dr["ResourceTypeCode"].ToString();
                string description = dr["Description"].ToString();

                Operation operation = new Operation(operationCode, resourceTypeCode, description);

                return operation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
