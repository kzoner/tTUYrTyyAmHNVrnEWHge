using System;
using Inside.SecurityProviders.DataAccess;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders
{
    public class ApplicationManager
    {
        private ApplicationAdapter m_applicationAdapter = null;

        private ApplicationAdapter Adapter
        {
            get
            {
                if (m_applicationAdapter == null)
                {
                    m_applicationAdapter = new ApplicationAdapter();
                }

                return m_applicationAdapter;
            }
        }

        /// <summary>
        /// Tao mot ung dung
        /// </summary>
        /// <param name="name">Ten ung dung</param>
        /// <param name="description">Mo ta ung dung</param>
        /// <returns>Tra ve ung dung da tao</returns>
        public Application Create(string name, string description)
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(name))
            {
                SecurityException customEx =
                    new SecurityException("Create application fail.\nApplication name can not blank!");

                throw customEx;
            }

            // Create new application
            Application application = Adapter.Create(name, description);

            return application;
        }

        /// <summary>
        /// Cap nhat ung dung
        /// </summary>
        /// <param name="application"></param>
        public void Update(Application application)
        {
            Adapter.Update(application);
        }

        /// <summary>
        /// Xoa ung dung
        /// </summary>
        /// <param name="application">Doi tuong ung dung</param>
        public void Remove(Application application)
        {
            Adapter.Remove(application);
        }

        /// <summary>
        /// lay danh sach cac ung dung ma user co quyen truy xuat tren do
        /// </summary>
        /// <returns></returns>
        public Application GetApplication(int applicationId)
        {
            return Adapter.GetApplication(applicationId);
        }

        /// <summary>
        /// Lay danh sach ung dung
        /// </summary>
        /// <returns></returns>
        public ApplicationCollection GetAllApplications()
        {
            return Adapter.GetAllApplications();
        }

        public ApplicationCollection GetApplicationsByUser(string strUserName)
        {
            return Adapter.GetApplicationByUser(strUserName);
        }

        /// <summary>
        /// Kiem tra ten ung dung da ton tai chua ?
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public Boolean Exists(string applicationName)
        {
            return Adapter.Exists(applicationName);
        }

    }
}
