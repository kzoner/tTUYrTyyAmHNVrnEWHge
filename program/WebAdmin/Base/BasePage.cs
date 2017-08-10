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
using System.Globalization;
using System.Text;
using System.IO;
using Inside.SecurityProviders;
using OfficeOpenXml;

namespace WebAdmin.Base
{
    public class BasePage : System.Web.UI.Page
    {
        protected const string TienIchSecretPass = "2d666D344d9D4B2P901589F9ZDA6359B";

        #region Members
        private Permission m_Permission;
        protected string Token = string.Empty;
        private bool requestAuthen = true;
        #endregion

        #region Properties
        public Permission Permission
        {
            get
            {
                if (m_Permission == null)
                    m_Permission = new Permission();
                return m_Permission;
            }
            set
            {
                this.m_Permission = value;
            }
        }


        /// <summary>
        /// Thuộc tính này cho phép một form yêu cầu phải xác thực quyền vào hay không
        /// Ví dụ RequestAuthen = false : form này sẽ luôn luôn được cho User vào
        /// Thuộc tính này dùng để mở quyền một form mà không cần tạo resource và phân quyền cho form đó
        /// </summary>
        public bool RequestAuthen
        {
            get
            {
                return requestAuthen;
            }
            set
            {
                this.requestAuthen = value;
            }
        }

        #endregion


