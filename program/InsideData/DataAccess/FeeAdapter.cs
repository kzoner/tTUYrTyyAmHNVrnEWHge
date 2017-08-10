using Inside.InsideData.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inside.InsideData.DataAccess
{
    public class FeeAdapter : GeneralDataAdapter
    {
        public DataTable FeeType_GetList(int feeTypeId, int feeTypeStatus)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@FeeTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = feeTypeId;
                paramList.Add(param);

                param = new SqlParameter("@feeTypeStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = feeTypeStatus;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_FeeType_GetList", CommandType.StoredProcedure, paramList.ToArray()))
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

        public int FeeType_RowTotal(string feeTypeName)
        {
            DataTable dt = new DataTable("dt");
            int rowTotal = 0;
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@FeeTypeName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = feeTypeName;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_FeeType_RowTotal", CommandType.StoredProcedure, paramList.ToArray()))
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

        public DataTable FeeType_Search(string feeTypeName, int rowsPerPage, int pageNumber)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@FeeTypeName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = feeTypeName;
                paramList.Add(param);

                param = new SqlParameter("@RowsPerPage", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = rowsPerPage;
                paramList.Add(param);

                param = new SqlParameter("@PageNumber", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = pageNumber;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_FeeType_Search", CommandType.StoredProcedure, paramList.ToArray()))
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

        public void FeeType_Insert(string feeTypeName, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@FeeTypeName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = feeTypeName;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_FeeType_Insert", CommandType.StoredProcedure, paramList.ToArray()))
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

        public void FeeType_Update(int feeTypeId, string feeTypeName, int feeTypeStatus, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@FeeTypeId", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = feeTypeId;
                paramList.Add(param);

                param = new SqlParameter("@FeeTypeName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = feeTypeName;
                paramList.Add(param);

                param = new SqlParameter("@FeeTypeStatus", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = feeTypeStatus;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_FeeType_Update", CommandType.StoredProcedure, paramList.ToArray()))
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

        public int Fee_RowTotal(DateTime fromDate, DateTime toDate, int feeTypeId)
        {
            DataTable dt = new DataTable("dt");
            int rowTotal = 0;
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@FromDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = fromDate;
                paramList.Add(param);

                param = new SqlParameter("@ToDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = toDate;
                paramList.Add(param);

                param = new SqlParameter("@FeeTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = feeTypeId;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Fee_RowTotal", CommandType.StoredProcedure, paramList.ToArray()))
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

        public DataTable Fee_Search(DateTime fromDate, DateTime toDate, int feeTypeId, int rowsPerPage, int pageNumber)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@FromDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = fromDate;
                paramList.Add(param);

                param = new SqlParameter("@ToDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = toDate;
                paramList.Add(param);

                param = new SqlParameter("@FeeTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = feeTypeId;
                paramList.Add(param);

                param = new SqlParameter("@RowsPerPage", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = rowsPerPage;
                paramList.Add(param);

                param = new SqlParameter("@PageNumber", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = pageNumber;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Fee_Search", CommandType.StoredProcedure, paramList.ToArray()))
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

        public DataTable Fee_GetList(int feeId)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@FeeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = feeId;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Fee_GetList", CommandType.StoredProcedure, paramList.ToArray()))
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

        public void Fee_Insert(FeeBase fee, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@FeeTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.FeeTypeId;
                paramList.Add(param);

                param = new SqlParameter("@Amount", SqlDbType.Decimal);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.Amount;
                paramList.Add(param);

                param = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.UserName;
                paramList.Add(param);

                param = new SqlParameter("@Note", SqlDbType.NVarChar, 500);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.Note;
                paramList.Add(param);

                param = new SqlParameter("@FeeStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.FeeStatus;
                paramList.Add(param);

                param = new SqlParameter("@CreateDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.CreateDate;
                paramList.Add(param);

                param = new SqlParameter("@CreateUser", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.CreateUser;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Fee_Insert", CommandType.StoredProcedure, paramList.ToArray()))
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

        public void Fee_Update(FeeBase fee, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@FeeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.FeeId;
                paramList.Add(param);

                param = new SqlParameter("@FeeTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.FeeTypeId;
                paramList.Add(param);

                param = new SqlParameter("@Amount", SqlDbType.Decimal);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.Amount;
                paramList.Add(param);

                param = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.UserName;
                paramList.Add(param);

                param = new SqlParameter("@Note", SqlDbType.NVarChar, 500);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.Note;
                paramList.Add(param);

                param = new SqlParameter("@FeeStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.FeeStatus;
                paramList.Add(param);

                param = new SqlParameter("@UpdateDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.UpdateDate;
                paramList.Add(param);

                param = new SqlParameter("@UpdateUser", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = fee.UpdateUser;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Fee_Update", CommandType.StoredProcedure, paramList.ToArray()))
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
