using Assignment.IServices;
using Assignment.Models;

namespace Assignment.Services
{
    public class UserServices: IUserServices
    {
        DbContexts context;
        public UserServices()
        {
            context = new DbContexts();
        }

        public bool CreateUser(User p)
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

        public bool DeleteUser(Guid id)
        {
            try
            {
                var User = context.Users.Find(id);
                context.Remove(User);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<User> GetAllUser()
        {
            return context.Users.ToList();
        }

        public bool UpdateUser(User p)
        {
            try
            {
                var User = context.Users.Find(p.UserID);
                User.Username = p.Username;
                User.Password = p.Password;
                User.Name = p.Name;
                User.Avata = p.Avata;
                User.PhoneNumber = p.PhoneNumber;
                User.RoleID = p.RoleID;
                User.Status = p.Status;
                context.Update(User);
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
