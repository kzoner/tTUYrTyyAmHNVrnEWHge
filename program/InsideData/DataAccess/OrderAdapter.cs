using Inside.InsideData.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inside.InsideData.DataAccess
{
    public class OrderAdapter : GeneralDataAdapter
    {
        public int Order_RowTotal(DateTime fromDate, DateTime toDate, int accountTypeId, string accountName)
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

                param = new SqlParameter("@AccountTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountTypeId;
                paramList.Add(param);

                param = new SqlParameter("@AccountName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = accountName;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Order_RowTotal", CommandType.StoredProcedure, paramList.ToArray()))
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

        public DataTable Order_Search(DateTime fromDate, DateTime toDate, int accountTypeId, string accountName, int rowsPerPage, int pageNumber)
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

                param = new SqlParameter("@AccountTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = accountTypeId;
                paramList.Add(param);

                param = new SqlParameter("@AccountName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = accountName;
                paramList.Add(param);

                param = new SqlParameter("@RowsPerPage", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = rowsPerPage;
                paramList.Add(param);

                param = new SqlParameter("@PageNumber", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = pageNumber;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Order_Search", CommandType.StoredProcedure, paramList.ToArray()))
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

        public DataTable Order_GetList(int orderId)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@OrderId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = orderId;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Order_GetList", CommandType.StoredProcedure, paramList.ToArray()))
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

        public void Order_Insert(OrderBase order, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@AccountTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = order.AccountTypeId;
                paramList.Add(param);

                param = new SqlParameter("@AccountId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = order.AccountId;
                paramList.Add(param);

                param = new SqlParameter("@OrderCode", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = order.OrderCode;
                paramList.Add(param);

                param = new SqlParameter("@TransportFee", SqlDbType.Money);
                param.Direction = ParameterDirection.Input;
                param.Value = order.TransportFee;
                paramList.Add(param);

                param = new SqlParameter("@Note", SqlDbType.NVarChar, 500);
                param.Direction = ParameterDirection.Input;
                param.Value = order.Note;
                paramList.Add(param);

                param = new SqlParameter("@CreateDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = order.CreateDate;
                paramList.Add(param);

                param = new SqlParameter("@CreateUser", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = order.CreateUser;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Order_Insert", CommandType.StoredProcedure, paramList.ToArray()))
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

        public void Order_Update(OrderBase order, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@OrderId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = order.OrderId;
                paramList.Add(param);

                param = new SqlParameter("@AccountTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = order.AccountTypeId;
                paramList.Add(param);

                param = new SqlParameter("@AccountId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = order.AccountId;
                paramList.Add(param);

                param = new SqlParameter("@OrderCode", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = order.OrderCode;
                paramList.Add(param);

                param = new SqlParameter("@TransportFee", SqlDbType.Money);
                param.Direction = ParameterDirection.Input;
                param.Value = order.TransportFee;
                paramList.Add(param);

                param = new SqlParameter("@Note", SqlDbType.NVarChar, 500);
                param.Direction = ParameterDirection.Input;
                param.Value = order.Note;
                paramList.Add(param);

                param = new SqlParameter("@UpdateDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = order.UpdateDate;
                paramList.Add(param);

                param = new SqlParameter("@UpdateUser", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = order.UpdateUser;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Order_Update", CommandType.StoredProcedure, paramList.ToArray()))
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

        public void Order_Delete(int orderId, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@OrderId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = orderId;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Order_Delete", CommandType.StoredProcedure, paramList.ToArray()))
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
