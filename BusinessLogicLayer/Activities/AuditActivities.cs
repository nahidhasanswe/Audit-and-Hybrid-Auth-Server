using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Activities
{
    public class AuditActivities
    {
        dbContext db = new dbContext();
        public void SaveActivity(AuditReport report)
        {
            report.Id = Guid.NewGuid();
            db.AuditReport.Add(report);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
