using System;

namespace Inside.InsideData.Base
{
    public class FeeBase
    {
        private int m_FeeId;
        private int m_FeeTypeId;
        private decimal m_Amount;
        private string m_UserName;
        private string m_Note;
        private int m_FeeStatus;
        private DateTime m_CreateDate;
        private string m_CreateUser;
        private DateTime m_UpdateDate;
        private string m_UpdateUser;

        public int FeeId { get => m_FeeId; set => m_FeeId = value; }
        public int FeeTypeId { get => m_FeeTypeId; set => m_FeeTypeId = value; }
        public decimal Amount { get => m_Amount; set => m_Amount = value; }
        public string UserName { get => m_UserName; set => m_UserName = value; }
        public string Note { get => m_Note; set => m_Note = value; }
        public int FeeStatus { get => m_FeeStatus; set => m_FeeStatus = value; }
        public DateTime CreateDate { get => m_CreateDate; set => m_CreateDate = value; }
        public string CreateUser { get => m_CreateUser; set => m_CreateUser = value; }
        public DateTime UpdateDate { get => m_UpdateDate; set => m_UpdateDate = value; }
        public string UpdateUser { get => m_UpdateUser; set => m_UpdateUser = value; }
    }
}
