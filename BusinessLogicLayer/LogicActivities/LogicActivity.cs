using BusinessLogicLayer.Activities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.LogicActivities
{
    public class LogicActivity
    {
        dbContext db = new dbContext();

        public string getUserRoleName(string userId)
        {
            return db.Employee.Where(p => p.EmployeeId == userId).Select(s => s.Roles.Select(o => o.RoleName)).FirstOrDefault().ToString();
        }

        public string getUserName(string userId)
        {
            return db.Employee.Where(p => p.EmployeeId == userId).Select(s => s.EmployeeName).FirstOrDefault().ToString();
        }

        public string[] getRolewisePermission(System.Guid RoleId)
        {
            GetActivities activity = new GetActivities();

            var RoleList = db.AspNetRoles.Where(s => s.Roles.Any(p => p.Id == RoleId)).Select(o=>o.Name).ToArray();

            return RoleList;
        }

        public object getEmployee(string eid,string[] permissionList)
        {
            return db.Employee.Where(s => s.EmployeeId == eid).Select(p => new
            {
                EmployeeId=eid,
                EmployeeName=p.EmployeeName,
                Email=p.Email,
                Designation=p.Designation.DesignationName,
                Section=p.Section.SectionName,
                Department=p.Section.Department.Name,
                JoinDate=p.JoiningDate,
                Location=p.Location,
                Role=p.Roles.Select(e=>e.RoleName).FirstOrDefault()
            }).FirstOrDefault();
        }
    }
}
