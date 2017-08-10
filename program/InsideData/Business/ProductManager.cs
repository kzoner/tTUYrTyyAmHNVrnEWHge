using Inside.InsideData.Base;
using Inside.InsideData.DataAccess;
using System.Data;

namespace Inside.InsideData.Business
{
    public class ProductManager
    {
        ProductAdapter adapter = new ProductAdapter();

        public DataTable ProductType_GetList()
        {
            return adapter.ProductType_GetList(0, 1);
        }

        public string ProductType_GetName(int productTypeId)
        {
            string result = productTypeId.ToString();
            DataTable dt = adapter.ProductType_GetList(productTypeId, 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = dr["ProductTypeName"].ToString();
            }
            return result;
        }

        public int Product_RowTotal(int productTypeId, string productCode, string productName, int productStatus)
        {
            return adapter.Product_RowTotal(productTypeId, productCode, productName, productStatus);
        }

        public DataTable Product_Search(int productTypeId, string productCode, string productName, int productStatus, int rowsPerPage, int pageNumber)
        {
            return adapter.Product_Search(productTypeId, productCode, productName, productStatus, rowsPerPage, pageNumber);
        }

        public DataTable Product_GetList(int productId)
        {
            return adapter.Product_GetList(productId);
        }

        public DataTable Product_GetList_ProductType(int productTypeId)
        {
            return adapter.Product_GetList_ProductType(0, productTypeId, 1);
        }

        public string Product_GetName(int productId)
        {
            string result = productId.ToString();
            DataTable dt = adapter.Product_GetList_ProductType(productId, 0, 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = dr["ProductName"].ToString();
            }
            return result;
        }

        public void Product_Insert(ProductBase product, ref int code, ref string msg)
        {
            adapter.Product_Insert(product, ref code, ref msg);
        }

        public void Product_Update(ProductBase product, ref int code, ref string msg)
        {
            adapter.Product_Update(product, ref code, ref msg);
        }
    }
}
