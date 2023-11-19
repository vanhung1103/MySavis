namespace Assignment.Models
{
    public class CouponCodeDetails
    {
        public Guid ID { get; set; }
        public Guid CouponCodeID { get; set; }
        public Guid ProductID { get; set; }
        public int CouponValue { get; set; }
        public virtual CouponCode CouponCode { get; set; }
        public virtual Product Product { get; set; }
    }
}
