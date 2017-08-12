using BusinessLogicLayer.Activities;
using BusinessLogicLayer.ModelClass;
using DAL;
using PresentationLayer.Models;
using PresentationLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PresentationLayer.Areas.AllAPI.Controllers
{
    [RoutePrefix("api/Activities")]
    public class UpdateActivitiesController : ApiController
    {
        SaveUpdateActivities activities = new SaveUpdateActivities();


        [PermissionRole("Add/Update Designation")]
        [Route("UpdateDesignation")]
        public IHttpActionResult UpdateDesignation([FromBody] Designation designation)
        {
            DataBind data = activities.SaveUpdateDesignation(designation);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Update", data.dataBefore, data.dataAfter);
            return Ok("Successfully updated " + designation.DesignationName);
        }

        [PermissionRole("Add/Update Section")]
        [Route("UpdateSection")]
        public IHttpActionResult UpdateSection([FromBody] Section section)
        {
            DataBind data = activities.SaveUpdateSection(section);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Update", data.dataBefore, data.dataAfter);
            return Ok("Successfully updated " + section.SectionName);
        }


        [PermissionRole("Add/Update Department")]
        [Route("UpdateDepartment")]
        public IHttpActionResult UpdateDepartment([FromBody] Department department)
        {
            DataBind data = activities.SaveUpdateDepartment(department);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Update", data.dataBefore, data.dataAfter);
            return Ok("Successfully updated " + department.Name);
        }


        [PermissionRole("Add/Update Branch")]
        [Route("UpdateBranch")]
        public IHttpActionResult UpdateBranch([FromBody] Branch branch)
        {
            try
            {
                DataBind data = activities.SaveUpdateBranch(branch);
                AuditTrialReport.SaveAuditReport(User.Identity.Name, "Update", data.dataBefore, data.dataAfter);
            }
            catch
            {
                return BadRequest("Swift Code is already assigned to Others");
            }
            return Ok("Successfully updated " + branch.BranchName);
        }


        [PermissionRole("Add/Update Customer")]
        [Route("UpdateCustomer")]
        public IHttpActionResult UpdateCustomer([FromBody] Customer customer)
        {
            DataBind data=activities.SaveUpdateCustomer(customer);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Update", data.dataBefore, data.dataAfter);
            return Ok("Successfully updated " + customer.AccHolderName);
        }

        [PermissionRole("EmployeeList,Add/UpdateEmployee")]
        [Route("UpdateTransaction")]
        public IHttpActionResult UpdateTransaction([FromBody] Transaction transaction)
        {
            activities.SaveUpdateTransaction(transaction);
            return Ok("Successfully updated transaction");
        }

        [PermissionRole("Add/Update Company Group")]
        [Route("UpdateGroup")]
        public IHttpActionResult UpdateGroup([FromBody] Group group)
        {
            DataBind data = activities.SaveUpdateGroup(group);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Update", data.dataBefore, data.dataAfter);
            return Ok("Successfully updated " + group.GroupName);
        }

        [PermissionRole("Add/Update Role")]
        [Route("UpdateRoles")]
        public IHttpActionResult UpdateRoles([FromBody] Roles role)
        {
            DataBind data = activities.SaveUpdateRole(role);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Insert", data.dataBefore, data.dataAfter);
            return Ok("Successfully updated " + role.RoleName);
        }

        [PermissionRole("Update User Permission")]
        [Route("UpdateUserRole")]
        public IHttpActionResult UpdateUserRole([FromBody] UserRoles userRole)
        {
            activities.SaveUpdateUserRole(userRole);
            return Ok("Successfully updated");
        }

        [PermissionRole("Update Role Permission")]
        [Route("UpdateRolePermission")]
        public IHttpActionResult UpdateRolePermission([FromBody] RoleWisePermission rolePermission)
        {
            DataBind data = activities.SaveUpdateRolePermission(rolePermission);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Update", data.dataBefore, data.dataAfter);
            return Ok("Successfully updated Role Permission");
        }

        [PermissionRole("Add/Update Employee")]
        [Route("UpdateEmployee")]
        public IHttpActionResult UpdateEmployee([FromBody] Employee employee)
        {
            DataBind data=activities.SaveUpdateEmployee(employee);
            AuditTrialReport.SaveAuditReport(User.Identity.Name,"Update",data.dataBefore,data.dataAfter);
            return Ok("Successfully Updated Employee info");
        }
    }
}
