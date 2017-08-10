using Inside.InsideData.Base;
using Inside.InsideData.DataAccess;
using System.Data;

namespace Inside.InsideData.Business
{
    public class OrderDetailManager
    {
        OrderDetailAdapter adapter = new OrderDetailAdapter();

        public DataTable OrderDetail_GetList_OrderId(int orderId)
        {
            return adapter.OrderDetail_GetList_OrderId(orderId);
        }

        public void OrderDetail_Insert(OrderDetailBase orderDetailBase, ref int code, ref string msg)
        {
            adapter.OrderDetail_Insert(orderDetailBase, ref code, ref msg);
        }

        public void OrderDetail_Delete_OrderId(int orderId, ref int code, ref string msg)
        {
            adapter.OrderDetail_Delete_OrderId(orderId, ref code, ref msg);
        }
    }
}
