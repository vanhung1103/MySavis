using Assignment.Models;

namespace Assignment.IServices
{
    public interface IRoleServices
    {
        public bool CreateRole(Role p);
        public bool UpdateRole(Role p);
        public bool DeleteRole(Guid id);
        public List<Role> GetAllRole();
    }
}
