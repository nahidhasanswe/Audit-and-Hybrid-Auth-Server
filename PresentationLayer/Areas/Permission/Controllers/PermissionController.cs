using PresentationLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PresentationLayer.Areas.Permission.Controllers
{
    [Authorize]
    [RoutePrefix("api/Permission")]
    public class PermissionController : ApiController
    {
        [PermissionRole("Employee List,Add/Update Employee")]
        [HttpGet]
        [Route("EmployeeList")]
        public IHttpActionResult EmployeeList()
        {
            return Ok();
        }

        //[Authorize(Roles = "Customer List,Add/Update Customer")]
        [PermissionRole("Customer List,Add/Update Customer")]
        [Route("CustomerList")]
        [HttpGet]
        public IHttpActionResult CustomerList()
        {
            return Ok();
        }

        //[Authorize(Roles = "Transaction List,Add Transaction")]
        [PermissionRole("Transaction List,Add Transaction")]
        [HttpGet]
        [Route("TransactionList")]
        public IHttpActionResult TransactionList()
        {
            return Ok();
        }

        //[Authorize(Roles = "Branch List,Add/Update Branch")]
        [PermissionRole("Branch List,Add/Update Branch")]
        [HttpGet]
        [Route("BranchList")]
        public IHttpActionResult BranchList()
        {
            return Ok();
        }

        //[Authorize(Roles = "Department List,Add/Update Department")]
        [PermissionRole("Department List,Add/Update Department")]
        [HttpGet]
        [Route("DepartmentList")]
        public IHttpActionResult DepartmentList()
        {
            return Ok();
        }

        //[Authorize(Roles = "Employee List,Add/Update Employee")]
        [PermissionRole("Employee List,Add/Update Department")]
        [HttpGet]
        [Route("DesignationList")]
        public IHttpActionResult DesignationList()
        {
            return Ok();
        }

        //[Authorize(Roles = "Section List,Add/Update Section")]
        [PermissionRole("Section List,Add/Update Section")]
        [HttpGet]
        [Route("SectionList")]
        public IHttpActionResult SectionList()
        {
            return Ok();
        }

        //[Authorize(Roles = "Role List,Add/Update Role")]
        [PermissionRole("Role List,Add/Update Role")]
        [HttpGet]
        [Route("RoleList")]
        public IHttpActionResult RoleList()
        {
            return Ok();
        }

        //[Authorize(Roles = "Employee Role,Update User Permission,User Role List")]
        [PermissionRole("Employee Role,Update User Permission,User Role List")]
        [HttpGet]
        [Route("UserRoleList")]
        public IHttpActionResult UserRoleList()
        {
            return Ok();
        }


       // [Authorize(Roles = "Role Wise Permission List,Update Role Permission")]
        [PermissionRole("Role Wise Permission List,Update Role Permission")]
        [HttpGet]
        [Route("PermissionList")]
        public IHttpActionResult PermissionList()
        {
            return Ok();
        }


        //[Authorize(Roles = "Access Permission List")]
        [PermissionRole("Access Permission List")]
        [HttpGet]
        [Route("AccessRoleList")]
        public IHttpActionResult AccessRoleList()
        {
            return Ok();
        }


        //[Authorize(Roles = "Customer List,Add/Update Customer")]
        [PermissionRole("Customer List,Add/Update Customer")]
        [HttpGet]
        [Route("CompanyList")]
        public IHttpActionResult CompanyList()
        {
            return Ok();
        }


        //[Authorize(Roles = "Audit Trial Report")]
        [PermissionRole("Audit Trial Report")]
        [HttpGet]
        [Route("AuditTrial")]
        public IHttpActionResult AuditTrial()
        {
            return Ok();
        }
    }
}
