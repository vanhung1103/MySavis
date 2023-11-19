using Assignment.IServices;
using Assignment.Models;

namespace Assignment.Services
{
    public class BillServices : IBillServices
    {
        DbContexts context;
        public BillServices()
        {
            context = new DbContexts();
        }

        public bool CreateBill(Bill p)
        {
            try
            {
                
                context.Add(p);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteBill(Guid id)
        {
            try
            {
                var Bill = context.Bills.Find(id);
                context.Remove(Bill);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Bill> GetAllBill()
        {
            return context.Bills.ToList();
        }
        public bool UpdateBill(Bill p)
        {
            try
            {
                var Bill = context.Bills.Find(p.ID);
                Bill.CreatDate = p.CreatDate;
                Bill.UserID = p.UserID;
                Bill.Status = p.Status;
                Bill.TotalAmout = p.TotalAmout;
                Bill.ShippingFee = p.ShippingFee;
                Bill.RecipientName = p.RecipientName;
                Bill.RecipientAddress = p.RecipientAddress;
                Bill.RecipientPhoneNumber = p.RecipientPhoneNumber;
                Bill.Discount = p.Discount;
                context.Update(Bill);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