        private void LoadPermission()
        {
            m_Permission = new Permission();
            //neu la man hinh welcome + menu + Home thi khong check quyen, mac dinh user dang nhap thanh cong se duoc xem
            if (this.Token == "12F3EBC0-A61A-4B2A-849A-7E9A8CF7F3F1" ||
                this.Token == "4EFE355D-A1A7-46AA-BAA8-6CCCB4F62179" ||
                this.Token == "5CCC1AA0-BCD8-4744-9DE3-0F9B7550C088" ||
                this.Token == "D154AF769FD446EABDB724FAB0D02DBF" ||
                this.Token == "GENERATE-TOKEN-PAGE" ||
                !this.requestAuthen)
            {
                m_Permission.IsAllowedView = true;
                return;
            }

            UserManager usrMan = new UserManager();
            DataTable dtPermissionList = new DataTable("PermissionList");
            dtPermissionList = usrMan.CheckAuthorization(this.Page.User.Identity.Name, this.Token);
            if (dtPermissionList == null || dtPermissionList.Rows.Count == 0)
            {
                //if (this.PreviousPage == null)
                Response.Redirect("~/Home.aspx", true);
                //else
                //    Response.Redirect(this.PreviousPage.Request.Url.ToString());


            }
            else
            {
                foreach (DataRow row in dtPermissionList.Rows)
                {
                    switch (int.Parse(row["OperationCode"].ToString()))
                    {
                        case 1:
                            m_Permission.IsAllowedView = (bool)row["Allow"];
                            break;
                        case 2:
                            m_Permission.IsAllowedAddnew = (bool)row["Allow"];
                            break;
                        case 3:
                            m_Permission.IsAllowedUpdate = (bool)row["Allow"];
                            break;
                        case 4:
                            m_Permission.IsAllowedDelete = (bool)row["Allow"];
                            break;
                        case 5:
                            m_Permission.IsAllowedImport = (bool)row["Allow"];
                            break;
                        case 6:
                            m_Permission.IsAllowedExport = (bool)row["Allow"];
                            break;
                        case 7:
                            m_Permission.IsAllowedPublish = (bool)row["Allow"];
                            break;
                        case 8:
                            m_Permission.IsAllowedApprove = (bool)row["Allow"];
                            break;
                        case 9:
                            m_Permission.IsAllowedSearch = (bool)row["Allow"];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            //this.SaveActionLog("test", "BasePage OnLoad 1");


            if (User.Identity.IsAuthenticated)
                LoadPermission();

            //Danh cho Inside cu

            //m_Permission = new Permission();
            //m_Permission.IsAllowedView = true;
            //m_Permission.IsAllowedAddnew = true;
            //m_Permission.IsAllowedUpdate = true;
            //m_Permission.IsAllowedDelete = true;
            //m_Permission.IsAllowedImport = true;
            //m_Permission.IsAllowedExport = true;
            //m_Permission.IsAllowedPublish = true;
            //m_Permission.IsAllowedApprove = true;

            //-------------------


            base.OnLoad(e);
            //this.SaveActionLog("test", "BasePage OnLoad 2");

        }

        protected override void OnError(EventArgs e)
        {
            Exception curException = Server.GetLastError().GetBaseException();
            ErrorLog errObj = new ErrorLog(User.Identity.Name, this.Request.RawUrl, curException);
            ErrorLogManager ErrManObj = new ErrorLogManager();
            ErrManObj.SavePageExeption(errObj);
            base.OnError(e);
            //this.Server.ClearError();
        }

        //protected override void OnInit(EventArgs e)
        //{
        //    this.Page.Theme = "Default";
        //    base.OnInit(e);
        //}
        protected void SaveErrorLog(Exception ex)
        {
            ErrorLog pageEx = new ErrorLog(User.Identity.Name, this.Request.RawUrl, ex);
            ErrorLogManager ErrManObj = new ErrorLogManager();
            ErrManObj.SavePageExeption(pageEx);
        }

        protected void SaveActionLog(string strOperation, string strData)
        {
            ActionLog al = new ActionLog();
            al.LogDate = DateTime.Now;
            al.IP = this.Request.ServerVariables["REMOTE_ADDR"];
            al.PageTitle = this.Page.Title;
            al.Path = this.Request.RawUrl;
            al.UserName = User.Identity.Name;
            al.Operation = strOperation;
            al.Data = strData;
            ActionLogManager alManager = new ActionLogManager();
            alManager.SaveActionLog(al);
        }

        public System.DateTime DateParse(string input)
        {
            //DateTimeFormatInfo sFormat = new DateTimeFormatInfo();
            //sFormat.ShortDatePattern = "dd/MM/yyyy";
            // System.DateTime output = new System.DateTime();
            //output = System.DateTime.Parse(input, sFormat);
            //return output;

            string[] strDate = input.Split('/');
            DateTime output = new DateTime(int.Parse(strDate[2]), int.Parse(strDate[1]), int.Parse(strDate[0]));
            return output;
        }

        public System.DateTime DateTimeParse(string input)
        {
            // dd/MM/yyyy HH:mm:ss
            string[] str = input.Split(' ');
            string[] strDate = str[0].Split('/');
            string[] strTime = str[1].Split(':');
            DateTime output = new DateTime(int.Parse(strDate[2]), int.Parse(strDate[1]), int.Parse(strDate[0]), int.Parse(strTime[0]), int.Parse(strTime[1]), int.Parse(strTime[2]));
            return output;
        }

        public static void Export(string fileName, GridView gv, string caption)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            //HttpContext.Current.Response.ContentType = "application/ms-excel"
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentEncoding = Encoding.Unicode;
            HttpContext.Current.Response.BinaryWrite(Encoding.Unicode.GetPreamble());

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            //  Create a form to contain the grid
            Table table = new Table();
            table.GridLines = gv.GridLines;
            //  add the header row to the table
            if ((((gv.HeaderRow) != null)))
            {
                PrepareControlForExport(gv.HeaderRow);
                table.Rows.Add(gv.HeaderRow);
            }
            //  add each of the data rows to the table
            foreach (GridViewRow row in gv.Rows)
            {
                PrepareControlForExport(row);
                table.Rows.Add(row);
            }
            //  add the footer row to the table
            if ((((gv.FooterRow) != null)))
            {
                PrepareControlForExport(gv.FooterRow);
                table.Rows.Add(gv.FooterRow);
            }

            //  render the table into the htmlwriter
            table.RenderControl(htw);

            HttpContext.Current.Response.Write(caption + "<br />");

            //  render the htmlwriter into the response
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }

        public static DataTable ConvertGVtoDT(GridView gridview)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (gridview.HeaderRow != null)
            {

                for (int i = 0; i < gridview.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(HtmlDecode(gridview.HeaderRow.Cells[i].Text.Replace("\r", "").Replace("\n", "").Replace("  ", "").Replace("&nbsp;", "")));
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in gridview.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = HtmlDecode(row.Cells[i].Text.Replace("\r", "").Replace("\n", "").Replace("  ", "").Replace("&nbsp;", ""));
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static void Excel(DataTable dt, string name)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                var finalString = new String(stringChars);
                string fileName = name + "_" + finalString;

                using (ExcelPackage xp = new ExcelPackage())
                {
                    ExcelWorksheet ws = xp.Workbook.Worksheets.Add(name);

                    int rowstart = 2;
                    int colstart = 2;
                    int rowend = rowstart;
                    int colend = colstart + dt.Columns.Count - 1;

                    ws.Cells[rowstart, colstart, rowend, colend].Merge = true;
                    ws.Cells[rowstart, colstart, rowend, colend].Value = name;
                    ws.Cells[rowstart, colstart, rowend, colend].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    ws.Cells[rowstart, colstart, rowend, colend].Style.Font.Bold = true;
                    ws.Cells[rowstart, colstart, rowend, colend].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[rowstart, colstart, rowend, colend].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                    rowstart += 2;
                    rowend = rowstart + dt.Rows.Count;
                    ws.Cells[rowstart, colstart].LoadFromDataTable(dt, true);

                    int i = 1;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        i++;
                        if (dc.DataType == typeof(decimal))
                            ws.Column(i).Style.Numberformat.Format = "#,##0.00;(#,##0.00)";
                    }
                    ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                       ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                       ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                       ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xlsx");
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.BinaryWrite(xp.GetAsByteArray());
                    HttpContext.Current.Response.End();
                }
            }
        }

        public static DataTable ToDataTable(FileUpload fuExcel)
        {
            ExcelPackage package = new ExcelPackage(fuExcel.FileContent);
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {
                table.Columns.Add(firstRowCell.Text);
            }

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                table.Rows.Add(newRow);
            }
            return table;
        }


