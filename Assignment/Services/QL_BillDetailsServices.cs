using Assignment.IServices;
using Assignment.Models;
using Assignment.ViewModels;
using System.Drawing;

namespace Assignment.Services
{
    public class QL_BillDetailsServices : IQL_BillDetailsServices
    {
        IBillDetailServices _billDetailServices;
        IBillServices _billServices;
        IProductServices _productServices;
        DbContexts context;
        public QL_BillDetailsServices()
        {
            context = new DbContexts();
            _billDetailServices = new BillDetailServices();
            _billServices = new BillServices();
            _productServices = new ProductServices();
        }

        public bool CreateBillDetail(QL_BillDetailsViews p)
        {
            if (p == null)
            {
                return false;
            }
            else
            {
                QL_BillDetailsViews c = new QL_BillDetailsViews()
                {
                    
                    IDHD = p.IDHD,
                    IDSP = p.IDSP,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    Type = p.Type,
                    Image = p.Image,
                    Size = p.Size,
                    Color = p.Color
                };
                context.Add(c);
                return true;
            }
        }
        public bool DeleteBillDetail(Guid id)
        {
            if (id == null)
            {
                return false;
            }
            else
            {
                var Remove = _billDetailServices.GetAllBillDetail().FirstOrDefault(c => c.ID == id);
                context.Remove(Remove);
                context.SaveChanges();
                return true;
            }
        }


        public List<QL_BillDetailsViews> GetAllBillDetail()
        {

            List<QL_BillDetailsViews> lst = new List<QL_BillDetailsViews>(
        from a in _billDetailServices.GetAllBillDetail()
        join b in _billServices.GetAllBill() on a.IDHD equals b.ID
        join c in _productServices.GetAllProduct() on a.IDSP equals c.ID
        select new QL_BillDetailsViews()
        {
            ID = a.ID,
            IDHD = a.IDHD,
            IDSP = a.IDSP,
            Quantity = a.Quantity,
            Price = a.Price,
            Type = a.Type,
            Image = a.Image,
            Size = a.Size,
            Color = a.Color,
            ProductName = c.Name
        }
        ).ToList();
            return lst;
        }

        public bool UpdateBillDetail(QL_BillDetailsViews p)
        {
            if (p == null)
            {
                return false;
            }
            else
            {
                var BillDetails = _billDetailServices.GetAllBillDetail().FirstOrDefault(c => c.ID == p.ID);
                BillDetails.ID=p.ID;
                BillDetails.IDHD = p.IDHD;
                BillDetails.IDSP = p.IDSP;
                BillDetails.Quantity = p.Quantity;
                BillDetails.Price = p.Price;
                BillDetails.Type = p.Type;
                BillDetails.Image = p.Image;
                BillDetails.Size = p.Size;
                BillDetails.Color = p.Color;
                context.Add(BillDetails);
                return true;
            }
        }
    }
}
