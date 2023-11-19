using Assignment.IServices;
using Assignment.Models;

namespace Assignment.Services
{
    public class BillDetailServices : IBillDetailServices
    {
        DbContexts context;
        public BillDetailServices()
        {
            context = new DbContexts();
        }

        public bool CreateBillDetail(BillDetail p)
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

        public bool DeleteBillDetail(Guid id)
        {
            try
            {
                var BillDetail = context.BillDetails.Find(id);
                context.Remove(BillDetail);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<BillDetail> GetAllBillDetail()
        {
            return context.BillDetails.ToList();
        }

        public bool UpdateBillDetail(BillDetail p)
        {
            try
            {
                var BillDetail = context.BillDetails.Find(p.ID);
                BillDetail.IDHD = p.IDHD;
                BillDetail.IDSP = p.IDSP;
                BillDetail.Price = p.Price;
                BillDetail.Size = p.Size;
                BillDetail.Image = p.Image;
                BillDetail.Type = p.Type;
                BillDetail.Color = p.Color;
                BillDetail.Quantity = p.Quantity;
                context.Update(BillDetail);
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
