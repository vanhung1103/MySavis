using Assignment.Models;

namespace Assignment.ViewModels
{
    public class QL_UserViews
    {
        public Guid UserID { get; set; }

        public string Username { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Avata { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public int Status { get; set; }
        public virtual List<Bill> Bills { get; set; }
        public virtual Role Role { get; set; }
    }
}
