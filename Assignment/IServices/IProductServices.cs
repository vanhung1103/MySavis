using Assignment.Models;

namespace Assignment.IServices
{
    public interface IProductServices
    {
        public bool CreateProduct(Product p);
        public bool UpdateProduct(Product p);
        public bool DeleteProduct(Guid id);
        public List<Product> GetAllProduct();
        
        
    }
}
