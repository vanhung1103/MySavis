using Assignment.Models;

namespace Assignment.IServices
{
    public interface ICouponCodeServices
    {
        public bool CreateCouponCode(CouponCode p);
        public bool UpdateCouponCode(CouponCode p);
        public bool DeleteCouponCode(Guid id);
        public List<CouponCode> GetAllCouponCode();
    }
}
