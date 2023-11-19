using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Assignment.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        [Display(Name="UserName")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Nhập chữ hoặc số cho {0}")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Phải dài {2} cho{0}")]
        public string Username { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Avata { get; set; }
        public string Password { get; set; }
        public Guid RoleID { get; set; }
        public int Status { get; set; }
        public virtual List<Bill> Bills { get; set; }
        public virtual Role Role { get; set; }

    }
}
