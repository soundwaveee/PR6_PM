using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace butakov_pr3.Models
{
    public class UserModel
    {
        public int IdUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}