using Assignment.Models;

namespace Assignment.IServices
{
    public interface IUserServices
    {
        public bool CreateUser(User p);
        public bool UpdateUser(User p);
        public bool DeleteUser(Guid id);
        public List<User> GetAllUser();
    }
}
