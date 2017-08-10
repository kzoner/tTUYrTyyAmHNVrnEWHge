using System;

namespace Inside.SecurityProviders
{
    public class MenuItem
    {
        #region Member variables

        private int m_MenuID = -1;
        private string m_DisplayName = string.Empty;
        private string m_Path = string.Empty;
        private string m_FileName = string.Empty;
        private string m_Link = string.Empty;
        private int m_ResourceID = -1;
        private int m_ParentMenuID = -1;
        private int m_Depth = -1;
        private string m_FunctionName = string.Empty;

        #endregion

        #region Constructors

        public MenuItem() { }

        public MenuItem(int menuID, string displayName, string path, string fileName, string link, int resourceID, int parentMenuID, int depth, string functionName)
        {
            m_MenuID = menuID;
            m_DisplayName = displayName;
            m_Path = path;
            m_FileName = fileName;
            m_Link = link;
            m_ResourceID = resourceID;
            m_ParentMenuID = parentMenuID;
            m_Depth = depth;
            m_FunctionName = functionName;
        }

        #endregion

        #region Properties

        public int MenuID 
        {
            get
            {
                return m_MenuID;
            }
        }
        public string DisplayName 
        {
            get
            {
                return m_DisplayName;
            }

            set
            {
                m_DisplayName = value;
            }
        }
        public string Path 
        {
            get
            {
                return m_Path;
            }

            set
            {
                m_Path = value;
            }
        }
        public string FileName 
        {
            get
            {
                return m_FileName;
            }

            set
            {
                m_FileName = value;
            }
        }
        public string Link 
        {
            get
            {
                return m_Link;
            }

            set
            {
                m_Link = value;
            }
        }
        public int ResourceID 
        {
            get
            {
                return m_ResourceID;
            }

            set
            {
                m_ResourceID = value;
            }
        }
        public int ParentMenuID
        {
            get
            {
                return m_ParentMenuID;
            }
            set
            {
                m_ParentMenuID = value;
            }
        }

        public int DepthIndex
        {
            get
            {
                return m_Depth;
            }
        }

        public string FunctionName
        {
            get
            {
                return m_FunctionName;
            }

            set
            {
                m_FunctionName = value;
            }
        }
        #endregion
    }
}
