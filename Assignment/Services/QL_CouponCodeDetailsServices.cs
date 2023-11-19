using Assignment.IServices;
using Assignment.Models;
using Assignment.ViewModels;
using Microsoft.CodeAnalysis;

namespace Assignment.Services
{
    public class QL_CouponCodeDetailsServices : IQL_CouponCodeDetailsServices
    {
        DbContexts context;
        IProductServices _productServices;
        ICouponCodeDetailsServices _couponCodeDetailsServices;
        public QL_CouponCodeDetailsServices()
        {
            context = new DbContexts();
            _productServices = new ProductServices();
            _couponCodeDetailsServices = new CouponCodeDetailsServices();
        }
        public bool CreateCouponCodeDetails(QL_CouponCodeDetailsViews p)
        {
            if (p == null)
            {
                return false;
            }
            else
            {
                CouponCodeDetails c = new CouponCodeDetails()
                {
                    CouponCodeID = p.CouponCodeID,
                    ProductID = p.ProductID,
                    CouponValue = p.CouponValue
                };
                context.Add(c);
                return true;
            }
        }

        public bool DeleteCouponCodeDetails(Guid id)
        {
            if (id == null)
            {
                return false;
            }
            else
            {
                var Remove = _couponCodeDetailsServices.GetAllCouponCode().FirstOrDefault(c => c.ID == id);
                context.Remove(Remove);
                context.SaveChanges();
                return true;
            }
        }

        public List<QL_CouponCodeDetailsViews> GetAllCouponCode()
        {
            List<QL_CouponCodeDetailsViews> lst = new List<QL_CouponCodeDetailsViews>(
       from a in _couponCodeDetailsServices.GetAllCouponCode()
       join c in _productServices.GetAllProduct() on a.ProductID equals c.ID
       select new QL_CouponCodeDetailsViews()
       {
           ID = a.ID,
           CouponCodeID = a.CouponCodeID,
           ProductID = a.ProductID,
           CouponValue = a.CouponValue,
           ProductName = c.Name

       }
       ).ToList();
            return lst;
        }

        public bool UpdateCouponCodeDetails(QL_CouponCodeDetailsViews p)
        {
            if (p == null)
            {
                return false;
            }
            else
            {
                var c = _couponCodeDetailsServices.GetAllCouponCode().FirstOrDefault(c => c.ID == p.ID);
                c.ID = p.ID;
                c.CouponCodeID = p.CouponCodeID;
                c.ProductID = p.ProductID;
                c.CouponValue = p.CouponValue;
                context.Add(c);
                return true;
            }
        }
    }
}
