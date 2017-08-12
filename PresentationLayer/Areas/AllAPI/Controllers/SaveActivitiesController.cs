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
    public class SaveActivitiesController : ApiController
    {
        SaveUpdateActivities activities = new SaveUpdateActivities();

        
        [PermissionRole("Add/Update Designation")]
        [Route("SaveDesignation")]
        public IHttpActionResult SaveDesignation([FromBody] Designation designation)
        {
            DataBind data = activities.SaveUpdateDesignation(designation);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Insert", data.dataBefore, data.dataAfter);
            return Ok("Successfully added " + designation.DesignationName);
        }

        
        [PermissionRole("Add/Update Section")]
        [Route("SaveSection")]
        public IHttpActionResult SaveSection([FromBody] Section section)
        {
            DataBind data= activities.SaveUpdateSection(section);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Insert", data.dataBefore, data.dataAfter);
            return Ok("Successfully added " + section.SectionName);
        }

        
        [PermissionRole("Add/Update Department")]
        [Route("SaveDepartment")]
        public IHttpActionResult SaveDepartment([FromBody] Department department)
        {
            DataBind data= activities.SaveUpdateDepartment(department);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Insert", data.dataBefore, data.dataAfter);
            return Ok("Successfully added " + department.Name);
        }


        [PermissionRole("Add/Update Branch")]
        [Route("SaveBranch")]
        public IHttpActionResult SaveBranch([FromBody] Branch branch)
        {
            try
            {
                DataBind data = activities.SaveUpdateBranch(branch);
                AuditTrialReport.SaveAuditReport(User.Identity.Name, "Insert", data.dataBefore, data.dataAfter);
            }
            catch
            {
                return BadRequest("Swif code already assigned");
            }
            
            return Ok("Successfully added " + branch.BranchName);
        }


        [PermissionRole("Add/Update Customer")]
        [Route("SaveCustomer")]
        public IHttpActionResult SaveCustomer([FromBody] Customer customer)
        {
            DataBind data= activities.SaveUpdateCustomer(customer);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Insert", data.dataBefore, data.dataAfter);
            return Ok("Successfully added " + customer.AccHolderName);
        }


        [PermissionRole("Add Transaction")]
        [Route("SaveTransaction")]
        public IHttpActionResult SaveTransaction([FromBody] Transaction transaction)
        {
            transaction.EmployeeId = User.Identity.Name;
            DataBind data = activities.SaveUpdateTransaction(transaction);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Transaction", data.dataBefore, data.dataAfter);
            return Ok("Successfully transaction");
        }


        [PermissionRole("Add/Update Company Group")]
        [Route("SaveGroup")]
        public IHttpActionResult SaveGroup([FromBody] Group group)
        {

            DataBind data = activities.SaveUpdateGroup(group);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Insert", data.dataBefore, data.dataAfter);
            return Ok("Successfully added" + group.GroupName);
        }

        [PermissionRole("Add/Update Role")]
        [Route("SaveRoles")]
        public IHttpActionResult SaveRoles([FromBody] Roles role)
        {
            DataBind data = activities.SaveUpdateRole(role);
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Insert", data.dataBefore, data.dataAfter);
            return Ok("Successfully added" + role.RoleName);
        }

        [Authorize(Roles = "Admin")]
        [Route("SaveUserRole")]
        public IHttpActionResult SaveUserRole([FromBody] UserRoles userRole)
        {
            activities.SaveUpdateUserRole(userRole);
            return Ok("Successfully added");
        }

        [Authorize(Roles = "Admin")]
        [Route("SaveRolePermission")]
        public IHttpActionResult SaveRolePermission([FromBody] RoleWisePermission rolePermission)
        {
            activities.SaveUpdateRolePermission(rolePermission);
            return Ok("Successfully added Role Permission");
        }
    }
}
