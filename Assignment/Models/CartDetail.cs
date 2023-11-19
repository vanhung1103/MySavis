using System.Drawing;

namespace Assignment.Models
{
    public class CartDetail
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid IDSP { get; set; }
        public int  Quantity { get; set; }
        public int  Price { get; set; }
        public int  Size { get; set; }
        public string  Color { get; set; }
        public string Type { get; set; } //Loại Hàng
        public string Image { get; set; }
        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
