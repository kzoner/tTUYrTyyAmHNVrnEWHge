using System;

namespace Inside.InsideData.Base
{
    public class OrderDetailBase
    {
        private int m_OrderDetailId;
        private int m_OrderId;
        private int m_ProductId;
        private int m_UnitTypeId;
        private int m_UnitId;
        private float m_UnitValue;
        private int m_Quantity;
        private decimal m_Price;
        private decimal m_Amount;

        public int OrderDetailId { get => m_OrderDetailId; set => m_OrderDetailId = value; }
        public int OrderId { get => m_OrderId; set => m_OrderId = value; }
        public int ProductId { get => m_ProductId; set => m_ProductId = value; }
        public int UnitTypeId { get => m_UnitTypeId; set => m_UnitTypeId = value; }
        public int UnitId { get => m_UnitId; set => m_UnitId = value; }
        public float UnitValue { get => m_UnitValue; set => m_UnitValue = value; }
        public int Quantity { get => m_Quantity; set => m_Quantity = value; }
        public decimal Price { get => m_Price; set => m_Price = value; }
        public decimal Amount { get => m_Amount; set => m_Amount = value; }
    }
}
