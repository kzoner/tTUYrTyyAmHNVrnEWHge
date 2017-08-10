using Inside.InsideData.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inside.InsideData.DataAccess
{
    public class ProductAdapter : GeneralDataAdapter
    {
        public DataTable ProductType_GetList(int productTypeId, int productTypeStatus)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@ProductTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = productTypeId;
                paramList.Add(param);

                param = new SqlParameter("@ProductTypeStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = productTypeStatus;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_ProductType_GetList", CommandType.StoredProcedure, paramList.ToArray()))
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

        public int Product_RowTotal(int productTypeId, string productCode, string productName, int productStatus)
        {
            DataTable dt = new DataTable("dt");
            int rowTotal = 0;
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@ProductTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = productTypeId;
                paramList.Add(param);

                param = new SqlParameter("@ProductCode", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = productCode;
                paramList.Add(param);

                param = new SqlParameter("@ProductName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = productName;
                paramList.Add(param);

                param = new SqlParameter("@ProductStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = productStatus;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Product_RowTotal", CommandType.StoredProcedure, paramList.ToArray()))
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

        public DataTable Product_Search(int productTypeId, string productCode, string productName, int productStatus, int rowsPerPage, int pageNumber)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@ProductTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = productTypeId;
                paramList.Add(param);

                param = new SqlParameter("@ProductCode", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = productCode;
                paramList.Add(param);

                param = new SqlParameter("@ProductName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = productName;
                paramList.Add(param);

                param = new SqlParameter("@ProductStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = productStatus;
                paramList.Add(param);

                param = new SqlParameter("@RowsPerPage", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = rowsPerPage;
                paramList.Add(param);

                param = new SqlParameter("@PageNumber", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = pageNumber;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Product_Search", CommandType.StoredProcedure, paramList.ToArray()))
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

        public DataTable Product_GetList(int productId)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@ProductId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = productId;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Product_GetList", CommandType.StoredProcedure, paramList.ToArray()))
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

        public DataTable Product_GetList_ProductType(int productId, int productTypeId, int ProductStatus)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@ProductId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = productId;
                paramList.Add(param);

                param = new SqlParameter("@ProductTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = productTypeId;
                paramList.Add(param);

                param = new SqlParameter("@ProductStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = ProductStatus;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Product_GetList_ProductType", CommandType.StoredProcedure, paramList.ToArray()))
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

        public void Product_Insert(ProductBase product, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@ProductTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ProductTypeId;
                paramList.Add(param);

                param = new SqlParameter("@ProductCode", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ProductCode;
                paramList.Add(param);

                param = new SqlParameter("@ProductName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ProductName;
                paramList.Add(param);

                param = new SqlParameter("@Price", SqlDbType.Decimal);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Price;
                paramList.Add(param);

                param = new SqlParameter("@Length", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Length;
                paramList.Add(param);

                param = new SqlParameter("@Width", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Width;
                paramList.Add(param);

                param = new SqlParameter("@Depth", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Depth;
                paramList.Add(param);

                param = new SqlParameter("@Height", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Height;
                paramList.Add(param);

                param = new SqlParameter("@Weigh", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Weigh;
                paramList.Add(param);

                param = new SqlParameter("@UnitTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = product.UnitTypeId;
                paramList.Add(param);

                param = new SqlParameter("@UnitId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = product.UnitId;
                paramList.Add(param);

                param = new SqlParameter("@UnitValue", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = product.UnitValue;
                paramList.Add(param);

                param = new SqlParameter("@ImageName", SqlDbType.NVarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ImageName;
                paramList.Add(param);

                param = new SqlParameter("@ImagePath", SqlDbType.NVarChar, 200);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ImagePath;
                paramList.Add(param);

                param = new SqlParameter("@Note", SqlDbType.NVarChar, 500);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Note;
                paramList.Add(param);

                param = new SqlParameter("@ProductStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ProductStatus;
                paramList.Add(param);

                param = new SqlParameter("@CreateDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = product.CreateDate;
                paramList.Add(param);

                param = new SqlParameter("@CreateUser", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = product.CreateUser;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Product_Insert", CommandType.StoredProcedure, paramList.ToArray()))
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

        public void Product_Update(ProductBase product, ref int code, ref string msg)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@ProductId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ProductId;
                paramList.Add(param);

                param = new SqlParameter("@ProductTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ProductTypeId;
                paramList.Add(param);

                param = new SqlParameter("@ProductCode", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ProductCode;
                paramList.Add(param);

                param = new SqlParameter("@ProductName", SqlDbType.NVarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ProductName;
                paramList.Add(param);

                param = new SqlParameter("@Price", SqlDbType.Decimal);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Price;
                paramList.Add(param);

                param = new SqlParameter("@Length", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Length;
                paramList.Add(param);

                param = new SqlParameter("@Width", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Width;
                paramList.Add(param);

                param = new SqlParameter("@Depth", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Depth;
                paramList.Add(param);

                param = new SqlParameter("@Height", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Height;
                paramList.Add(param);

                param = new SqlParameter("@Weigh", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Weigh;
                paramList.Add(param);

                param = new SqlParameter("@UnitTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = product.UnitTypeId;
                paramList.Add(param);

                param = new SqlParameter("@UnitId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = product.UnitId;
                paramList.Add(param);

                param = new SqlParameter("@UnitValue", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = product.UnitValue;
                paramList.Add(param);

                param = new SqlParameter("@ImageName", SqlDbType.NVarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ImageName;
                paramList.Add(param);

                param = new SqlParameter("@ImagePath", SqlDbType.NVarChar, 200);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ImagePath;
                paramList.Add(param);

                param = new SqlParameter("@Note", SqlDbType.NVarChar, 500);
                param.Direction = ParameterDirection.Input;
                param.Value = product.Note;
                paramList.Add(param);

                param = new SqlParameter("@ProductStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = product.ProductStatus;
                paramList.Add(param);

                param = new SqlParameter("@UpdateDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = product.UpdateDate;
                paramList.Add(param);

                param = new SqlParameter("@UpdateUser", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.Input;
                param.Value = product.UpdateUser;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Product_Update", CommandType.StoredProcedure, paramList.ToArray()))
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
