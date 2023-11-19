using Assignment.Models;

namespace Assignment.ViewModels
{
    public class QL_BillViews
    {
        public Guid ID { get; set; }
        public DateTime CreatDate { get; set; }
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
        public int TotalAmout { get; set; }
        public int ShippingFee { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public double Discount { get; set; } //giá trị giảm
        public virtual List<BillDetail> BillDetails { get; set; }
        public virtual User User { get; set; }
    }
}
