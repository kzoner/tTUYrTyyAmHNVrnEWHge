using System;

namespace Inside.InsideData.Base
{
    public class ProductBase
    {
        private int m_ProductId;
        private int m_ProductTypeId;
        private string m_ProductCode;
        private string m_ProductName;
        private decimal m_Price;
        private float m_Length;
        private float m_Width;
        private float m_Depth;
        private float m_Height;
        private float m_Weigh;
        private int m_UnitTypeId;
        private int m_UnitId;
        private float m_UnitValue;
        private string m_ImageName;
        private string m_ImagePath;
        private string m_Note;
        private int m_ProductStatus;
        private DateTime m_CreateDate;
        private string m_CreateUser;
        private DateTime m_UpdateDate;
        private string m_UpdateUser;

        public int ProductId { get => m_ProductId; set => m_ProductId = value; }
        public int ProductTypeId { get => m_ProductTypeId; set => m_ProductTypeId = value; }
        public string ProductCode { get => m_ProductCode; set => m_ProductCode = value; }
        public string ProductName { get => m_ProductName; set => m_ProductName = value; }
        public decimal Price { get => m_Price; set => m_Price = value; }
        public float Length { get => m_Length; set => m_Length = value; }
        public float Width { get => m_Width; set => m_Width = value; }
        public float Depth { get => m_Depth; set => m_Depth = value; }
        public float Height { get => m_Height; set => m_Height = value; }
        public float Weigh { get => m_Weigh; set => m_Weigh = value; }
        public int UnitTypeId { get => m_UnitTypeId; set => m_UnitTypeId = value; }
        public int UnitId { get => m_UnitId; set => m_UnitId = value; }
        public float UnitValue { get => m_UnitValue; set => m_UnitValue = value; }
        public string ImageName { get => m_ImageName; set => m_ImageName = value; }
        public string ImagePath { get => m_ImagePath; set => m_ImagePath = value; }
        public string Note { get => m_Note; set => m_Note = value; }
        public int ProductStatus { get => m_ProductStatus; set => m_ProductStatus = value; }
        public DateTime CreateDate { get => m_CreateDate; set => m_CreateDate = value; }
        public string CreateUser { get => m_CreateUser; set => m_CreateUser = value; }
        public DateTime UpdateDate { get => m_UpdateDate; set => m_UpdateDate = value; }
        public string UpdateUser { get => m_UpdateUser; set => m_UpdateUser = value; }
    }
}
