using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using PresentationLayer.Models;
using PresentationLayer.Providers;
using PresentationLayer.Results;
using System.Linq;
using BusinessLogicLayer.Activities;
using DAL;
using BusinessLogicLayer.ModelClass;
using BusinessLogicLayer.LogicActivities;
using Newtonsoft.Json;
using PresentationLayer.Services;

namespace PresentationLayer.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }



        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Logout", null, null);
            return Ok();
        }



        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Password is not match");
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest("Internal Server Problem");
            }

            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Change Password", null, null);

            return Ok("Password change successfull");
        }



        // POST api/Account/Register
        [PermissionRole("Add/Update Employee")]
        [Route("Register")]
        public async Task<IHttpActionResult> Register([FromBody] RegisterEmployee model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.EmployeeId, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user, "123456789");

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            try
            {
                SaveUpdateActivities activity = new SaveUpdateActivities();
                LogicActivity logic = new LogicActivity();
                DataBind da=activity.SaveUpdateEmployee(new Employee
                {
                    EmployeeId = model.EmployeeId,
                    EmployeeName = model.EmployeeName,
                    DesignationId = model.DesignationId,
                    SectionId = model.SectionId,
                    Email = model.Email,
                    JoiningDate = model.JoiningDate,
                    ReportTo = model.ReportTo,
                    Location = model.Location,
                    GroupName = model.GroupId,
                    RoleId = model.RoleId
                });

                UserManager.AddToRoles(user.Id, model.AccessPermission.ToArray());

                var beforData=logic.getEmployee(model.EmployeeId,model.AccessPermission.ToArray());

                var dataAfter = new { Info=beforData,AccessList= model.AccessPermission };

                string data=JsonConvert.SerializeObject(dataAfter);


                AuditTrialReport.SaveAuditReport(User.Identity.Name,"Insert",null, data);

            }
            catch (Exception ev)
            {
                return BadRequest(ev.ToString());
            }

            return Ok("Successfully added Employee with default password");
        }


        [PermissionRole("Add/Update Employee")]
        [Route("AccountRagistration")]
        public async Task<IHttpActionResult> AccountRegistration([FromBody] RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.EmployeeId, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user, "123456789");

            if (!result.Succeeded)
            {
                return BadRequest("Employee is already registerd");

            }

            var Acc = new { userId = user.Id, EmployeeId = model.EmployeeId, Email = model.Email };

            var dataAfter = JsonConvert.SerializeObject(Acc);

            AuditTrialReport.SaveAuditReport(User.Identity.Name, "Account Register", null, dataAfter);


            return Ok("Successfully account Registration with default password");
        }


        [PermissionRole("Add/Update Designation")]
        [Route("UpdateUserRole")]
        public async Task<IHttpActionResult> UpdateUserRole([FromBody] UserRoles model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Provide Proper Information");
            }

            SaveUpdateActivities activities = new SaveUpdateActivities();

           

            var user = UserManager.FindByName(model.EmployeeId);

            if (user==null)
            {
                return BadRequest("The User is not registered");
            }


            DataBind Roledata = activities.SaveUpdateUserRole(model);
            DataBind data = new DataBind();

            string[] oldRoles = new List<string>(UserManager.GetRoles(user.Id)).ToArray();
            await UserManager.RemoveFromRolesAsync(user.Id, oldRoles);

            var data1 = new { EmployeeId = user.UserName, Role = Roledata.dataBefore, AccessList = oldRoles };
            data.dataBefore = JsonConvert.SerializeObject(data1);
            


            await UserManager.AddToRolesAsync(user.Id, model.PermissionName);

            var data2 = new { EmployeeId = user.UserName, Role = Roledata.dataAfter, AccessList = model.PermissionName };
            data.dataAfter = JsonConvert.SerializeObject(data2);

            AuditTrialReport.SaveAuditReport(User.Identity.Name,"Update",data.dataBefore,data.dataAfter);

            return Ok("Successfully added user permission");
        }

        [Authorize(Roles = "Admin")]
        [Route("BrowserInfo")]
        public IHttpActionResult GetAllResult()
        {

           
            HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;
            string UserAgent = HttpContext.Current.Request.UserAgent;

            BrowserInfo info = new BrowserInfo();


            ENT_TrackingData ret = new ENT_TrackingData()
            {
                IPAddress = HttpContext.Current.Request.UserHostName,
                Browser = bc.Browser + " " + bc.Version,
                DateStamp = DateTime.Now,
                PageViewed = HttpContext.Current.Request.Url.AbsolutePath,
                IsMobileDevice = UserAgent,
                Platform = info.GetUserPlatform()
            };

            return Ok(ret);
        }

        [Authorize(Roles ="Admin")]
        [Route("AddPermission")]
        public async Task<IHttpActionResult> AddPermissionRole([FromBody] RolePermissions roleName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SaveUpdateActivities activities = new SaveUpdateActivities();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName.RoleName));

            if (roleResult.Succeeded)
            {
                return Ok("Successfully added Role Permission");
            }

            return BadRequest("Internal Server Error");
        }


        [Route("GetUserRoleList")]
        public IHttpActionResult GetUserPermissionList()
        {

            dbContext db = new dbContext();
            SaveUpdateActivities activities = new SaveUpdateActivities();

            var permissionList = db.AspNetUsers.Select(s => new
            {
                EmployeeId=s.UserName,
                EmployeeName=db.Employee.Where(p=>p.EmployeeId==s.UserName).Select(o=>o.EmployeeName).FirstOrDefault(),
                RoleInfo=db.Employee.Where(k=>k.EmployeeId==s.UserName).Select(j=>j.Roles),
                PermissionName=s.AspNetRoles.Select(p=>new {
                    Id=p.Id,
                    PermissionName=p.Name
                }).ToList()
            }).ToList();
            return Ok(permissionList);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        private string GetEmployeeName(string userId)
        {
            dbContext db = new dbContext();
            var user = UserManager.FindById(userId);
            return db.Employee.Where(p => p.EmployeeId == user.UserName).Select(s => s.EmployeeName).ToString();
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
