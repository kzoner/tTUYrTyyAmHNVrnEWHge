using System;

namespace Inside.InsideData.Base
{
    public class AccountBase
    {
        private int m_AccountId;
        private int m_AccountTypeId;
        private string m_AccountName;
        private string m_AccountShortName;
        private int m_AccountLevelId;
        private string m_ContactName;
        private string m_Address;
        private string m_PhoneNumber1;
        private string m_PhoneNumber2;
        private string m_PhoneNumber3;
        private string m_Email;
        private string m_Website;
        private string m_Note;
        private int m_AccountStatus;
        private DateTime m_CreateDate;
        private string m_CreateUser;
        private DateTime m_UpdateDate;
        private string m_UpdateUser;

        public int AccountId { get => m_AccountId; set => m_AccountId = value; }
        public int AccountTypeId { get => m_AccountTypeId; set => m_AccountTypeId = value; }
        public string AccountName { get => m_AccountName; set => m_AccountName = value; }
        public string AccountShortName { get => m_AccountShortName; set => m_AccountShortName = value; }
        public int AccountLevelId { get => m_AccountLevelId; set => m_AccountLevelId = value; }
        public string ContactName { get => m_ContactName; set => m_ContactName = value; }
        public string Address { get => m_Address; set => m_Address = value; }
        public string PhoneNumber1 { get => m_PhoneNumber1; set => m_PhoneNumber1 = value; }
        public string PhoneNumber2 { get => m_PhoneNumber2; set => m_PhoneNumber2 = value; }
        public string PhoneNumber3 { get => m_PhoneNumber3; set => m_PhoneNumber3 = value; }
        public string Email { get => m_Email; set => m_Email = value; }
        public string Website { get => m_Website; set => m_Website = value; }
        public string Note { get => m_Note; set => m_Note = value; }
        public int AccountStatus { get => m_AccountStatus; set => m_AccountStatus = value; }
        public DateTime CreateDate { get => m_CreateDate; set => m_CreateDate = value; }
        public string CreateUser { get => m_CreateUser; set => m_CreateUser = value; }
        public DateTime UpdateDate { get => m_UpdateDate; set => m_UpdateDate = value; }
        public string UpdateUser { get => m_UpdateUser; set => m_UpdateUser = value; }
    }
}
