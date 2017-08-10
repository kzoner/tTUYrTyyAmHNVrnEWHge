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

namespace WebAdmin.Base
{
    public class Permission
    {
        #region Members

        private bool m_bView = false;
        private bool m_bAddnew = false;
        private bool m_bUpdate = false;
        private bool m_bDelete = false;

        private bool m_bImport = false;
        private bool m_bExport = false;
        private bool m_bPublish = false;
        private bool m_bApprove = false;
        private bool m_bSearch = false;

        #endregion

        #region Properties

        /// <summary>
        /// Cho phep Xem du lieu
        /// </summary>
        public bool IsAllowedView
        {
            get
            {
                return this.m_bView;
            }
            set
            {
                this.m_bView = value;
            }
        }

        /// <summary>
        /// Cho phep Insert du lieu
        /// </summary>
        public bool IsAllowedAddnew
        {
            get
            {
                return this.m_bAddnew;
            }
            set
            {
                this.m_bAddnew = value;
            }
        }

        /// <summary>
        /// Cho phep cap nhat du lieu
        /// </summary>
        public bool IsAllowedUpdate
        {
            get
            {
                return this.m_bUpdate;
            }
            set
            {
                this.m_bUpdate = value;
            }
        }

        /// <summary>
        /// Cho phep xoa du lieu
        /// </summary>
        public bool IsAllowedDelete
        {
            get
            {
                return this.m_bDelete;
            }
            set
            {
                this.m_bDelete = value;
            }
        }

        /// <summary>
        /// Cho phep publish tin tuc
        /// </summary>
        public bool IsAllowedPublish
        {
            get
            {
                return this.m_bPublish;
            }
            set
            {
                this.m_bPublish = value;
            }
        }

        /// <summary>
        /// Cho phep Import du lieu
        /// </summary>
        public bool IsAllowedImport
        {
            get
            {
                return this.m_bImport;
            }
            set
            {
                this.m_bImport = value;
            }
        }

        /// <summary>
        /// Cho phep xuat du lieu (Xuat Excel)
        /// </summary>
        public bool IsAllowedExport
        {
            get
            {
                return this.m_bExport;
            }
            set
            {
                this.m_bExport = value;
            }
        }

        /// <summary>
        /// Cho phep Approve
        /// </summary>
        public bool IsAllowedApprove
        {
            get
            {
                return this.m_bApprove;
            }
            set
            {
                this.m_bApprove = value;
            }
        }

        public bool IsAllowedSearch
        {
            get
            {
                return this.m_bSearch;
            }
            set
            {
                this.m_bSearch = value;
            }
        }
        #endregion

        #region Constructors
        public Permission() { }
        /*
             public Permission(bool bIsAllowedView, bool bIsAllowedExport)
             {
                 this.m_bView = bIsAllowedView;
                 this.m_bExport = bIsAllowedExport;
             }

             public Permission(bool bIsAllowedView, bool bIsAllowedDestroyCard, bool bIsAllowedActiveCard)
             {
                 this.m_bView = bIsAllowedView;
                 this = bIsAllowedActiveCard;
                 this.m_bDestroyCard = bIsAllowedDestroyCard;
             }

             public Permission(bool bIsAllowedView, bool bIsAllowedDestroyCard, bool bIsAllowedActiveCard, bool bIsAllowedExport)
             {
                 this.m_bView = bIsAllowedView;
                 this.m_bActiveCard = bIsAllowedActiveCard;
                 this.m_bExport= bIsAllowedExport;
                 this.m_bDestroyCard = bIsAllowedDestroyCard;
             }

             public Permission(bool bIsAllowedView, bool bIsAllowedAddnew, bool bIsAllowedUpdate, bool bIsAllowedDelete, bool bIsAllowedPublish)
             {
                 this.m_bView = bIsAllowedView;
                 this.m_bAddnew = bIsAllowedAddnew;
                 this.m_bUpdate = bIsAllowedUpdate;
                 this.m_bDelete = bIsAllowedDelete;
                 this.m_bPublish = bIsAllowedPublish;
             }

             public Permission(bool bIsAllowedView, bool bIsAllowedAddnew, bool bIsAllowedUpdate, bool bIsAllowedDelete, bool bIsAllowedPublish, bool bIsAllowedDestroyCard, bool bIsAllowedActiveCard, bool bIsAllowedExport)
             {
                 this.m_bView = bIsAllowedView;
                 this.m_bAddnew = bIsAllowedAddnew;
                 this.m_bUpdate = bIsAllowedUpdate;
                 this.m_bDelete = bIsAllowedDelete;
                 this.m_bPublish = bIsAllowedPublish;
                 this.m_bActiveCard = bIsAllowedActiveCard;
                 this.m_bExport = bIsAllowedExport;
                 this.m_bDestroyCard = bIsAllowedDestroyCard;
             }
         */
        #endregion

    }
}
