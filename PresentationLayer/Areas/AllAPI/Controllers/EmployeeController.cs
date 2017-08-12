using BusinessLogicLayer.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PresentationLayer.Areas.AllAPI.Controllers
{
    [RoutePrefix("api/Activities")]
    public class EmployeeController : ApiController
    {
        GetActivities employee = new GetActivities();

        [Route("GetEmployeeList")]
        public IHttpActionResult GetEmployee()
        {
            return Ok(employee.GetEmployeeList());
        }


        [Route("GetSectionList")]
        public IHttpActionResult GetSectionList()
        {
            return Ok(employee.GetSectionList());
        }


        [Route("GetDepartmentList")]
        public IHttpActionResult GetDepartmentList()
        {
            return Ok(employee.GetDepartmentList());
        }

        [Route("GetDesignationList")]
        public IHttpActionResult GetDesignationList()
        {
            return Ok(employee.GetDesignationList());
        }

        [Route("GetCompanyList")]
        public IHttpActionResult GetCompanyList()
        {
            return Ok(employee.GetCompanyList());
        }

        [Route("GetCustomerList")]
        public IHttpActionResult GetCustomerList()
        {
            return Ok(employee.GetCustomerList());
        }

        [Route("GetRoleList")]
        public IHttpActionResult GetRoleList()
        {
            return Ok(employee.GetRoleList());
        }

        [Route("GetUserRoleList")]
        public IHttpActionResult GetUserRoleList()
        {
            return Ok(employee.GetUserRoleList());
        }

        [Route("GetPermissionList")]
        public IHttpActionResult GetPermissionList()
        {
            return Ok(employee.GetPermissionList());
        }

        [Route("GetTransactionList")]
        public IHttpActionResult GetTransactionList()
        {
            return Ok(employee.GetTransactionList());
        }

        [Route("GetBranchList")]
        public IHttpActionResult GetBranchList()
        {
            return Ok(employee.GetBranchList());
        }

        [Route("GetAccessRoleList")]
        public IHttpActionResult GetAccessRoleList()
        {
            return Ok(employee.GetAccessRoleList());
        }

        [Route("GetListAccessRole")]
        public IHttpActionResult GetListAccessRole()
        {
            return Ok(employee.GetListOfAccessList());
        }

        
        [Route("GetRoleWisePermission")]
        public IHttpActionResult GetRoleWisePermission()
        {
            return Ok(employee.GetRoleWisePermissionList());
        }

        [Route("GetAuditReport")]
        public IHttpActionResult GetAuditTrialReport()
        {
            return Ok(employee.GetAuditTrialReport());
        }



    }
}
