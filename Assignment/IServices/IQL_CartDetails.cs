using Assignment.ViewModels;

namespace Assignment.IServices
{
    public interface IQL_CartDetails
    {
        public bool CreateCartDetail(QL_CartDetailsViews p);
        public bool UpdateCartDetail(QL_CartDetailsViews p);
        public bool DeleteCartDetail(Guid id);
        public List<QL_CartDetailsViews> GetAllCartDetail();
    }
}
