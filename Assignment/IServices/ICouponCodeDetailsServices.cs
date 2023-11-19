using Assignment.Models;

namespace Assignment.IServices
{
    public interface ICouponCodeDetailsServices
    {
        public bool CreateCouponCodeDetails(CouponCodeDetails p);
        public bool UpdateCouponCodeDetails(CouponCodeDetails p);
        public bool DeleteCouponCodeDetails(Guid id);
        public List<CouponCodeDetails> GetAllCouponCode();
    }
}
