//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuditReport
    {
        public System.Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string OperationType { get; set; }
        public string BeforeChange { get; set; }
        public string AfterChange { get; set; }
        public Nullable<System.DateTime> DateTimeStamp { get; set; }
        public string Browser { get; set; }
        public string OS { get; set; }
        public string RequestedPath { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
