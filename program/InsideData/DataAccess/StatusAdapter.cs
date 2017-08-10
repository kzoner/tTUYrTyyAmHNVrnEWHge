using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inside.InsideData.DataAccess
{
    public class StatusAdapter: GeneralDataAdapter
    {
        public DataTable Status_GetList(int statusId)
        {
            DataTable dt = new DataTable("dt");
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter param;

                param = new SqlParameter("@StatusId", SqlDbType.Int, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = statusId;
                paramList.Add(param);

                using (SqlDataReader dr = (SqlDataReader)Database.ExecuteReader("usp_Status_GetList", CommandType.StoredProcedure, paramList.ToArray()))
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
