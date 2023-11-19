using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Assignment.Models
{
    public class Product
    {
        public Guid ID { get; set; }
        public string Name { get; set; }// Tên
        public int Price { get; set; } // giá
        public int AvailableQuantity { get; set; } //sl tồn
        public int Status { get; set; } //trạng thái
        public int Size { get; set; } //Size
        public string Color { get; set; } // màu sắc
        public string  Supplier { get; set; } //nsx
        public string Type { get; set; } //Loại Hàng
        public string Image { get; set; } //Ảnh
        public string Image1 { get; set; } //Ảnh
        public string Image2 { get; set; } //Ảnh
        public string Image3 { get; set; } //Ảnh
        public string Image4 { get; set; } //Ảnh
        public string Image5 { get; set; } //Ảnh
        public string Description { get; set; } //mô tả
        public virtual List<BillDetail> BillDetails { get; set; }
        public virtual List<CartDetail> CartDetails { get; set; }
        public virtual List<CouponCodeDetails> CouponCodeDetails { get; set; }
    }
}
