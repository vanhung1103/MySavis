using System.Drawing;

namespace Assignment.Models
{
    public class BillDetail
    {
        public Guid ID { get; set; }
        public Guid IDHD { get; set; }
        public Guid IDSP { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Type { get; set; } //Loại Hàng
        public string Image { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}
