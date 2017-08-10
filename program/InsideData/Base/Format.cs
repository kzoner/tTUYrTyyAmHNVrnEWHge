
namespace Inside.InsideData.Base
{
    public class Format
    {
        public string RenameAccountTitle(string str)
        {
            string result = "";

            switch (str)
            {
                case "AccountId":
                    result = "Mã";
                    break;
                case "AccountTypeId":
                    result = "Loại";
                    break;
                case "AccountName":
                    result = "Tên";
                    break;
                case "AccountShortName":
                    result = "Tên viết tắt";
                    break;
                case "AccountLevelId":
                    result = "Cấp đại lý";
                    break;
                case "ContactName":
                    result = "Người liên hệ";
                    break;
                case "Address":
                    result = "Địa chỉ";
                    break;
                case "PhoneNumber1":
                    result = "Điện thoại 1";
                    break;
                case "PhoneNumber2":
                    result = "Điện thoại 2";
                    break;
                case "PhoneNumber3":
                    result = "Điện thoại 3";
                    break;
                case "Email":
                    result = "Email";
                    break;
                case "Website":
                    result = "Website";
                    break;
                case "Note":
                    result = "Ghi chú";
                    break;
                case "AccountStatus":
                    result = "Trạng thái";
                    break;
                case "CreateDate":
                    result = "Ngày tạo";
                    break;
                case "CreateUser":
                    result = "Người tạo";
                    break;
                case "UpdateDate":
                    result = "Ngày cập nhật";
                    break;
                case "UpdateUser":
                    result = "Người cập nhật";
                    break;
                default:
                    result = str;
                    break;
            }

            return result;
        }

        public string RenameProductTitle(string str)
        {
            string result = "";

            switch (str)
            {
                case "ProductId":
                    result = "Mã";
                    break;
                case "ProductTypeId":
                    result = "Loại";
                    break;
                case "ProductCode":
                    result = "Mã sản phẩm";
                    break;
                case "ProductName":
                    result = "Tên sản phẩm";
                    break;
                case "Price":
                    result = "Đơn giá";
                    break;
                case "Length":
                    result = "Chiểu dài";
                    break;
                case "Width":
                    result = "Chiểu rộng";
                    break;
                case "Depth":
                    result = "Chiểu sâu";
                    break;
                case "Height":
                    result = "Chiểu cao";
                    break;
                case "Weigh":
                    result = "Khối lượng";
                    break;
                case "UnitTypeId":
                    result = "Đơn vị tính";
                    break;
                case "UnitId":
                    result = "Đơn vị";
                    break;
                case "UnitValue":
                    result = "Quy đổi";
                    break;
                case "ImageName":
                    result = "Hình";
                    break;
                case "ImagePath":
                    result = "Hình";
                    break;
                case "Note":
                    result = "Ghi chú";
                    break;
                case "ProductStatus":
                    result = "Trạng thái";
                    break;
                case "CreateDate":
                    result = "Ngày tạo";
                    break;
                case "CreateUser":
                    result = "Người tạo";
                    break;
                case "UpdateDate":
                    result = "Ngày cập nhật";
                    break;
                case "UpdateUser":
                    result = "Người cập nhật";
                    break;
                default:
                    result = str;
                    break;
            }

            return result;
        }

        public string RenameOrderTitle(string str)
        {
            string result = "";

            switch (str)
            {
                case "OrderId":
                    result = "Mã";
                    break;
                case "AccountTypeId":
                    result = "Loại";
                    break;
                case "AccountName":
                    result = "Tên";
                    break;
                case "OrderCode":
                    result = "Mã đơn hàng";
                    break;
                case "Quantity":
                    result = "Tổng số lượng";
                    break;
                case "Amount":
                    result = "Tổng tiền";
                    break;
                case "TransportFee":
                    result = "Phí vận chuyển";
                    break;
                case "Note":
                    result = "Ghi chú";
                    break;
                case "CreateDate":
                    result = "Ngày tạo";
                    break;
                case "CreateUser":
                    result = "Người tạo";
                    break;
                case "UpdateDate":
                    result = "Ngày cập nhật";
                    break;
                case "UpdateUser":
                    result = "Người cập nhật";
                    break;
                default:
                    result = str;
                    break;
            }

            return result;
        }

        public string RenameFeeTitle(string str)
        {
            string result = "";

            switch (str)
            {
                case "FeeId":
                    result = "Mã";
                    break;
                case "FeeTypeId":
                    result = "Loại";
                    break;
                case "Amount":
                    result = "Tiền";
                    break;
                case "UserName":
                    result = "Người dùng";
                    break;
                case "Note":
                    result = "Ghi chú";
                    break;
                case "FeeStatus":
                    result = "Trạng thái";
                    break;
                case "CreateDate":
                    result = "Ngày tạo";
                    break;
                case "CreateUser":
                    result = "Người tạo";
                    break;
                case "UpdateDate":
                    result = "Ngày cập nhật";
                    break;
                case "UpdateUser":
                    result = "Người cập nhật";
                    break;
                default:
                    result = str;
                    break;
            }

            return result;
        }
    }
}
