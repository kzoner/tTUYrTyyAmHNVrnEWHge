using System;
using Inside.SecurityProviders.DataAccess;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders
{
    public class OperationManager
    {

        private OperationAdapter m_operationAdapter = null;

        private OperationAdapter Adapter
        {
            get
            {
                if (m_operationAdapter == null)
                {
                    m_operationAdapter = new OperationAdapter();
                }
                return m_operationAdapter;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="application"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public Operation Create(int operationCode, string resourceTypeCode, string description)
        {
            try
            {
                //validate data
                if (string.IsNullOrEmpty(operationCode.ToString())) throw new SecurityException("Create operation fail. Operation code can not empty");
                if (string.IsNullOrEmpty(resourceTypeCode)) throw new SecurityException("Create operation fail. Resource type code can not empty");

                return Adapter.Create(operationCode, resourceTypeCode, description);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        public void Update(Operation operation)
        {
            try
            {
                Adapter.Update(operation);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        public void Remove(Operation operation)
        {
            try
            {
                Adapter.Remove(operation);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="category"></param>
        public Operation GetOperation(int operationCode, string resourceTypeCode)
        {
            try
            {
                return Adapter.GetOperation(operationCode, resourceTypeCode);
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
        public OperationCollection FindOperationsByResources(int resourceId)
        {
            try
            {
                return Adapter.FindOperationsByResource(resourceId);
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
        public OperationCollection FindOperationsByResourceType(string resourceTypeCode)
        {
            try
            {
                return Adapter.FindOperationsByResourceType(resourceTypeCode);
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
        /// <returns></returns>
        public OperationCollection FindOperationsByCategory(int operartionCode)
        {
            try
            {
                return Adapter.FindOperationsByCategory(operartionCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
