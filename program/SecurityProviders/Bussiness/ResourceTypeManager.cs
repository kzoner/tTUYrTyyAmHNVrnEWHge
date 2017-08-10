using System;
using Inside.SecurityProviders.DataAccess;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders
{
    public class ResourceTypeManager
    {
        private ResourceTypeAdapter m_resourceTypeAdapter = null;

        private ResourceTypeAdapter Adapter
        {
            get
            {
                if (m_resourceTypeAdapter == null)
                {
                    m_resourceTypeAdapter = new ResourceTypeAdapter();
                }
                return m_resourceTypeAdapter;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceTypeCode"></param>
        /// <param name="name"></param>        
        /// <returns></returns>
        public ResourceType Create(string resourceTypeCode, string name)
        {
            try
            {
                // validate data
                if (string.IsNullOrEmpty(resourceTypeCode)) throw new SecurityException("Lỗi thêm mới. Mã loại tài nguyên không được rỗng.");
                if (resourceTypeCode.Contains(" ")) throw new SecurityException("Lỗi thêm mới. Mã loại tài nguyên không được có khoảng trắng.");
                if (string.IsNullOrEmpty(name)) throw new SecurityException("Lỗi thêm mới. Tên loại tài nguyên không được rỗng.");

                return Adapter.Create(resourceTypeCode, name); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceType"></param>
        public void Update(ResourceType resourceType)
        {
            try
            {
                // validate data
                if (string.IsNullOrEmpty(resourceType.ResourceTypeCode)) throw new SecurityException("Lỗi cập nhật. Mã loại tài nguyên không được rỗng.");
                if (resourceType.ResourceTypeCode.Contains(" ")) throw new SecurityException("Lỗi cập nhật. Mã loại tài nguyên không được có khoảng trắng.");
                if (string.IsNullOrEmpty(resourceType.Name)) throw new SecurityException("Lỗi cập nhật. Tên loại tài nguyên không được rỗng.");

                Adapter.Update(resourceType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceType"></param>
        public void Remove(ResourceType resourceType)
        {
            try
            {
                Adapter.Remove(resourceType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceTypeCode"></param>
        /// <returns></returns>
        public ResourceType GetResourceType(string resourceTypeCode)
        {
            try
            {
                return Adapter.GetResourceType(resourceTypeCode);
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
        public ResourceTypeCollection GetAllResourceTypes()
        {
            try
            {
                return Adapter.GetAllResourceTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
