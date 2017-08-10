using Inside.InsideData.Base;
using Inside.InsideData.DataAccess;
using System;
using System.Data;

namespace Inside.InsideData.Business
{
    public class OrderManager
    {
        OrderAdapter adapter = new OrderAdapter();

        public DataTable Order_Search(DateTime fromDate, DateTime toDate, int accountTypeId, string accountName, int rowsPerPage, int pageNumber)
        {
            return adapter.Order_Search(fromDate, toDate, accountTypeId, accountName, rowsPerPage, pageNumber);
        }

        public int Order_RowTotal(DateTime fromDate, DateTime toDate, int accountTypeId, string accountName)
        {
            return adapter.Order_RowTotal(fromDate, toDate, accountTypeId, accountName);
        }

        public DataTable Order_GetList(int orderId)
        {
            return adapter.Order_GetList(orderId);
        }

        public void Order_Insert(OrderBase order, ref int code, ref string msg)
        {
            adapter.Order_Insert(order, ref code, ref msg);
        }

        public void Order_Update(OrderBase order, ref int code, ref string msg)
        {
            adapter.Order_Update(order, ref code, ref msg);
        }

        public void Order_Delete(int orderId, ref int code, ref string msg)
        {
            adapter.Order_Delete(orderId, ref code, ref msg);
        }
    }
}
