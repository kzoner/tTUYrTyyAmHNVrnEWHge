using System;
using System.Data;
using System.Data.SqlClient;

using Inside.SecurityProviders.DataAccess;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders
{
    public class UserManager
    {
        private UserAdapter m_userAdapter = null;

        private UserAdapter Adapter
        {
            get
            {
                if (m_userAdapter == null)
                {
                    m_userAdapter = new UserAdapter();
                }

                return m_userAdapter;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="fullname"></param>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public User CreateUser(string username, string password, string fullname, string email, bool blocked)
        {
            try
            {
                // validate input parameters
                if (string.IsNullOrEmpty(username.Trim())) throw new SecurityException("Create user fail. Username can not blank");
                if (string.IsNullOrEmpty(password.Trim())) throw new SecurityException("Create user fail. Password can not blank");
                if (string.IsNullOrEmpty(fullname.Trim())) throw new SecurityException("Create user fail. FullName can not blank");
                if (DataChecker.IsValidEmail(email) == false) throw new SecurityException("Create user fail. Invalid email address");
                if (DataChecker.IsValidPassword(password) == false) throw new SecurityException("Create user fail. Invalid password");

                // create user
                User user = Adapter.Create(username, password, fullname, email, blocked);

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.FullName.Trim())) throw new SecurityException("Create user fail. FullName can not blank");
                if (DataChecker.IsValidEmail(user.Email) == false) throw new SecurityException("Create user fail. Invalid email address");

                Adapter.Update(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void Remove(User user)
        {
            try
            {
                Adapter.Remove(user.UserName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Xoa User
        /// </summary>
        /// <param name="username">UserName can xoa</param>
        public void Remove(string username)
        {
            try
            {
                Adapter.Remove(username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void BlockUser(string username)
        {
            try
            {
                Adapter.BlockUser(username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>W
        public void UnblockUser(string username)
        {
            try
            {
                Adapter.UnblockUser(username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        public void ChangePassword(string username, string currentPassword, string newPassword)
        {
            try
            {
                // verify password
                if (!DataChecker.IsValidPassword(currentPassword)) throw new SecurityException("Wrong password");
                if (!DataChecker.IsValidPassword(newPassword)) throw new SecurityException("Invalid new password");
                if (!DataChecker.IsValidPassword(currentPassword)) throw new SecurityException("Mật khẩu không hợp lệ.");
                if (!DataChecker.IsValidPassword(newPassword)) throw new SecurityException("Mật khẩu mới không hợp lệ.");
                Adapter.ChangePassword(username, currentPassword, newPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public string RecoveryPassword(string username)
        {
            try
            {
                return Adapter.RecoveryPassword(username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void SetPassword(string username, string password)
        {
            try
            {
                Adapter.SetPassword(username, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateUser(string username, string password)
        {
            try
            {
                return Adapter.ValidateUser(username, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUser(string username)
        {
            try
            {
                User user = Adapter.GetUser(username);

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UserCollection GetAllUsers()
        {
            try
            {
                UserCollection collection = Adapter.GetAllUsers();

                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Tim User bang email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserCollection FindUsersByEmail(string email)
        {
            try
            {
                return Adapter.FindUsersByEmail(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Tim User bang UserName
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserCollection FindUsersByUserName(string userName)
        {
            try
            {
                return Adapter.FindUsersByUserName(userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Tim User bang FullName
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserCollection FindUsersByFullName(string fullName)
        {
            try
            {
                return Adapter.FindUsersByFullName(fullName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public UserCollection GetUsersInRole(int roleID)
        {
            return  Adapter.GetUsersInRole(roleID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static User Populate(IDataReader dr)
        {
            try
            {
                User user = new User(
                      dr["UserName"].ToString()
                    , dr["FullName"].ToString()
                    , dr["Email"].ToString()
                    , dr.GetBoolean(dr.GetOrdinal("Bblocked"))
                    , dr.GetDateTime(dr.GetOrdinal("BlockedDate"))
                    , dr.GetDateTime(dr.GetOrdinal("CreatedTime"))
                    , dr.GetDateTime(dr.GetOrdinal("LastLogin"))
                    , dr.GetString(dr.GetOrdinal("LastIP")));

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check login for user
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="IP"></param>
        /// <returns></returns>
        public GlobalEnum.LoginStatus CheckLogin(string UserName, string Password, string IP)
        {
            return Adapter.CheckLogin(UserName, Password, IP);
        }

        /// <summary>
        /// Set permission for user
        /// </summary>
        /// <param name="strUserName"></param>
        /// <param name="iResourceID"></param>
        /// <param name="strDataPermission"></param>
        /// <returns></returns>
        public ReturnObject SetPermission(string strUserName, int iResourceID, string strDataPermission)
        {
            return Adapter.SetPermission(strUserName, iResourceID, strDataPermission);
        }


        public DataTable CheckAuthorization(string strUserName, string strToken)
        {
            return Adapter.CheckAuthorization(strUserName, strToken);
        }

    }
}
