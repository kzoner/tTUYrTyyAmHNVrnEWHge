using System;
using Inside.SecurityProviders.DataAccess;
using Inside.SecurityProviders;
using System.Runtime.InteropServices;
namespace Inside.SecurityProviders
{
    public class ResourceManager
    {

        private ResourceAdpater m_resourceAdapter = null;

        private ResourceAdpater Adapater
        {
            get
            {
                if (m_resourceAdapter == null)
                {
                    m_resourceAdapter = new ResourceAdpater();
                }
                return m_resourceAdapter;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="application"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public Resource Create(string resourceTypeCode, string path, string fileName, string link, int applicationId, string resourceName, bool status, bool isParent)
        {
            if (!isParent)
            {                
                // validate data                
                if (string.IsNullOrEmpty(path)) throw new SecurityException("Thêm mới không thành công. Đường dẫn không được rỗng.");
                if (string.IsNullOrEmpty(fileName)) throw new SecurityException("Thêm mới không thành công. Tên file không được rỗng.");
            }
            if (string.IsNullOrEmpty(resourceTypeCode)) throw new SecurityException("Thêm mới không thành công. Mã loại tài nguyên không được rỗng.");
            if (string.IsNullOrEmpty(applicationId.ToString())) throw new SecurityException("Thêm mới không thành công. Mã ứng dụng không được rỗng.");
            
            return Adapater.Create(resourceTypeCode, path, fileName, link, applicationId, resourceName, status);
        }

        public Resource Create(string resourceTypeCode, string path, string fileName, string link, int applicationId, string resourceName, bool status, bool isParent, string token)
        {
            if (!isParent)
            {
                // validate data                
                if (string.IsNullOrEmpty(path)) throw new SecurityException("Thêm mới không thành công. Đường dẫn không được rỗng.");
                if (string.IsNullOrEmpty(fileName)) throw new SecurityException("Thêm mới không thành công. Tên file không được rỗng.");
            }
            if (string.IsNullOrEmpty(resourceTypeCode)) throw new SecurityException("Thêm mới không thành công. Mã loại tài nguyên không được rỗng.");
            if (string.IsNullOrEmpty(applicationId.ToString())) throw new SecurityException("Thêm mới không thành công. Mã ứng dụng không được rỗng.");

            return Adapater.Create(resourceTypeCode, path, fileName, link, applicationId, resourceName, status,token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        public void Update(Resource resource)
        {
            try
            {
                // validate data
                if (string.IsNullOrEmpty(resource.ResourceTypeCode)) throw new SecurityException("Update resource fail. Resource type code can not empty");

                Adapater.Update(resource);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        public void Remove(int resourceId)
        {
            try
            {
                Adapater.Remove(resourceId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Đọc chi tiết resource
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public Resource GetResource(int resourceId)
        {
            try
            {
                return Adapater.GetResource(resourceId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Đọc tất cả resource
        /// </summary>
        /// <returns></returns>
        public ResourceCollection GetAllResource()
        {
            try
            {
                return Adapater.GetAllResources();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        /// <summary>
        /// Đọc tất cả resource không trỏ đến bất kì MenuItem nào
        /// </summary>
        /// <returns></returns>
        public ResourceCollection GetAllOrphanResource(int ApplicationID)
        {
            try
            {
                return Adapater.GetAllOrphanResource(ApplicationID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Tìm resource theo ứng dụng
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public ResourceCollection FindResourcesByApplication(int applicationId)
        {
            try
            {
                return Adapater.FindResourcesByApplication(applicationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Tìm resource theo tên
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public ResourceCollection FindResourcesByName(string name)
        {
            try
            {
                return Adapater.FindResourcesByName(name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Tìm resource theo đường dẫn
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ResourceCollection FindResourcesByPath(string path)
        {
            try
            {
                return Adapater.FindResourcesByPath(path);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        /// <summary>
        /// Đổi trạng thái resource
        /// </summary>
        /// <param name="resourceID"></param>
        /// <param name="newStatus"></param>
        public void UpdateResourceStatus(int resourceID, bool newStatus)
        {
            try
            {
                Adapater.UpdateResourceStatus(resourceID, newStatus);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        /// <summary>
        /// Filter danh sách resource  theo ApplicationID
        /// </summary>
        /// <param name="resourcesIn"></param>
        /// <param name="applicationID"></param>
        public ResourceCollection FilterResourceByApplication(ResourceCollection resourcesIn, int applicationID)
        {
            try
            {
                ResourceCollection resourcesOut = new ResourceCollection();
                foreach (Resource item in resourcesIn)
                {
                    if (item.ApplicationID == applicationID)
                    {
                        resourcesOut.Add(item);
                    }
                }

                return resourcesOut;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        /// <summary>
        /// Lấy tất cả user / role liên quan đến resource (kể cả deny)
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public System.Data.DataTable GetAllRolesUsersByResource(int resourceID)
        {
            try
            {                
                return Adapater.GetAllRolesUsersByResource(resourceID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Đọc danh sách user và role có quyền truy cập vào resource
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public System.Data.DataTable GetUsersAndRolesByResourceID(int resourceID)
        {
            try
            {
                return Adapater.GetUsersAndRolesByResourceID(resourceID);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        /// <summary>
        /// Generate token string
        /// </summary>
        /// <returns></returns>
        public string GenerateToken()
        {
            return Adapater.GenerateToken();
        }

        public System.Data.DataTable GetRolesUsersByResource(int iResourceID)
        {
            return Adapater.GetRolesUsersByResource(iResourceID);
        }
    }
}
