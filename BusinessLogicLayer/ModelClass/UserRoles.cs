using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ModelClass
{
    public class UserRoles
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public System.Guid RoleId { get; set; }
        [Required]
        public string[] PermissionName { get; set; }
    }
}
