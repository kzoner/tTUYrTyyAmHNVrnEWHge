using System;
using Inside.SecurityProviders.DataAccess;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders
{
    public class OperationCategoryManager
    {
        private OperationCategoryAdapter m_operationCategoryAdapter = null;

        private OperationCategoryAdapter Adapter
        {
            get
            {
                if (m_operationCategoryAdapter == null)
                {
                    m_operationCategoryAdapter = new OperationCategoryAdapter();
                }

                return m_operationCategoryAdapter;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationCode"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public OperationCategory Create(string name,string description)
        {
            try
            {
                // validate input param
                
                if (string.IsNullOrEmpty(name))
                    throw new SecurityException("Create operation category fail. Operation name can not empty");

                return Adapter.Create(name, description);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        public void Update(OperationCategory category)
        {
            Adapter.Update(category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        public void Remove(OperationCategory category)
        {
            Adapter.Remove(category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationCode"></param>
        /// <returns></returns>
        public OperationCategory GetOperationCategory(int operationCode)
        {
            return Adapter.GetOperationCategory(operationCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OperationCategoryCollection GetAllOperationCategories()
        {
            return Adapter.GetAllOperationCategories();
        }

        public OperationCategoryCollection GetOperationCategoriesByResourceTypeCode(string ResourceTypeCode)
        {
            return Adapter.GetOperationCategoriesByResourceTypeCode(ResourceTypeCode);
        }
    }

}
