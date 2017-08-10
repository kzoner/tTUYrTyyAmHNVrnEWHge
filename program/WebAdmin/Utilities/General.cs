using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
namespace InsideGate.WebAdmin.Utilities
{
    public class General
    {
        /// <summary>
        /// ma hoa chuoi input
        /// </summary>
        /// <param name="strInput">du lieu can duoc ma hoa</param>
        /// <param name="strKey">chieu dai bat buoc 16 ki tu</param>
        /// <returns></returns>
        public static string Encript(string strInput, string strKey)
        {
            byte[] key = { };
            byte[] IV = { 0x96, 0x85, 0x74, 0x63, 0x52, 0x41, 0x98, 0x65};
            try
            {
                key = Encoding.UTF8.GetBytes(strKey);
                using (DESCryptoServiceProvider oDESCryptTo = new DESCryptoServiceProvider())
                {
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(strInput);
                    MemoryStream oMemoryStream = new MemoryStream();
                    CryptoStream oCryptoStream = new CryptoStream(oMemoryStream,
                    oDESCryptTo.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                    oCryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                    oCryptoStream.FlushFinalBlock();
                    return Convert.ToBase64String(oMemoryStream.ToArray());
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// giai ma chuoi input
        /// </summary>
        /// <param name="strInput">du lieu can duoc giai ma</param>
        /// <param name="strKey">chieu dai bat buoc 16 ki tu</param>
        /// <returns></returns>
        public static string Decript(string strInput, string strKey)
        {
            byte[] key = { };
            byte[] IV = { 0x96, 0x85, 0x74, 0x63, 0x52, 0x41, 0x98, 0x65 };
            strInput = strInput.Replace(' ', '+');
            byte[] inputByteArray = new byte[strInput.Length];
            try
            {
                key = Encoding.UTF8.GetBytes(strKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strInput);
                MemoryStream oMemoryStream = new MemoryStream();
                CryptoStream oCryptoStream = new CryptoStream(oMemoryStream, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                oCryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                oCryptoStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(oMemoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*

        ''' <summary>
    ''' Đổi string sang Date.
    ''' </summary>
    ''' <param name="input">Chuỗi ngày dạng dd/MM/yyyy</param>
    ''' <returns>Ngày dạng dd/MM/yyyy</returns>
    ''' <remarks></remarks>
    Public Function DateParse(ByVal input As String) As Date
        If String.IsNullOrEmpty(input) Then
            Return Nothing
        End If
        Dim sFormat As New DateTimeFormatInfo
        sFormat.ShortDatePattern = "dd/MM/yyyy"
        Dim output As New Date
        output = Date.Parse(input, sFormat)
        Return output
    End Function*/

        /// <summary>
        /// Convert string strInput with format "strInputPattern" to DateTime
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="strInputPattern">Default is "dd/MM/yyyy"</param>
        /// <returns></returns>
        public static DateTime DateParse(string strInput, string strInputPattern)
        {
            try
            {
                if (string.IsNullOrEmpty(strInput))
                    return DateTime.Now;
                if (string.IsNullOrEmpty(strInputPattern))
                    strInputPattern = "dd/MM/yyyy";
                System.Globalization.DateTimeFormatInfo sFormat = new System.Globalization.DateTimeFormatInfo();
                sFormat.ShortDatePattern = strInputPattern;
                DateTime objDate = new DateTime();
                objDate = DateTime.Parse(strInput, sFormat);
                return objDate;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot parse this value to DateTime", ex);
            }
            

        }

        public static string MoneyToString(decimal Amount)
        {
            try
            {
                string Resp, Tien, DOC, Dem, Nhom, Chu, So1, So2, So3, Dich;
                int S;
                if (Amount == 0)
                {
                    Resp = "Không đồng";
                }
                else
                {
                    if (Amount > 999999999999)
                    {
                        Resp = "Số quá lớn";
                    }
                    else
                    {
                        if (Amount < 0)
                            Resp = "Trừ";
                        else
                        {
                            Resp = string.Empty;
                        }
                        Tien = string.Format("{0:###########0.00}", double.Parse(Amount.ToString()));
                        Tien = (Tien.PadLeft(15));
                        DOC = Dem = string.Empty;
                        DOC = DOC + "trăm  mươi  tỷ    ";
                        DOC = DOC + "trăm  mươi  triệu ";
                        DOC = DOC + "trăm  mươi  ngàn  ";
                        DOC = DOC + "trăm  mươi  đồng  ";
                        DOC = DOC + "trăm  mươi  xu    ";
                        Dem = Dem + "một  hai  ba   bốn  năm  ";
                        Dem = Dem + "sáu  bảy  tám  chín";


                        for (int i = 1; i < 6; i++)
                        {
                            Nhom = Tien.Substring(i * 3 - 3, 3);
                            if (Nhom != "".PadRight(3))
                            {
                                switch (Nhom)
                                {
                                    case "000":
                                        Chu = (i == 4 ? "đồng " : "");
                                        break;
                                    case ".00":
                                        Chu = "chẵn";
                                        break;

                                    default:
                                        So1 = Nhom.Substring(0, 1);
                                        So2 = Nhom.Substring(1, 1);
                                        So3 = Nhom.Substring(Nhom.Length > 1 ? Nhom.Length - 1 : 0, 1);
                                        Chu = "";
                                        for (int j = 1; j < 4; j++)
                                        {
                                            Dich = "";
                                            if (int.TryParse(Nhom.Substring(j - 1, 1), out S) == false)
                                                S = -1;
                                            if (S > 0)
                                            {
                                                Dich = Dem.Substring(S * 5 - 5, 4).Trim() + " ";
                                                Dich += DOC.Substring((i - 1) * 18 + j * 6 - 5 - 1, 5) + " ";
                                            }
                                            switch (j)
                                            {
                                                case 2:
                                                    if (S == 1)
                                                        Dich = "mười ";
                                                    else if (S == 0 && So3 != "0")
                                                        if ((int.Parse(So1) >= 1 && int.Parse(So1) <= 9) ||
                                                            (So1 == "0" && i == 4))
                                                            Dich = "lẻ ";
                                                    break;
                                                case 3:
                                                    if (S == 0 && Nhom != "".PadRight(2) + "0")
                                                        Dich = DOC.Substring((i - 1) * 18 + j * 6 - 5 - 1, 5).Trim() + "".PadRight(1);
                                                    else if (S == 5 && So2 != "".PadRight(1) && So2 != "0")
                                                        Dich = "l" + Dich.Substring(1);
                                                    break;
                                                default:
                                                    break;
                                            }
                                            Chu += Dich;
                                        }
                                        break;

                                }
                                Chu = Chu.Replace("  ", " ");
                                Resp += Chu.Replace("mươi một", "mươi mốt");
                            }


                        }
                    }
                }
                return Resp.Substring(0, 1).ToUpper() + Resp.Substring(1);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string SignData(string OriginData, string PrivKeyPath, string PwdPrivKey)
        {

            try
            {
                SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
                X509Certificate2 x509Cert = new X509Certificate2(PrivKeyPath, PwdPrivKey);
                RSACryptoServiceProvider rsaCryptoIPT = (RSACryptoServiceProvider)x509Cert.PrivateKey;
                Byte[] data = UTF8Encoding.UTF8.GetBytes(OriginData);
                return Convert.ToBase64String(rsaCryptoIPT.SignData(data, sha1));
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetServiceMobile(string Input)
        {
            switch (Input)
            {
                case "098":
                case "097":
                case "0163":
                case "0164":
                case "0165":
                case "0166":
                case "0167":
                case "0168":
                case "0169":
                    return "VIETTEL";
                case "090":
                case "093":
                case "0121":
                case "0122":
                case "0126":
                case "0128":
                case "0120":
                    return "VMS";
                case "091":
                case "094":
                case "0123":
                case "0125":
                case "0127":
                case "0129": 
                case "0124":
                    return "VINAPHONE";
                case "095":
                    return "SFONE";
                case "092":
                case "0188":
                    return "VNMOBILE";
                case "096":
                    return "EVN";
                default:
                    return "";
            }
        }


        public static string RemoveSignUnicode(string InputString)
        {

            string[,] CharacterArray = new string[14, 17];


            Byte i, j;
            string NoSignCharacterArray;
            string Thga, Thge, Thgo, Thgu, Thgi, Thgd, Thgy;
            string HoaA, HoaE, HoaO, HoaU, HoaI, HoaD, HoaY;
            NoSignCharacterArray = "aAeEoOuUiIdDyY";
            Thga = "áàạảãâấầậẩẫăắằặẳẵ";
            HoaA = "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ";
            Thge = "éèẹẻẽêếềệểễeeeeee";
            HoaE = "ÉÈẸẺẼÊẾỀỆỂỄEEEEEE";
            Thgo = "óòọỏõôốồộổỗơớờợởỡ";
            HoaO = "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ";
            Thgu = "úùụủũưứừựửữuuuuuu";
            HoaU = "ÚÙỤỦŨƯỨỪỰỬỮUUUUUU";
            Thgi = "íìịỉĩiiiiiiiiiiii";
            HoaI = "ÍÌỊỈĨIIIIIIIIIIII";
            Thgd = "đdddddddddddddddd";
            HoaD = "ĐDDDDDDDDDDDDDDDD";
            Thgy = "ýỳỵỷỹyyyyyyyyyyyy";
            HoaY = "ÝỲỴỶỸYYYYYYYYYYYY";
            //Nạp vào trong Mảng các ký tự
            //Nạp vào từng đầu hàng các ký tự không dấu
            //Nạp vào cột đầu tiên
            for (i = 0; i < 14; i++)
            {
                CharacterArray[i, 0] = NoSignCharacterArray.Substring(i, 1);
            }


            //Nạp vào từng ô các ký tự có dấu
            //for (j = 1; j < 17; ++j)
            for (i = 1; i < 17; i++)
            {

                CharacterArray[0, i] = Thga.Substring(i, 1); //Nạp từng ký tự trong chuỗi Thga vào từng ô trong hàng 0
                CharacterArray[1, i] = HoaA.Substring(i, 1); //Nạp từng ký tự trong chuỗi HoaA vào từng ô trong  hàng 1
                CharacterArray[2, i] = Thge.Substring(i, 1); //Nạp từng ký tự trong chuỗi Thge vào từng ô trong  hàng 2
                CharacterArray[3, i] = HoaE.Substring(i, 1); //Nạp từng ký tự trong chuỗi HoaE vào từng ô trong  hàng 3
                CharacterArray[4, i] = Thgo.Substring(i, 1); //Nạp từng ký tự trong chuỗi Thgo vào từng ô trong  hàng 4
                CharacterArray[5, i] = HoaO.Substring(i, 1); //Nạp từng ký tự trong chuỗi HoaO vào từng ô trong  hàng 5
                CharacterArray[6, i] = Thgu.Substring(i, 1); //Nạp từng ký tự trong chuỗi Thgu vào từng ô trong  hàng 6
                CharacterArray[7, i] = HoaU.Substring(i, 1); //Nạp từng ký tự trong chuỗi HoaU vào từng ô trong  hàng 7
                CharacterArray[8, i] = Thgi.Substring(i, 1); //Nạp từng ký tự trong chuỗi Thgi vào từng ô trong  hàng 8
                CharacterArray[9, i] = HoaI.Substring(i, 1);//Nạp từng ký tự trong chuỗi HoaI vào từng ô trong  hàng 9
                CharacterArray[10, i] = Thgd.Substring(i, 1); //Nạp từng ký tự trong chuỗi Thgd vào từng ô trong  hàng 10
                CharacterArray[11, i] = HoaD.Substring(i, 1);//Nạp từng ký tự trong chuỗi HoaD vào từng ô trong  hàng 11
                CharacterArray[12, i] = Thgy.Substring(i, 1); //Nạp từng ký tự trong chuỗi Thgy vào từng ô trong  hàng 12
                CharacterArray[13, i] = HoaY.Substring(i, 1); //Nạp từng ký tự trong chuỗi HoaY vào từng ô trong  hàng 13
            }

            string StrTemp1 = InputString;
            string StrTemp2 = "";

            for (j = 0; j < 14; j++)
            {
                for (i = 0; i < 17; i++)
                {
                    StrTemp2 = StrTemp1.Replace(CharacterArray[j, i], CharacterArray[j, 0]);
                    StrTemp1 = StrTemp2;
                }
            }

            StrTemp1 = StrTemp1.Replace("?", "");
            StrTemp1 = StrTemp1.Replace("'", "");
            StrTemp1 = StrTemp1.Replace("\"", "");
            StrTemp1 = StrTemp1.Replace("!", "");
            StrTemp1 = StrTemp1.Replace(":", "");
            StrTemp1 = StrTemp1.Replace("[", "");
            StrTemp1 = StrTemp1.Replace("/", "-");
            StrTemp1 = StrTemp1.Replace("]", "");
            StrTemp1 = StrTemp1.Replace(".", "");
            StrTemp1 = StrTemp1.Replace(",", "");
            StrTemp1 = StrTemp1.Replace("(", "");
            StrTemp1 = StrTemp1.Replace(")", "");
            StrTemp1 = StrTemp1.Replace("&", "");
            StrTemp1 = StrTemp1.Replace("{", "");
            StrTemp1 = StrTemp1.Replace("}", "");
            return StrTemp1;
        }

    }
}
