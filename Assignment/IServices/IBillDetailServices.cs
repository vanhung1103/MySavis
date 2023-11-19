using Assignment.Models;

namespace Assignment.IServices
{
    public interface IBillDetailServices
    {
        public bool CreateBillDetail(BillDetail p);
        public bool UpdateBillDetail(BillDetail p);
        public bool DeleteBillDetail(Guid id);
        public List<BillDetail> GetAllBillDetail();
    }
}
