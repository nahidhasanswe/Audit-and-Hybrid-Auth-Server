using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class RegisterEmployee
    {
        
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "EmployeeId")]
        public string EmployeeId { get; set; }

        [Required]
        [Display(Name = "EmployeeName")]
        public string EmployeeName { get; set; }


        [Required]
        [Display(Name = "AccessPermission")]
        public List<string> AccessPermission { get; set; }

        [Required]
        [Display(Name = "ReportTo")]
        public string ReportTo { get; set; }

        
        [Display(Name = "JoiningDate")]
        public System.DateTime JoiningDate { get; set; }

        [Required]
        [Display(Name = "DesignationId")]
        public System.Guid DesignationId { get; set; }

        [Required]
        [Display(Name = "SectionId")]
        public System.Guid SectionId { get; set; }


        [Display(Name = "GroupId")]
        public string GroupId { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "RoleId")]
        public System.Guid RoleId { get; set; }

    }
}