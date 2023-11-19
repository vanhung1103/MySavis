using Assignment.Models;

namespace Assignment.ViewModels
{
    public class QL_CouponCodeDetailsViews
    {
        public Guid ID { get; set; }
        public Guid CouponCodeID { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public int CouponValue { get; set; }
        public virtual CouponCode CouponCode { get; set; }
        public virtual Product Product { get; set; }
    }
}
