using Inside.InsideData.Base;
using Inside.InsideData.DataAccess;
using System.Data;

namespace Inside.InsideData.Business
{
    public class AccountManager
    {
        AccountAdapter adapter = new AccountAdapter();

        public DataTable AccountLevel_GetList(int accountTypeId)
        {
            return adapter.AccountLevel_GetList(0, accountTypeId, 1);
        }

        public string AccountLevel_GetName(int accountLevelId)
        {
            string result = accountLevelId.ToString();
            DataTable dt = adapter.AccountLevel_GetList(accountLevelId, 0, 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = dr["AccountLevelName"].ToString();
            }
            return result;
        }

        public DataTable AccountType_GetList()
        {
            return adapter.AccountType_GetList(0, 1);
        }

        public string AccountType_GetName(int accountTypeId)
        {
            string result = accountTypeId.ToString();
            DataTable dt = adapter.AccountType_GetList(accountTypeId, 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = dr["AccountTypeName"].ToString();
            }
            return result;
        }

        public string OrderType_GetName(int accountTypeId)
        {
            string result = accountTypeId.ToString();
            DataTable dt = adapter.AccountType_GetList(accountTypeId, 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = dr["OrderTypeName"].ToString();
            }
            return result;
        }

        public int Account_RowTotal(int accountTypeId, string accountName, int accountLevelId, int accountStatus)
        {
            return adapter.Account_RowTotal(accountTypeId, accountName, accountLevelId, accountStatus);
        }

        public DataTable Account_Search(int accountTypeId, string accountName, int accountLevelId, int accountStatus, int rowsPerPage, int pageNumber)
        {
            return adapter.Account_Search(accountTypeId, accountName, accountLevelId, accountStatus, rowsPerPage, pageNumber);
        }

        public DataTable Account_GetList(int accountId)
        {
            return adapter.Account_GetList(accountId);
        }

        public DataTable Account_GetList_AccountType(int accountTypeId)
        {
            return adapter.Account_GetList_AccountType(0, accountTypeId, 1);
        }

        public string Account_GetName(int accountId)
        {
            string result = accountId.ToString();
            DataTable dt = adapter.Account_GetList_AccountType(accountId, 0, 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = dr["AccountName"].ToString();
            }
            return result;
        }

        public void Account_Insert(AccountBase account, ref int code, ref string msg)
        {
            adapter.Account_Insert(account, ref code, ref msg);
        }

        public void Account_Update(AccountBase account, ref int code, ref string msg)
        {
            adapter.Account_Update(account, ref code, ref msg);
        }
    }
}
