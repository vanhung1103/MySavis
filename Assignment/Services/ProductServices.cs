using Assignment.IServices;
using Assignment.Models;

namespace Assignment.Services
{
    public class ProductServices:IProductServices
    {
        DbContexts context;
        public ProductServices()
        {
            context = new DbContexts();
        }

        public bool CreateProduct(Product p)
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

        public bool DeleteProduct(Guid id)
        {
            try
            {
                var Product = context.Products.Find(id);
                context.Remove(Product);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetAllProduct()
        {
            return context.Products.ToList();
        }

        public bool UpdateProduct(Product p)
        {
            try
            {
                var Product = context.Products.Find(p.ID);
                Product.Name = p.Name;
                Product.Description = p.Description;
                Product.Price = p.Price;
                Product.Supplier = p.Supplier;
                Product.Status = p.Status;
                Product.AvailableQuantity = p.AvailableQuantity;
                Product.Color = p.Color;
                Product.Size = p.Size;
                Product.Type = p.Type;
                Product.Image =  p.Image;
                Product.Image1 =  p.Image1;
                Product.Image2 =  p.Image2;
                Product.Image3 =  p.Image3;
                Product.Image4 =  p.Image4;
                Product.Image5 =  p.Image5;
                context.Update(Product);
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
