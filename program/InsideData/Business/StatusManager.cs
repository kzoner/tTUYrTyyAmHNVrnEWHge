using Inside.InsideData.DataAccess;
using System.Data;

namespace Inside.InsideData.Business
{
    public class StatusManager
    {
        StatusAdapter adapter = new StatusAdapter();

        public DataTable Status_GetList()
        {
            return adapter.Status_GetList(0);
        }

        public string Status_GetName(int statusId)
        {
            string result = "";
            DataTable dt = adapter.Status_GetList(statusId);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = dr["StatusName"].ToString();
            }
            return result;
        }
    }
}
