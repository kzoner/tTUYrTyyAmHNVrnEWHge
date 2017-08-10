using System;
using System.Text.RegularExpressions;

namespace Inside.SecurityProviders
{
    class DataChecker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            bool ReturnData = false;
            Regex objRegex;
            Match matchResult;
            try
            {
                objRegex = new Regex("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$");
                matchResult = objRegex.Match(email);
                if (matchResult.Success)
                {
                    ReturnData = true;
                }
                else
                {
                    ReturnData = false;
                }
            }
            catch (Exception)
            {

                ReturnData = false;
            }
            return ReturnData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringNumber"></param>
        /// <returns></returns>
        public static bool IsNumeric(string number)
        {
            bool ReturnData = false;
            Regex objRegex;
            Match matchResult;
            try
            {
                objRegex = new Regex("^\\d+$");
                matchResult = objRegex.Match(number);
                if (matchResult.Success)
                {
                    ReturnData = true;
                }
                else
                {
                    ReturnData = false;
                }
            }
            catch (Exception)
            {

                ReturnData = false;
            }
            return ReturnData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIPAddress(string ip)
        {
            bool ReturnData = false;
            Regex objRegex;
            Match matchResult;
            try
            {
                objRegex = new Regex("^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
                matchResult = objRegex.Match(ip);
                if (matchResult.Success)
                {
                    ReturnData = true;
                }
                else
                {
                    ReturnData = false;
                }
            }
            catch (Exception)
            {

                ReturnData = false;
            }
            return ReturnData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string password)
        {
            return true;
        }
    }
}
