using Assignment.IServices;
using Assignment.Models;

namespace Assignment.Services
{
    public class CartServices: ICartServices
    {
        DbContexts context;
        public CartServices()
        {
            context = new DbContexts();
        }

        public bool CreateCart(Cart p)
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

        public bool DeleteCart(Guid id)
        {
            try
            {
                var Cart = context.Carts.Find(id);
                context.Remove(Cart);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Cart> GetAllCart()
        {
            return context.Carts.ToList();
        }

        public bool UpdateCart(Cart p)
        {
            try
            {
                var Cart = context.Carts.Find(p.UserID);
                Cart.Description = p.Description;
                context.Update(Cart);
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
