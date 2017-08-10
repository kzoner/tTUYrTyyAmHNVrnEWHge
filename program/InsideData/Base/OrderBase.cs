using System;

namespace Inside.InsideData.Base
{
    public class OrderBase
    {
        private int m_OrderId;
        private int m_AccountTypeId;
        private int m_AccountId;
        private string m_OrderCode;
        private decimal m_TransportFee;
        private string m_Note;
        private DateTime m_CreateDate;
        private string m_CreateUser;
        private DateTime m_UpdateDate;
        private string m_UpdateUser;

        public int OrderId { get => m_OrderId; set => m_OrderId = value; }
        public int AccountTypeId { get => m_AccountTypeId; set => m_AccountTypeId = value; }
        public int AccountId { get => m_AccountId; set => m_AccountId = value; }
        public string OrderCode { get => m_OrderCode; set => m_OrderCode = value; }
        public decimal TransportFee { get => m_TransportFee; set => m_TransportFee = value; }
        public string Note { get => m_Note; set => m_Note = value; }
        public DateTime CreateDate { get => m_CreateDate; set => m_CreateDate = value; }
        public string CreateUser { get => m_CreateUser; set => m_CreateUser = value; }
        public DateTime UpdateDate { get => m_UpdateDate; set => m_UpdateDate = value; }
        public string UpdateUser { get => m_UpdateUser; set => m_UpdateUser = value; }
    }
}
