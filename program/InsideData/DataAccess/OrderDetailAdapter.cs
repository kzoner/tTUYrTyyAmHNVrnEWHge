using Inside.InsideData.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inside.InsideData.DataAccess
{
    public class OrderDetailAdapter : GeneralDataAdapter
    {
        public DataTable OrderDetail_GetList_OrderId(int orderId)
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

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_OrderDetail_GetList_OrderId", CommandType.StoredProcedure, paramList.ToArray()))
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

        public void OrderDetail_Insert(OrderDetailBase orderDetailBase, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@OrderId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = orderDetailBase.OrderId;
                paramList.Add(param);

                param = new SqlParameter("@ProductId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = orderDetailBase.ProductId;
                paramList.Add(param);

                param = new SqlParameter("@UnitTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = orderDetailBase.UnitTypeId;
                paramList.Add(param);

                param = new SqlParameter("@UnitId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = orderDetailBase.UnitId;
                paramList.Add(param);

                param = new SqlParameter("@UnitValue", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = orderDetailBase.UnitValue;
                paramList.Add(param);

                param = new SqlParameter("@Quantity", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = orderDetailBase.Quantity;
                paramList.Add(param);

                param = new SqlParameter("@Price", SqlDbType.Decimal);
                param.Direction = ParameterDirection.Input;
                param.Value = orderDetailBase.Price;
                paramList.Add(param);

                param = new SqlParameter("@Amount", SqlDbType.Decimal);
                param.Direction = ParameterDirection.Input;
                param.Value = orderDetailBase.Amount;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_OrderDetail_Insert", CommandType.StoredProcedure, paramList.ToArray()))
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

        public void OrderDetail_Delete_OrderId(int orderId, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@OrderId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = orderId;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_OrderDetail_Delete_OrderId", CommandType.StoredProcedure, paramList.ToArray()))
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
