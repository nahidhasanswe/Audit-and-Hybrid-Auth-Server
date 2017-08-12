using DAL;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.ModelClass;
using System.Data.Entity.Migrations;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace BusinessLogicLayer.Activities
{
    public class SaveUpdateActivities
    {
        DataBind dataBind=new DataBind();
        dbContext db = new dbContext();
        public DataBind SaveUpdateEmployee(Employee employee)
        {
            var isExist = db.Employee.Where(e => e.EmployeeId == employee.EmployeeId).ToList();
            if (!isExist.Any())
            {
                db.Employee.Add(employee);
                db.SaveChanges();
                DataBind da= SaveUpdateUserRole(new UserRoles
                {
                    RoleId = employee.RoleId,
                    EmployeeId = employee.EmployeeId
                });

                return new DataBind();
            }
            else
            {
                var databefore = db.Employee.Where(s => s.EmployeeId == employee.EmployeeId).Select(p => new
                {
                    EmployeeId = p.EmployeeId,
                    EmployeeName = p.EmployeeName,
                    Email = p.Email,
                    Designation = p.Designation.DesignationName,
                    Section = p.Section.SectionName,
                    Department = p.Section.Department.Name,
                    JoinDate = p.JoiningDate,
                    Location = p.Location,
                    Role = p.Roles.FirstOrDefault().RoleName
                    }).FirstOrDefault();

                var data1 = JsonConvert.SerializeObject(databefore);
                db.Employee.AddOrUpdate(employee);
                //db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();

                var dataAfter = db.Employee.Where(s => s.EmployeeId == employee.EmployeeId).Select(p => new
                {
                    EmployeeId = p.EmployeeId,
                    EmployeeName = p.EmployeeName,
                    Email=p.Email,
                    Designation = p.Designation.DesignationName,
                    Section = p.Section.SectionName,
                    Department = p.Section.Department.Name,
                    JoinDate = p.JoiningDate,
                    Location = p.Location,
                    Role = p.Roles.FirstOrDefault().RoleName
                }).FirstOrDefault();
                var data2 = JsonConvert.SerializeObject(dataAfter);

                DataBind data = new DataBind {dataBefore= data1,dataAfter=data2 };
                db.Dispose();

                return data;
            }
        }

        public DataBind SaveUpdateDepartment(Department department)
        {
            if (department.Id==Guid.Empty)
            {
                department.Id = Guid.NewGuid();
                db.Department.Add(department);
                db.SaveChanges();
                var data = new{Id=department.Id,DepartmentName=department };
                var data1 = JsonConvert.SerializeObject(data);

                dataBind.dataAfter = data1;
            }
            else
            {
                var databefore = db.Department.Where(p => p.Id == department.Id).Select(s => new {
                    Id=s.Id,
                    DepartmentName=s.Name
                });
                var data1 = JsonConvert.SerializeObject(databefore);

                db.Department.AddOrUpdate(department);
                db.SaveChanges();

                var dataafter = db.Department.Where(p => p.Id == department.Id).Select(s => new {
                    Id = s.Id,
                    DepartmentName = s.Name
                });
                var data2 = JsonConvert.SerializeObject(dataafter);

                dataBind.dataBefore = data1;
                dataBind.dataAfter = data2;

            }

           
            db.Dispose();

            return dataBind;

        }

        public DataBind SaveUpdateSection(Section section)
        {
            if (section.Id == Guid.Empty)
            {
                section.Id = Guid.NewGuid();
                db.Section.Add(section);
                db.SaveChanges();

                var data = db.Section.Where(s => s.Id == section.Id).Select(p => new {
                    Id=p.Id,
                    SectionName=p.SectionName,
                    DepartmentName=p.Department.Name
                });

                dataBind.dataAfter = JsonConvert.SerializeObject(data);

            }
            else
            {
                var data1 = db.Section.Where(s => s.Id == section.Id).Select(p => new {
                    Id = p.Id,
                    SectionName = p.SectionName,
                    DepartmentName = p.Department.Name
                });
                dataBind.dataBefore = JsonConvert.SerializeObject(data1);

                db.Section.AddOrUpdate(section);
                db.SaveChanges();

                var data2 = db.Section.Where(s => s.Id == section.Id).Select(p => new {
                    Id = p.Id,
                    SectionName = p.SectionName,
                    DepartmentName = p.Department.Name
                });
                dataBind.dataAfter = JsonConvert.SerializeObject(data2);

            }
            db.Dispose();

            return dataBind;
        }

        public DataBind SaveUpdateDesignation(Designation designation)
        {
            if (designation.Id == Guid.Empty)
            {
                designation.Id = Guid.NewGuid();
                db.Designation.Add(designation);
                db.SaveChanges();

                var data1 = db.Designation.Where(s => s.Id == designation.Id).Select(p=>new {
                    Id=p.Id,
                    DesignationName=p.DesignationName
                });

                dataBind.dataAfter = JsonConvert.SerializeObject(data1);
            }
            else
            {
                var data1 = db.Designation.Where(s => s.Id == designation.Id).Select(p => new {
                    Id = p.Id,
                    DesignationName = p.DesignationName
                });

                dataBind.dataBefore = JsonConvert.SerializeObject(data1);

                db.Designation.AddOrUpdate(designation);
                db.SaveChanges();

                var data2 = db.Designation.Where(s => s.Id == designation.Id).Select(p => new {
                    Id = p.Id,
                    DesignationName = p.DesignationName
                });

                dataBind.dataAfter = JsonConvert.SerializeObject(data2);
            }

            db.Dispose();

            return dataBind;

        }

        public DataBind SaveUpdateBranch(Branch branch)
        {
            if (branch.Id == Guid.Empty)
            {

                branch.Id = Guid.NewGuid();
                db.Branch.Add(branch);
                db.SaveChanges();

                var data1 = db.Branch.Where(s => s.Id == branch.Id).Select(p => new {
                    Id = p.Id,
                    SwiftCode=p.SwiftCode,
                    BranchName = p.BranchName,
                    Address=p.Address
                });

                dataBind.dataAfter = JsonConvert.SerializeObject(data1);

            }
            else
            {
                var data1 = db.Branch.Where(s => s.Id == branch.Id).Select(p => new {
                    Id = p.Id,
                    SwiftCode = p.SwiftCode,
                    BranchName = p.BranchName,
                    Address = p.Address
                });

                dataBind.dataBefore = JsonConvert.SerializeObject(data1);

                db.Branch.AddOrUpdate(branch);
                db.SaveChanges();

                var data2 = db.Branch.Where(s => s.Id == branch.Id).Select(p => new {
                    Id = p.Id,
                    SwiftCode = p.SwiftCode,
                    BranchName = p.BranchName,
                    Address = p.Address
                });

                dataBind.dataAfter = JsonConvert.SerializeObject(data2);

            }

           
            db.Dispose();

            return dataBind;
        }

        public DataBind SaveUpdateGroup(Group group)
        {
            var isExist = db.Group.Where(g => g.Id == group.Id);

            if (isExist.Any())
            {
                var data1 = db.Group.Where(s => s.Id == group.Id).Select(p => new {
                    ShortName = p.Id,
                    FullName=p.GroupName
                });

                dataBind.dataBefore = JsonConvert.SerializeObject(data1);

                db.Group.AddOrUpdate(group);
                db.SaveChanges();

                var data2 = db.Group.Where(s => s.Id == group.Id).Select(p => new {
                    ShortName = p.Id,
                    FullName = p.GroupName
                });

                dataBind.dataAfter = JsonConvert.SerializeObject(data2);
            }
            else
            {
                db.Group.Add(group);
                db.SaveChanges();

                var data1 = db.Group.Where(s => s.Id == group.Id).Select(p => new {
                    ShortName = p.Id,
                    FullName = p.GroupName
                });

                dataBind.dataAfter = JsonConvert.SerializeObject(data1);
            }

            
            db.Dispose();

            return dataBind;
        }

        public DataBind SaveUpdateRole(Roles role)
        {
            if (role.Id == Guid.Empty)
            {
                role.Id = Guid.NewGuid();
                db.Roles.Add(role);
                db.SaveChanges();

                var data1 = db.Roles.Where(s => s.Id == role.Id).Select(p => new {
                    Id=p.Id,
                    RoleName=p.RoleName
                });

                dataBind.dataAfter = JsonConvert.SerializeObject(data1);
            }
            else
            {
                var data1 = db.Roles.Where(s => s.Id == role.Id).Select(p => new {
                    Id = p.Id,
                    RoleName = p.RoleName
                });

                dataBind.dataBefore = JsonConvert.SerializeObject(data1);

                db.Roles.AddOrUpdate(role);
                db.SaveChanges();

                var data2 = db.Roles.Where(s => s.Id == role.Id).Select(p => new {
                    Id = p.Id,
                    RoleName = p.RoleName
                });

                dataBind.dataAfter = JsonConvert.SerializeObject(data2);

            }

            db.Dispose();

            return dataBind;

        }

        public DataBind SaveUpdateTransaction(Transaction transaction)
        {
            if (transaction.TransactionId == Guid.Empty)
            {
                transaction.TransactionId = Guid.NewGuid();
                transaction.DatetimeStamp = DateTime.Now;
                transaction.BranchId = db.Customer.Where(p => p.AccountNo == transaction.AccNo).Select(s => s.BranchId).FirstOrDefault();
                db.Transaction.Add(transaction);
                db.SaveChanges();

                var data1 = db.Transaction.Where(s => s.TransactionId == transaction.TransactionId).Select(p => new {
                    TransactionId=p.TransactionId,
                    TransactionAcc=p.AccNo,
                    TransactionTime=p.DatetimeStamp,
                    Type=p.TransactionType,
                    Balance=p.DebitCreditBalance
                });

                dataBind.dataAfter = JsonConvert.SerializeObject(data1);

            }
            else
            {
                db.Transaction.AddOrUpdate(transaction);
            }

            db.Dispose();

            return dataBind;
        }

        public DataBind SaveUpdateCustomer(Customer customer)
        {
            DataBind data;

            var isExists = db.Customer.Where(c => c.AccountNo == customer.AccountNo).ToList();
            if (!isExists.Any())
            {
                db.Customer.Add(customer);
                db.SaveChanges();
                var dataafter = db.Customer.Where(s => s.AccountNo == customer.AccountNo).Select(p => new {
                    AccountNo=p.AccountNo,
                    AccHolderName=p.AccHolderName,
                    Balance=p.Balance,
                    Address=p.Address,
                    Branch=p.Branch.BranchName
                });

                data = new DataBind { dataBefore = null, dataAfter = OtoJ(dataafter) };
            }
            else
            {
                var dataBefore = db.Customer.Where(s => s.AccountNo == customer.AccountNo).Select(p => new {
                    AccountNo = p.AccountNo,
                    AccHolderName = p.AccHolderName,
                    Balance = p.Balance,
                    Address = p.Address,
                    Branch = p.Branch.BranchName
                });

                var data1 = JsonConvert.SerializeObject(dataBefore);
                db.Customer.AddOrUpdate(customer);
                db.SaveChanges();

                var dataAfter = db.Customer.Where(s => s.AccountNo == customer.AccountNo).Select(p => new {
                    AccountNo = p.AccountNo,
                    AccHolderName = p.AccHolderName,
                    Balance = p.Balance,
                    Address = p.Address,
                    Branch = p.Branch.BranchName
                });
                var data2 = JsonConvert.SerializeObject(dataBefore);

                data = new DataBind { dataBefore =data1, dataAfter =data2};

            }

            
            db.Dispose();

            return data;
        }

        public DataBind SaveUpdateRolePermission(RoleWisePermission rolePermissionList)
        {
            var RoleObject = db.Roles.Where(s => s.Id == rolePermissionList.RoleId).FirstOrDefault();

            if (RoleObject.AspNetRoles.Any())
            {
                

                List<string> access=new List<string>();
                foreach (var rolePermission in RoleObject.AspNetRoles.ToList())
                {
                    RoleObject.AspNetRoles.Remove(rolePermission);
                    access.Add(rolePermission.Name);
                }

                var data1 = new { RoleName = RoleObject.RoleName, AccessList = access.ToArray()};
                dataBind.dataBefore = JsonConvert.SerializeObject(data1);
            }


            List<string> access1 = new List<string>();
            foreach (var Role in rolePermissionList.PermissionList.Distinct())
            {
                RoleObject.AspNetRoles.Add(db.AspNetRoles.Where(p => p.Id == Role.Id).FirstOrDefault());
                access1.Add(Role.Name);
            }

            var data2 = new { RoleName = RoleObject.RoleName, AccessList = access1.ToArray() };
            dataBind.dataAfter = JsonConvert.SerializeObject(data2);


            db.SaveChanges();
            db.Dispose();

            return dataBind;

        }

        public DataBind SaveUpdateUserRole(UserRoles userRole)
        {
            var Employee = db.Employee.Where(s => s.EmployeeId == userRole.EmployeeId).FirstOrDefault();
            if (Employee.Roles.Any())
            {
                var oldRole = Employee.Roles.FirstOrDefault();
                Employee.Roles.Remove(oldRole);

                var RoleIn = new { Id = oldRole.Id, RoleName = oldRole.RoleName };

                dataBind.dataBefore = JsonConvert.SerializeObject(RoleIn.RoleName);
            }

            var newRole = db.Roles.Where(p => p.Id == userRole.RoleId).FirstOrDefault();
            Employee.Roles.Add(newRole);

            var RoleInfo = new { Id = newRole.Id, RoleName = newRole.RoleName };

            dataBind.dataAfter = JsonConvert.SerializeObject(RoleInfo.RoleName);

            db.SaveChanges();
            db.Dispose();

            return dataBind;

        }

        public string OtoJ(dynamic obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }

    public class DataBind
    {
        public string dataBefore { get; set; }
        public string dataAfter { get; set; }
    }
}
