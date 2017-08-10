using Inside.InsideData.DataAccess;
using System.Data;

namespace Inside.InsideData.Business
{
    public class UnitManager
    {
        UnitAdapter adapter = new UnitAdapter();

        public DataTable UnitType_GetList(int productTypeId)
        {
            return adapter.UnitType_GetList(0, productTypeId, 1);
        }

        public string UnitType_GetName(int unitTypeId)
        {
            string result = unitTypeId.ToString();
            DataTable dt = adapter.UnitType_GetList(unitTypeId, 0, 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = dr["UnitTypeName"].ToString();
            }
            return result;
        }

        public DataTable Unit_GetList(int unitTypeId)
        {
            return adapter.Unit_GetList(0, unitTypeId, 1);
        }

        public string Unit_GetName(int unitId)
        {
            string result = unitId.ToString();
            DataTable dt = adapter.Unit_GetList(unitId, 0, 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = dr["UnitName"].ToString();
            }
            return result;
        }
    }
}
