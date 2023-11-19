using Assignment.IServices;
using Assignment.Models;

namespace Assignment.Services
{
    public class CouponCodeDetailsServices : ICouponCodeDetailsServices
    {
        DbContexts context;
        public CouponCodeDetailsServices()
        {
            context = new DbContexts();
        }
        public bool CreateCouponCodeDetails(CouponCodeDetails p)
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

        public bool DeleteCouponCodeDetails(Guid id)
        {
            try
            {
                var Product = context.CouponCodeDetails.Find(id);
                context.Remove(Product);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CouponCodeDetails> GetAllCouponCode()
        {
            return context.CouponCodeDetails.ToList();
        }

        public bool UpdateCouponCodeDetails(CouponCodeDetails p)
        {
            try
            {
                var coupon = context.CouponCodeDetails.Find(p.ID);
                coupon.ID = p.ID;
                coupon.CouponCodeID = p.CouponCodeID;
                coupon.ProductID = p.ProductID;
                coupon.CouponValue = p.CouponValue;
                context.Update(coupon);
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