        public static string HtmlDecode(string myString)
        {
            StringWriter myWriter = new StringWriter();
            HttpUtility.HtmlDecode(myString, myWriter);

            return myWriter.ToString();
        }

        //public static void ExportExcel(GridView gv, string caption)
        //{
        //    if (gv.Rows.Count > 0)
        //    {
        //        var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        //        var stringChars = new char[8];
        //        var random = new Random();
        //        for (int i = 0; i < stringChars.Length; i++)
        //        {
        //            stringChars[i] = chars[random.Next(chars.Length)];
        //        }
        //        var finalString = new String(stringChars);
        //        string fileName = "download_" + finalString + ".xls";

        //        HttpContext.Current.Response.Clear();
        //        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        //        //HttpContext.Current.Response.ContentType = "application/ms-excel"
        //        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        //        HttpContext.Current.Response.Charset = "";
        //        HttpContext.Current.Response.ContentEncoding = Encoding.Unicode;
        //        HttpContext.Current.Response.BinaryWrite(Encoding.Unicode.GetPreamble());

        //        StringWriter sw = new StringWriter();
        //        HtmlTextWriter htw = new HtmlTextWriter(sw);
        //        //  Create a form to contain the grid
        //        Table table = new Table();
        //        table.GridLines = gv.GridLines;
        //        //  add the header row to the table
        //        if ((((gv.HeaderRow) != null)))
        //        {
        //            PrepareControlForExport(gv.HeaderRow);
        //            table.Rows.Add(gv.HeaderRow);
        //        }
        //        //  add each of the data rows to the table
        //        foreach (GridViewRow row in gv.Rows)
        //        {
        //            PrepareControlForExport(row);
        //            table.Rows.Add(row);
        //        }
        //        //  add the footer row to the table
        //        if ((((gv.FooterRow) != null)))
        //        {
        //            PrepareControlForExport(gv.FooterRow);
        //            table.Rows.Add(gv.FooterRow);
        //        }

        //        //  render the table into the htmlwriter
        //        table.RenderControl(htw);

        //        HttpContext.Current.Response.Write(caption + "<br />");

        //        //  render the htmlwriter into the response
        //        HttpContext.Current.Response.Write(sw.ToString());
        //        HttpContext.Current.Response.End();
        //    }
        //}

        // Replace any of the contained controls with literals
        private static void PrepareControlForExport(Control control)
        {
            int i = 0;
            while ((i < control.Controls.Count))
            {
                Control current = control.Controls[i];
                if ((current is LinkButton))
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl(((LinkButton)current).Text));
                }
                else if ((current is ImageButton))
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl(((ImageButton)current).AlternateText));
                }
                else if ((current is HyperLink))
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl(((HyperLink)current).Text));
                }
                else if ((current is DropDownList))
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl(((DropDownList)current).SelectedItem.Text));
                }
                else if ((current is CheckBox))
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl(((CheckBox)current).Checked.ToString()));
                    //TODO: Warning!!!, inline IF is not supported ?
                }
                if (current.HasControls() == true)
                {
                    PrepareControlForExport(current);
                }
                i = (i + 1);
            }
        }

        ////Ghi lỗi vào File
        //public static String = "D:\\Logs\\Inside\\";
        //public void WriteLog(string FunctionPage, string ErrDesc)
        //{           
        //    System.DateTime DateNow = new System.DateTime();
        //    //Dim FileName As String = LOGPATH & DateTime.Now.Year.ToString() & DateTime.Now.Month.ToString("00") & ".txt"
        //    string FileName = LOGPATH + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + ".txt";
        //    System.IO.StreamWriter objStreamWriter = null;
        //    //Try
        //    if (!System.IO.File.Exists(FileName))
        //    {
        //        objStreamWriter = System.IO.File.CreateText(FileName);
        //        objStreamWriter.Close();
        //    }

        //    objStreamWriter = System.IO.File.AppendText(FileName);
        //    objStreamWriter.WriteLine(DateTime.Now.ToString() + "," + FunctionPage + "," + ErrDesc);
        //    objStreamWriter.Close();
        //    //Catch ex As Exception

        //    //Finally

        //    //End Try
        //}



        protected string TienIchGetSHA1Hash(params string[] arrParams)
        {
            string Input = "";
            int i;
            for (i = 0; i <= (arrParams.Length - 1); i++)
            {
                Input = Input + " " + arrParams[i];
            }


            System.Security.Cryptography.SHA1CryptoServiceProvider x = new System.Security.Cryptography.SHA1CryptoServiceProvider();

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(Input);

            bs = x.ComputeHash(bs);

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string md5String = (string)(s.ToString());
            return md5String;

        }

        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}
