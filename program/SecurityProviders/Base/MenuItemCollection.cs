using System;
using System.Collections;
using System.Xml;
using System.IO;
namespace Inside.SecurityProviders
{
    public class MenuItemCollection : CollectionBase
    {
        public MenuItem this[int index]
        {
            get
            {
                return (MenuItem)this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        public int Add(MenuItem menuItem)
        {
            return List.Add(menuItem);
        }

        public void Remove(MenuItem menuItem)
        {
            List.Remove(menuItem);
        }

        public void Insert(int index, MenuItem menuItem)
        {
            List.Insert(index, menuItem);
        }

        public int IndexOf(MenuItem menuItem)
        {
            return List.IndexOf(menuItem);
        }

        public bool Contains(MenuItem menuItem)
        {
            return List.Contains(menuItem);
        }
        
        public MenuItem Find(int MenuID)
        {
            foreach (MenuItem item in this)
            {
                if (item.MenuID == MenuID)
                    return item;
            }
            return null;
        }

        public override string ToString()
        {
            MemoryStream ms = new MemoryStream();
            XmlWriter xw = XmlWriter.Create(ms);
            int prevDepth = -1;
            int newDepth = 0;
            string ret = string.Empty;
            try
            {
                foreach (MenuItem item in this)
                {
                    newDepth = item.DepthIndex;
                    CloseOpenElements(xw, prevDepth, newDepth);
                    xw.WriteStartElement("MenuItem");
                    xw.WriteAttributeString("MenuID", item.MenuID.ToString());
                    xw.WriteAttributeString("DisplayName", item.DisplayName);
                    xw.WriteAttributeString("Path", item.Path);
                    xw.WriteAttributeString("FileName", item.FileName);
                    xw.WriteAttributeString("Link", item.Link);
                    prevDepth = newDepth;
                }
                newDepth = 0;
                CloseOpenElements(xw, prevDepth, newDepth);
                xw.Flush();
                StreamReader reader = new StreamReader(ms);
                ms.Seek(0, SeekOrigin.Begin);
                ret = reader.ReadToEnd();
                reader.Close();
                //xw.Close();
                ms.Close();                
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        private void CloseOpenElements(XmlWriter xw, int PrevDepth, int NewDepth)
        {
            if (NewDepth <= PrevDepth)
            {
                for (int i = 0; i <= PrevDepth - NewDepth; i++)
                    xw.WriteEndElement();
            }
        }
    }
}
