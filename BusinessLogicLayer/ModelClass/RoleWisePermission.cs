using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ModelClass
{
    public class RoleWisePermission
    {
        public List<AspNetRoles> PermissionList { get; set; }
        public System.Guid RoleId { get; set; }
    }
}
