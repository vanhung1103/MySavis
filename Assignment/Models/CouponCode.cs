namespace Assignment.Models
{
    public class CouponCode
    {
        public Guid ID { get; set; }
        public string CouponCodes { get; set; }
        public int CouponValue { get; set; }
        public int Status { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<CouponCodeDetails> CouponCodeDetails { get; set; }
    }
}
