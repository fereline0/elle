using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elle.Models
{
    public class User : BaseEntity
    {
        public enum RoleType
        {
            User,
            Admin,
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; } = RoleType.User;
    }
}
