using Inside.InsideData.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inside.InsideData.DataAccess
{
    public class AccountAdapter : GeneralDataAdapter
    {
        public DataTable AccountLevel_GetList(int accountLevelId, int accountTypeId, int accountLevelStatus)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@AccountLevelId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountLevelId;
                paramList.Add(param);

                param = new SqlParameter("@AccountTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountTypeId;
                paramList.Add(param);

                param = new SqlParameter("@AccountLevelStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountLevelStatus;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_AccountLevel_GetList", CommandType.StoredProcedure, paramList.ToArray()))
                {
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }

                    Database.CloseConn();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public DataTable AccountType_GetList(int accountTypeId, int accountTypeStatus)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@AccountTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountTypeId;
                paramList.Add(param);

                param = new SqlParameter("@AccountTypeStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountTypeStatus;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_AccountType_GetList", CommandType.StoredProcedure, paramList.ToArray()))
                {
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }

                    Database.CloseConn();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public int Account_RowTotal(int accountTypeId, string accountName, int accountLevelId, int accountStatus)
        {
            DataTable dt = new DataTable("dt");
            int rowTotal = 0;
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@AccountTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountTypeId;
                paramList.Add(param);

                param = new SqlParameter("@AccountName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = accountName;
                paramList.Add(param);

                param = new SqlParameter("@AccountLevelId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountLevelId;
                paramList.Add(param);

                param = new SqlParameter("@AccountStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountStatus;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Account_RowTotal", CommandType.StoredProcedure, paramList.ToArray()))
                {
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                        rowTotal = int.Parse(dt.Rows[0]["RowTotal"].ToString());
                    }

                    Database.CloseConn();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowTotal;
        }

        public DataTable Account_Search(int accountTypeId, string accountName, int accountLevelId, int accountStatus, int rowsPerPage, int pageNumber)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@AccountTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountTypeId;
                paramList.Add(param);

                param = new SqlParameter("@AccountName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = accountName;
                paramList.Add(param);

                param = new SqlParameter("@AccountLevelId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountLevelId;
                paramList.Add(param);

                param = new SqlParameter("@AccountStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountStatus;
                paramList.Add(param);

                param = new SqlParameter("@RowsPerPage", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = rowsPerPage;
                paramList.Add(param);

                param = new SqlParameter("@PageNumber", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = pageNumber;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Account_Search", CommandType.StoredProcedure, paramList.ToArray()))
                {
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }

                    Database.CloseConn();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public DataTable Account_GetList(int accountId)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@AccountId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountId;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Account_GetList", CommandType.StoredProcedure, paramList.ToArray()))
                {
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }

                    Database.CloseConn();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public DataTable Account_GetList_AccountType(int accountId, int accountTypeId, int acountStatus)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@AccountId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountId;
                paramList.Add(param);

                param = new SqlParameter("@AccountTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountTypeId;
                paramList.Add(param);

                param = new SqlParameter("@AccountStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = acountStatus;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Account_GetList_AccountType", CommandType.StoredProcedure, paramList.ToArray()))
                {
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }

                    Database.CloseConn();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public void Account_Insert(AccountBase account, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@AccountTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = account.AccountTypeId;
                paramList.Add(param);

                param = new SqlParameter("@AccountName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = account.AccountName;
                paramList.Add(param);

                param = new SqlParameter("@AccountShortName", SqlDbType.NVarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.AccountShortName;
                paramList.Add(param);

                param = new SqlParameter("@AccountLevelId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = account.AccountLevelId;
                paramList.Add(param);

                param = new SqlParameter("@ContactName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = account.ContactName;
                paramList.Add(param);

                param = new SqlParameter("@Address", SqlDbType.NVarChar, 200);
                param.Direction = ParameterDirection.Input;
                param.Value = account.Address;
                paramList.Add(param);

                param = new SqlParameter("@PhoneNumber1", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.PhoneNumber1;
                paramList.Add(param);

                param = new SqlParameter("@PhoneNumber2", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.PhoneNumber2;
                paramList.Add(param);

                param = new SqlParameter("@PhoneNumber3", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.PhoneNumber3;
                paramList.Add(param);

                param = new SqlParameter("@Email", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.Email;
                paramList.Add(param);

                param = new SqlParameter("@Website", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.Website;
                paramList.Add(param);

                param = new SqlParameter("@Note", SqlDbType.NVarChar, 500);
                param.Direction = ParameterDirection.Input;
                param.Value = account.Note;
                paramList.Add(param);

                param = new SqlParameter("@AccountStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = account.AccountStatus;
                paramList.Add(param);

                param = new SqlParameter("@CreateDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = account.CreateDate;
                paramList.Add(param);

                param = new SqlParameter("@CreateUser", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.CreateUser;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Account_Insert", CommandType.StoredProcedure, paramList.ToArray()))
                {
                    if (dr.HasRows)
                    {
                        DataTable dt = new DataTable("dt");
                        dt.Load(dr);

                        DataRow row = dt.Rows[0];
                        code = int.Parse(row["code"].ToString());
                        msg = row["msg"].ToString();
                    }

                    Database.CloseConn();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Account_Update(AccountBase account, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@AccountId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = account.AccountId;
                paramList.Add(param);

                param = new SqlParameter("@AccountTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = account.AccountTypeId;
                paramList.Add(param);

                param = new SqlParameter("@AccountName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = account.AccountName;
                paramList.Add(param);

                param = new SqlParameter("@AccountShortName", SqlDbType.NVarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.AccountShortName;
                paramList.Add(param);

                param = new SqlParameter("@AccountLevelId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = account.AccountLevelId;
                paramList.Add(param);

                param = new SqlParameter("@ContactName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = account.ContactName;
                paramList.Add(param);

                param = new SqlParameter("@Address", SqlDbType.NVarChar, 200);
                param.Direction = ParameterDirection.Input;
                param.Value = account.Address;
                paramList.Add(param);

                param = new SqlParameter("@PhoneNumber1", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.PhoneNumber1;
                paramList.Add(param);

                param = new SqlParameter("@PhoneNumber2", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.PhoneNumber2;
                paramList.Add(param);

                param = new SqlParameter("@PhoneNumber3", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.PhoneNumber3;
                paramList.Add(param);

                param = new SqlParameter("@Email", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.Email;
                paramList.Add(param);

                param = new SqlParameter("@Website", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.Website;
                paramList.Add(param);

                param = new SqlParameter("@Note", SqlDbType.NVarChar, 500);
                param.Direction = ParameterDirection.Input;
                param.Value = account.Note;
                paramList.Add(param);

                param = new SqlParameter("@AccountStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = account.AccountStatus;
                paramList.Add(param);

                param = new SqlParameter("@UpdateDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = account.UpdateDate;
                paramList.Add(param);

                param = new SqlParameter("@UpdateUser", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = account.UpdateUser;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Account_Update", CommandType.StoredProcedure, paramList.ToArray()))
                {
                    if (dr.HasRows)
                    {
                        DataTable dt = new DataTable("dt");
                        dt.Load(dr);

                        DataRow row = dt.Rows[0];
                        code = int.Parse(row["code"].ToString());
                        msg = row["msg"].ToString();
                    }

                    Database.CloseConn();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
