using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RolePermission
    {
        public string PermissionId { get; set; }
        public System.Guid RoleId { get; set; }

        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }
    }
}
