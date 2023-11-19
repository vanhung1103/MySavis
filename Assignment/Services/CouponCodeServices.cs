using Assignment.IServices;
using Assignment.Models;

namespace Assignment.Services
{
    public class CouponCodeServices : ICouponCodeServices
    {
        DbContexts context;
        public CouponCodeServices()
        {
            context = new DbContexts();
        }
        public bool CreateCouponCode(CouponCode p)
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

        public bool DeleteCouponCode(Guid id)
        {
            try
            {
                var Product = context.CouponCode.Find(id);
                context.Remove(Product);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CouponCode> GetAllCouponCode()
        {
            return context.CouponCode.ToList();
        }

        public bool UpdateCouponCode(CouponCode p)
        {
            try
            {
                var coupon = context.CouponCode.Find(p.ID);
                coupon.ID = p.ID;
                coupon.CouponCodes = p.CouponCodes;
                coupon.CouponValue = p.CouponValue;
                coupon.Status = p.Status;
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
