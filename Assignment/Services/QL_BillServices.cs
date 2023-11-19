using Assignment.IServices;
using Assignment.Models;
using Assignment.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services
{
    public class QL_BillServices : IQL_BillServices
    {
        IBillServices _billServices;
        IUserServices _userServices;
        DbContexts context;
        public QL_BillServices()
        {
            context = new DbContexts();
            _billServices = new BillServices();
            _userServices = new UserServices();
        }
        public bool CreateBill(QL_BillViews p)
        {
            if (p == null)
            {
                return false;
            }
            else
            {
                QL_BillViews c = new QL_BillViews()
                {
                    UserID = p.UserID,
                    CreatDate = p.CreatDate,
                    Status = p.Status,
                    Discount = p.Discount,
                    RecipientAddress = p.RecipientAddress,
                    RecipientName = p.RecipientName,
                    RecipientPhoneNumber = p.RecipientPhoneNumber,
                    ShippingFee = p.ShippingFee,
                    TotalAmout = p.TotalAmout
                };
                context.Add(c);
                return true;
            }
        }

        public bool DeleteBill(Guid id)
        {
            if (id == null)
            {
                return false;
            }
            else
            {
                var Remove = _billServices.GetAllBill().FirstOrDefault(c => c.ID == id);
                context.Remove(Remove);
                context.SaveChanges();
                return true;
            }
        }
            public List<QL_BillViews> GetAllBill()
        {
            List<QL_BillViews> lst = new List<QL_BillViews>(
        from a in _billServices.GetAllBill()
        join b in _userServices.GetAllUser() on a.UserID equals b.UserID
        select new QL_BillViews()
        {
            ID = a.ID,
            UserID = a.UserID,
            CreatDate = a.CreatDate,
            Status = a.Status,
            Discount = a.Discount,
            RecipientAddress = a.RecipientAddress,
            RecipientName = a.RecipientName,
            RecipientPhoneNumber = a.RecipientPhoneNumber,
            ShippingFee = a.ShippingFee,
            TotalAmout = a.TotalAmout,
            UserName = b.Username
        }
        ).ToList();
            return lst;
        }

        public bool UpdateBill(QL_BillViews p)
        {
            if (p == null)
            {
                return false;
            }
            else
            {
                var Bill = _billServices.GetAllBill().FirstOrDefault(c => c.ID == p.ID);
                Bill.UserID = p.UserID;
                Bill.CreatDate = p.CreatDate;
                Bill.Status = p.Status;
                Bill.Discount = p.Discount;
                Bill.RecipientAddress = p.RecipientAddress;
                Bill.RecipientName = p.RecipientName;
                Bill.RecipientPhoneNumber = p.RecipientPhoneNumber;
                Bill.ShippingFee = p.ShippingFee;
                Bill.TotalAmout = p.TotalAmout;
                context.Add(Bill);
                return true;
            }
        }
        public List<Bill> GetBillsByDate(DateTime startDate, DateTime endDate)
        {
            var sales = _billServices.GetAllBill()
                .Where(s => s.CreatDate >= startDate && s.CreatDate <= endDate)
                .ToList();

            return sales;
        }
        public List<Bill> GetBillsStatistics()
        {
            // Thực hiện truy vấn để lấy dữ liệu thống kê từ cơ sở dữ liệu
            var statistics = _billServices.GetAllBill()
                .GroupBy(s => new { s.ID, s.CreatDate })
                .Select(g => new Bill
                {
                    CreatDate = g.Key.CreatDate,
                    ID = g.Key.ID,
                    TotalAmout = g.Sum(s => s.TotalAmout)


                })
                .ToList();

            return statistics;
        }
    }
}
