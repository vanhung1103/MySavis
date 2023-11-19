using Assignment.IServices;
using Assignment.Models;

namespace Assignment.Services
{
    public class CartDetailServices: ICartDetailServices
    {
        DbContexts context;
        public CartDetailServices()
        {
            context = new DbContexts();
        }

        public bool CreateCartDetail(CartDetail p)
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

        public bool DeleteCartDetail(Guid id)
        {
            try
            {
                var CartDetail = context.CartDetails.Find(id);
                context.Remove(CartDetail);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<CartDetail> GetAllCartDetail()
        {
            return context.CartDetails.ToList();
        }

        public bool UpdateCartDetail(CartDetail p)
        {
            try
            {
                var CartDetail = context.CartDetails.Find(p.ID);
                CartDetail.UserID = p.UserID;
                CartDetail.IDSP = p.IDSP;
                CartDetail.Color = p.Color;
                CartDetail.Size = p.Size;
                CartDetail.Image = p.Image;
                CartDetail.Type = p.Type;
                CartDetail.Quantity = p.Quantity;
                context.Update(CartDetail);
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
