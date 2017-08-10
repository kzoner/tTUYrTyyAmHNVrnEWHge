using Inside.InsideData.Base;
using Inside.InsideData.DataAccess;
using System;
using System.Data;

namespace Inside.InsideData.Business
{
    public class FeeManager
    {
        FeeAdapter adapter = new FeeAdapter();

        public DataTable FeeType_GetList()
        {
            return adapter.FeeType_GetList(0, 1);
        }

        public string FeeType_GetName(int feeTypeId)
        {
            string result = feeTypeId.ToString();
            DataTable dt = adapter.FeeType_GetList(feeTypeId, 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = dr["FeeTypeName"].ToString();
            }
            return result;
        }

        public DataTable FeeType_GetList_FeeTypeId(int feeTypeId)
        {
            return adapter.FeeType_GetList(feeTypeId, 0);
        }

        public int FeeType_RowTotal(string feeTypeName)
        {
            return adapter.FeeType_RowTotal(feeTypeName);
        }

        public DataTable FeeType_Search(string feeTypeName, int rowsPerPage, int pageNumber)
        {
            return adapter.FeeType_Search(feeTypeName, rowsPerPage, pageNumber);
        }

        public void FeeType_Insert(string feeTypeName, ref int code, ref string msg)
        {
            adapter.FeeType_Insert(feeTypeName, ref code, ref msg);
        }

        public void FeeType_Update(int feeTypeId, string feeTypeName, int feeTypeStatus, ref int code, ref string msg)
        {
            adapter.FeeType_Update(feeTypeId, feeTypeName, feeTypeStatus, ref code, ref msg);
        }

        public int Fee_RowTotal(DateTime fromDate, DateTime toDate, int feeTypeId)
        {
            return adapter.Fee_RowTotal(fromDate, toDate, feeTypeId);
        }

        public DataTable Fee_Search(DateTime fromDate, DateTime toDate, int feeTypeId, int rowsPerPage, int pageNumber)
        {
            return adapter.Fee_Search(fromDate, toDate, feeTypeId, rowsPerPage, pageNumber);
        }

        public DataTable Fee_GetList(int feeId)
        {
            return adapter.Fee_GetList(feeId);
        }

        public void Fee_Insert(FeeBase fee, ref int code, ref string msg)
        {
            adapter.Fee_Insert(fee, ref code, ref msg);
        }

        public void Fee_Update(FeeBase fee, ref int code, ref string msg)
        {
            adapter.Fee_Update(fee, ref code, ref msg);
        }
    }
}
