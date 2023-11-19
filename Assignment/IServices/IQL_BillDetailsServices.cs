using Assignment.Models;
using Assignment.ViewModels;

namespace Assignment.IServices
{
    public interface IQL_BillDetailsServices
    {
        public bool CreateBillDetail(QL_BillDetailsViews p);
        public bool UpdateBillDetail(QL_BillDetailsViews p);
        public bool DeleteBillDetail(Guid id);
        public List<QL_BillDetailsViews> GetAllBillDetail();
        
    }
}
