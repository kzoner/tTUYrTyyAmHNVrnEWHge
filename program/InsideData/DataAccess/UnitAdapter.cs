using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inside.InsideData.DataAccess
{
    public class UnitAdapter : GeneralDataAdapter
    {
        public DataTable UnitType_GetList(int unitTypeId, int productTypeId, int unitTypeStatus)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@UnitTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = unitTypeId;
                paramList.Add(param);

                param = new SqlParameter("@ProductTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = productTypeId;
                paramList.Add(param);

                param = new SqlParameter("@UnitTypeStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = unitTypeStatus;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_UnitType_GetList", CommandType.StoredProcedure, paramList.ToArray()))
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

        public DataTable Unit_GetList(int unitId, int unitTypeId, int unitStatus)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@UnitId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = unitId;
                paramList.Add(param);

                param = new SqlParameter("@UnitTypeId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = unitTypeId;
                paramList.Add(param);

                param = new SqlParameter("@UnitStatus", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = unitStatus;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Unit_GetList", CommandType.StoredProcedure, paramList.ToArray()))
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
    }
}
