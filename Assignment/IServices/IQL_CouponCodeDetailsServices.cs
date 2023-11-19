using Assignment.Models;
using Assignment.ViewModels;

namespace Assignment.IServices
{
    public interface IQL_CouponCodeDetailsServices
    {
        public bool CreateCouponCodeDetails(QL_CouponCodeDetailsViews p);
        public bool UpdateCouponCodeDetails(QL_CouponCodeDetailsViews p);
        public bool DeleteCouponCodeDetails(Guid id);
        public List<QL_CouponCodeDetailsViews> GetAllCouponCode();
    }
}
