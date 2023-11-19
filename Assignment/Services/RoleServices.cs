using Assignment.IServices;
using Assignment.Models;

namespace Assignment.Services
{
    public class RoleServices: IRoleServices
    {
        DbContexts context;
        public RoleServices()
        {
            context = new DbContexts();
        }

        public bool CreateRole(Role p)
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

        public bool DeleteRole(Guid id)
        {
            try
            {
                var Role = context.Roles.Find(id);
                context.Remove(Role);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Role> GetAllRole()
        {
            return context.Roles.ToList();
        }

        public bool UpdateRole(Role p)
        {
            try
            {
                var Role = context.Roles.Find(p.RoleID);
                Role.RoleName = p.RoleName;
                Role.Description = p.Description;
                Role.Description = p.Description;
                Role.Status = p.Status;
                context.Update(Role);
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
