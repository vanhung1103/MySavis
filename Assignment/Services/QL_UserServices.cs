using Assignment.IServices;
using Assignment.Models;
using Assignment.ViewModels;

namespace Assignment.Services
{
    public class QL_UserServices:IQL_UserServices
    {
        IUserServices _userServices;
        IRoleServices _roleServices;
        DbContexts context;
        public QL_UserServices()
        {
            _userServices = new UserServices();
            _roleServices = new RoleServices();
            context = new DbContexts();
        }
        public bool CreateUser(QL_UserViews p)
        {
            if (p == null)
            {
                return false;
            }
            else
            {
                var GetRoleID = _roleServices.GetAllRole().FirstOrDefault(c => c.RoleName == p.RoleName).RoleID;
                User c = new User()
                {
                    Username = p.Username,
                    Name = p.Name,
                    PhoneNumber = p.PhoneNumber,
                    Avata = p.Avata,
                    Password = p.Password,
                    RoleID = GetRoleID,
                    Status = p.Status,

                };
                context.Add(c);
                return true;
            }
        }

        public bool DeleteUser(Guid id)
        {
            if (id == null)
            {
                return false;
            }
            else
            {
                var Remove = _userServices.GetAllUser().FirstOrDefault(c => c.UserID == id);
                context.Remove(Remove);
                context.SaveChanges();
                return true;
            }
        }

        public List<QL_UserViews> GetAllUser()
        {
            List<QL_UserViews> lst = new List<QL_UserViews>(
                from a in _userServices.GetAllUser()
                join b in _roleServices.GetAllRole() on a.RoleID equals b.RoleID
                select new QL_UserViews()
                {
                    UserID = a.UserID,
                    Username = a.Username,
                    Name = a.Name,
                    PhoneNumber = a.PhoneNumber,
                    Avata = a.Avata,
                    Password = a.Password,
                    Status = a.Status,
                    //RoleID = a.RoleID,
                    RoleName = b.RoleName
                }
                ).ToList();
            return lst;
        }

        public bool UpdateUser(QL_UserViews p)
        {
            if (p == null)
            {
                return false;
            }
            else
            {
                var User = _userServices.GetAllUser().FirstOrDefault(c => c.UserID == p.UserID);
                var GetRoleID = _roleServices.GetAllRole().FirstOrDefault(c => c.RoleName == p.RoleName).RoleID;
                User.Username = p.Username;
                User.Name = p.Name;
                User.PhoneNumber = p.PhoneNumber;
                User.Avata = p.Avata;
                User.Password = p.Password;
                User.Status = p.Status;
                User.RoleID = GetRoleID;
                context.Update(User);
                return true;
            }
        }
    }
}
