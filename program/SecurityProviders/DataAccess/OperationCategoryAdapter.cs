using System;
using System.Data;
using System.Data.SqlClient;
using Inside.DataProviders;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders.DataAccess
{
    internal class OperationCategoryAdapter
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
        /// <param name="name"></param>
        /// <returns></returns>
        public OperationCategory Create(string name, string description)
        {
            try
            {
                // create sql parameters
                SqlParameter prmCode = new SqlParameter("@OperationCode", SqlDbType.Int,4);
                prmCode.Direction = ParameterDirection.Output;

                SqlParameter prmName = new SqlParameter("@Name", SqlDbType.NVarChar, 250);
                prmName.Direction = ParameterDirection.Input;
                prmName.Value = name;

                SqlParameter prmDesc = new SqlParameter("@Description", SqlDbType.NVarChar, 500);
                prmDesc.Direction = ParameterDirection.Input;
                prmDesc.Value =  description ;

                SqlParameter prmErrNum = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrNum.Direction = ParameterDirection.Output;

                SqlParameter prmErrMsg = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrMsg.Direction = ParameterDirection.Output;

                // execute procedure to create a new operation code
                Database.ExecuteNonQuery("UspCreateOperationCategory", CommandType.StoredProcedure,
                    prmCode, prmName, prmDesc, prmErrNum, prmErrMsg);

                int errorCode = int.Parse(prmErrNum.Value.ToString());

                switch (errorCode)
                {
                    case 0: // create success

                        int operationCode = int.Parse(prmCode.Value.ToString());

                        OperationCategory operationCategory = new OperationCategory(operationCode, name, description);

                        return operationCategory;

                    default:    // error

                        string errorMessage = prmErrMsg.Value.ToString();

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
        /// <param name="category"></param>
        public void Update(OperationCategory category)
        {
            try
            {
                // create sql parameters
                SqlParameter prmCode = new SqlParameter("@OperationCode", SqlDbType.Int,4);
                prmCode.Direction = ParameterDirection.Input;
                prmCode.Value = category.OperationCode;

                SqlParameter prmName = new SqlParameter("@Name", SqlDbType.NVarChar, 250);
                prmName.Direction = ParameterDirection.Input;
                prmName.Value = category.Name;

                SqlParameter prmDesc = new SqlParameter("@Description", SqlDbType.NVarChar, 500);
                prmDesc.Direction = ParameterDirection.Input;
                prmDesc.Value = category.Description;

                SqlParameter prmErrNum = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrNum.Direction = ParameterDirection.Output;

                SqlParameter prmErrMsg = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrMsg.Direction = ParameterDirection.Output;

                // execute procedure to create a new operation code
                Database.ExecuteNonQuery("UspUpdateOperationCategory", CommandType.StoredProcedure,
                    prmCode, prmName, prmErrNum, prmErrMsg, prmDesc);

                int errorCode = int.Parse(prmErrNum.Value.ToString());

                if (errorCode > 0)
                {
                    string errorMessage = prmErrMsg.Value.ToString();

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
        /// <param name="category"></param>
        public void Remove(OperationCategory category)
        {
            try
            {
                // create sql parameters
                SqlParameter prmCode = new SqlParameter("@OperationCode", SqlDbType.Int, 50);
                prmCode.Direction = ParameterDirection.Input;
                prmCode.Value = category.OperationCode;

                SqlParameter prmErrNum = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrNum.Direction = ParameterDirection.Output;

                SqlParameter prmErrMsg = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrMsg.Direction = ParameterDirection.Output;

                // execute procedure to create a new operation code
                Database.ExecuteNonQuery("UspRemoveOperationCategory", CommandType.StoredProcedure,
                    prmCode, prmErrNum, prmErrMsg);

                int errorCode = int.Parse(prmErrNum.Value.ToString());

                if (errorCode > 0)
                {
                    string errorMessage = prmErrMsg.Value.ToString();

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
        /// <param name="operationCode"></param>
        /// <returns></returns>
        public OperationCategory GetOperationCategory(int operationCode)
        {
            try
            {
                // create sql parameters
                SqlParameter prmCode = new SqlParameter("@OperationCode", SqlDbType.Int, 4);
                prmCode.Direction = ParameterDirection.Input;
                prmCode.Value = operationCode;

                using (IDataReader dr = Database.ExecuteReader("UspGetOperationCategory", CommandType.StoredProcedure,prmCode))
                {
                    if (dr.Read())
                    {
                        OperationCategory operationCategory = new OperationCategory(
                               int.Parse(dr["OperationCode"].ToString())
                             , dr["Name"].ToString(), dr["Description"].ToString() );

                        return operationCategory;
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OperationCategoryCollection GetOperationCategoriesByResourceTypeCode(string ResourceTypeCode)
        {
            try
            {
                OperationCategoryCollection collection = new OperationCategoryCollection();
                // create sql parameters
                SqlParameter prmResourceTypeCode = new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 10);
                prmResourceTypeCode.Direction = ParameterDirection.Input;
                prmResourceTypeCode.Value = ResourceTypeCode;

                using (IDataReader dr = Database.ExecuteReader("UspGetOperationCatByResourceType", CommandType.StoredProcedure, prmResourceTypeCode))
                {
                    while (dr.Read())
                    {
                        OperationCategory operationCat = Populate(dr);

                        collection.Add(operationCat);
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
        /// <returns></returns>
        public OperationCategoryCollection GetAllOperationCategories()
        {
            try
            {
                OperationCategoryCollection collection = new OperationCategoryCollection();

                collection.Clear();

                using (IDataReader dr = Database.ExecuteReader("UspGetAllOperationCategories", CommandType.StoredProcedure))
                {
                    while (dr.Read())
                    {
                        OperationCategory category = Populate(dr);

                        collection.Add(category);
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
        private OperationCategory Populate(IDataReader dr)
        {
            try
            {
                OperationCategory operationCategory = new OperationCategory(
                    int.Parse(dr["OperationCode"].ToString())
                    , dr["Name"].ToString(), dr["Description"].ToString() );

                return operationCategory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
