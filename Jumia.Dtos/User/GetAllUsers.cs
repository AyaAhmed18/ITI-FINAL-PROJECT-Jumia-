using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Dtos.User
{
    public class GetAllUsers
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

    }
}
