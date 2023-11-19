using Assignment.Models;

namespace Assignment.IServices
{
    public interface IBillServices
    {
        public bool CreateBill(Bill p);
        public bool UpdateBill(Bill p);
        public bool DeleteBill(Guid id);
        public List<Bill> GetAllBill();
    }
}
