using Assignment.ViewModels;

namespace Assignment.IServices
{
    public interface IQL_UserServices
    {
        public bool CreateUser(QL_UserViews p);
        public bool UpdateUser(QL_UserViews p);
        public bool DeleteUser(Guid id);
        public List<QL_UserViews> GetAllUser();
    }
}
