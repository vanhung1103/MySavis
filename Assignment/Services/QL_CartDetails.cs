using Assignment.IServices;
using Assignment.Models;
using Assignment.ViewModels;

namespace Assignment.Services
{
    public class QL_CartDetails : IQL_CartDetails
    {
        IProductServices _productServices;
        ICartServices _cartServices;
        ICartDetailServices _cartDetailServices;
        DbContexts context;
        public QL_CartDetails()
        {
            context = new DbContexts();
            _cartServices = new CartServices();
            _productServices = new ProductServices();
            _cartDetailServices = new CartDetailServices();
        }

        public bool CreateCartDetail(QL_CartDetailsViews p)
        {
            if (p == null)
            {
                return false;
            }
            else
            {
                CartDetail c = new CartDetail()
                {
                    UserID = p.UserID,
                    Price = p.Price,
                    IDSP = p.ProductID,
                    Quantity = p.Quantity,
                    Size = p.Size,
                    Image = p.Image,
                    Color = p.Color,
                    Type = p.Type
                };
                context.Add(c);
                return true;
            }
        }
        public bool DeleteCartDetail(Guid id)
        {
            if (id == null)
            {
                return false;
            }
            else
            {
                var Remove = _cartDetailServices.GetAllCartDetail().FirstOrDefault(c=>c.ID==id);
                context.Remove(Remove);
                context.SaveChanges();
                return true;
            }
        }

        public List<QL_CartDetailsViews> GetAllCartDetail()
        {
            List<QL_CartDetailsViews> lst = new List<QL_CartDetailsViews>(
        from a in _cartDetailServices.GetAllCartDetail()
        join b in _cartServices.GetAllCart() on a.UserID equals b.UserID
        join c in _productServices.GetAllProduct() on a.IDSP equals c.ID
        select new QL_CartDetailsViews()
        {
            ID = a.ID,
            UserID = b.UserID,
            ProductID = c.ID,
            ProductName = c.Name,
            Quantity = a.Quantity,
            Price = c.Price,
            Size = c.Size,
            Image = c.Image,
            Color = c.Color,
            Type = c.Type
            
        }
        ).ToList();
                return lst;
            }

        public bool UpdateCartDetail(QL_CartDetailsViews p)
        {
            if (p == null)
            {
                return false;
            }
            else
            {
                var cartDetails = _cartDetailServices.GetAllCartDetail().FirstOrDefault(c=>c.ID==p.ID);
                cartDetails.UserID = p.UserID;
                cartDetails.IDSP = p.ProductID;
                cartDetails.Quantity = p.Quantity;
                cartDetails.Size = p.Size;
                cartDetails.Price = p.Price;
                cartDetails.Image = p.Image;
                cartDetails.Color = p.Color;
                cartDetails.Type = p.Type;
                context.Add(cartDetails);
                return true;
            }
        }
    }
}
