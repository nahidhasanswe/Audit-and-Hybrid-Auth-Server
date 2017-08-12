using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Activities
{
    public class GetActivities
    {
        dbContext db = new dbContext();
        public object GetEmployeeList()
        {
            var EmployeeList = db.Employee.Select(s => new {
                EmployeeId=s.EmployeeId,
                EmployeeName=s.EmployeeName,
                Email=s.Email,
                DesignationId=s.DesignationId,
                Designation=s.Designation.DesignationName,
                SectionId=s.SectionId,
                Section=s.Section.SectionName,
                DepartmentId=s.Section.DepartmentId,
                Department=s.Section.Department.Name,
                ReportToId=s.Employee2.EmployeeId,
                ReportToName = s.Employee2.EmployeeName,
                JoiningDate =s.JoiningDate,
                Location=s.Location,
                GroupId=s.GroupName,
                GroupName=s.Group.GroupName
            }).ToList();

            return EmployeeList;
        }

        public object GetSectionList()
        {
            var SectionList = db.Section.Select(s => new
            {
                SectionId = s.Id,
                SectionName = s.SectionName,
                DepartmentId = s.Department.Id,
                DepartmentName = s.Department.Name
            }).ToList();

            return SectionList;
        }

        public object GetDepartmentList()
        {
            var DepartmentList = db.Department.Select(s => new
            {
                DepartmentId = s.Id,
                DepartmentName = s.Name,
            }).ToList();

            return DepartmentList;
        }

        public object GetDesignationList()
        {
            var DesignationList = db.Designation.Select(s => new
            {
                DesignationId = s.Id,
                DesignationName = s.DesignationName,
            }).ToList();

            return DesignationList;
        }

        public object GetCompanyList()
        {
            var CompanyList = db.Group.Select(g=>new {
                Id=g.Id,
                GroupName = g.GroupName
            }).ToList();

            return CompanyList;
        }

        public object GetCustomerList()
        {
            var CustomerList = db.Customer.Select(s => new
            {
                AccountNo=s.AccountNo,
                AccHolderName=s.AccHolderName,
                Balance=s.Balance,
                Address=s.Address,
                BranchId=s.BranchId,
                Branch=s.Branch.BranchName ?? null
            }).ToList();

            return CustomerList;
        }

        public object GetRoleList()
        {
            var RoleList = db.Roles.Select(s => new
            {
                RoleId=s.Id,
                RoleName=s.RoleName
            }).ToList();

            return RoleList;
        }

        public object GetUserRoleList()
        {
            var UserRoleList = db.Employee.Select(s => new
            {
                EmployeeId = s.EmployeeId,
                EmployeeName = s.EmployeeName,
                RoleId = s.Roles.Where(r => r.Employee.Contains(s)).Select(p => p.Id).FirstOrDefault(),
                RoleName = s.Roles.Where(r => r.Employee.Contains(s)).Select(p => p.RoleName).FirstOrDefault(),
                AccessList = db.AspNetUsers.Where(p => p.UserName == s.EmployeeId).FirstOrDefault().AspNetRoles.Select(r=>new {
                                Id=r.Id,
                                Name=r.Name
                })
            }).ToList();

            return UserRoleList;
        }

        public object GetPermissionList()
        {
            var PermissionList = db.Roles.Select(s => new
            {
                RoleId=s.Id,
                RoleName=s.RoleName,
                PermissionNameList=db.AspNetRoles.Where(p=>p.Roles.Contains(s)).Select(n=>n.Name.ToString())
            }).ToList();

            return PermissionList;
        }

        public object GetTransactionList()
        {
            var TransactionList = db.Transaction.Select(s => new
            {
                TransactionId = s.TransactionId,
                AccountNo = s.AccNo,
                HolderName=s.Customer.AccHolderName,
                TransactionType = s.TransactionType,
                Balance = s.DebitCreditBalance,
                TransactionTime = s.DatetimeStamp,
                TransactionById=s.EmployeeId,
                TransactionByName =s.Employee.EmployeeName,
                Branch=s.Branch.BranchName,
            }).OrderByDescending(p=>p.TransactionTime).ToList();

            return TransactionList;
        }

        public object GetBranchList()
        {
            var BranchList = db.Branch.Select(s => new
            {
                BranchId=s.Id,
                BranchName = s.BranchName,
                Address = s.Address,
                SwiftCode = s.SwiftCode
            }).OrderBy(d=>d.BranchName).ToList();

            return BranchList;
        }

        public object GetAccessRoleList()
        {
            return db.AspNetRoles.Select(s => s.Name).ToArray();
        }

        public object GetListOfAccessList()
        {
            return db.AspNetRoles.Select(s => new
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }

        public object GetRoleWisePermissionList()
        {
            var PermissionList = db.Roles.Select(s => new
            {
                RoleId = s.Id,
                RoleName = s.RoleName,
                PermissionNameList = s.AspNetRoles.Select(p=>new {
                    Id=p.Id,
                    Name=p.Name
                }).ToList()
            }).ToList();

            return PermissionList;
        }

        public object GetAuditTrialReport()
        {
            return db.AuditReport.Select(s => new
            {
                Id=s.Id,
                EmployeeId=s.EmployeeId,
                EmployeeName=s.Employee.EmployeeName,
                IpAddress=s.IpAddress,
                MacAddress=s.MacAddress,
                OperationType=s.OperationType,
                TransactionTime=s.DateTimeStamp,
                Browser=s.Browser,
                Platform=s.OS,
                PageView=s.RequestedPath,
                BeforeData=s.BeforeChange,
                AfterData=s.AfterChange
            }).OrderByDescending(k=>k.TransactionTime).ToList();
        }
    }
}
