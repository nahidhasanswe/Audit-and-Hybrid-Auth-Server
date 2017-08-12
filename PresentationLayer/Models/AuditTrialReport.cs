using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using BusinessLogicLayer.Activities;

namespace PresentationLayer.Models
{
    public class AuditTrialReport
    {
        public static  void SaveAuditReport(string userId,string OperationName,string beforeChange,string afterChange)
        {
            HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;

            AuditReport report = new AuditReport {
                EmployeeId = userId,
                IpAddress = HttpContext.Current.Request.UserHostAddress,
                MacAddress = GetMACAddress(),
                OS = GetUserPlatform(),
                OperationType=OperationName,
                Browser = bc.Browser + " " + bc.Version,
                RequestedPath= HttpContext.Current.Request.Url.AbsolutePath,
                DateTimeStamp=System.DateTime.Now,
                BeforeChange=beforeChange,
                AfterChange=afterChange
            };

            AuditActivities activity = new AuditActivities();
            activity.SaveActivity(report);

        }

        public static string GetUserPlatform()
        {
            var ua = HttpContext.Current.Request.UserAgent;

            //var ua = request.UserAgent;

            if (ua.Contains("Android"))
                return string.Format("Android {0}", GetMobileVersion(ua, "Android"));

            if (ua.Contains("iPad"))
                return string.Format("iPad OS {0}", GetMobileVersion(ua, "OS"));

            if (ua.Contains("iPhone"))
                return string.Format("iPhone OS {0}", GetMobileVersion(ua, "OS"));

            if (ua.Contains("Linux") && ua.Contains("KFAPWI"))
                return "Kindle Fire";

            if (ua.Contains("RIM Tablet") || (ua.Contains("BB") && ua.Contains("Mobile")))
                return "Black Berry";

            if (ua.Contains("Windows Phone"))
                return string.Format("Windows Phone {0}", GetMobileVersion(ua, "Windows Phone"));

            if (ua.Contains("Mac OS"))
                return "Mac OS";

            if (ua.Contains("Windows NT 5.1") || ua.Contains("Windows NT 5.2"))
                return "Windows XP";

            if (ua.Contains("Windows NT 6.0"))
                return "Windows Vista";

            if (ua.Contains("Windows NT 6.1"))
                return "Windows 7";

            if (ua.Contains("Windows NT 6.2"))
                return "Windows 8";

            if (ua.Contains("Windows NT 6.3"))
                return "Windows 8.1";

            if (ua.Contains("Windows NT 10"))
                return "Windows 10";

            //fallback to basic platform:
            return HttpContext.Current.Request.Browser.Platform + (ua.Contains("Mobile") ? " Mobile " : "");
        }

        public static String GetMobileVersion(string userAgent, string device)
        {
            var temp = userAgent.Substring(userAgent.IndexOf(device) + device.Length).TrimStart();
            var version = string.Empty;

            foreach (var character in temp)
            {
                var validCharacter = false;
                int test = 0;

                if (Int32.TryParse(character.ToString(), out test))
                {
                    version += character;
                    validCharacter = true;
                }

                if (character == '.' || character == '_')
                {
                    version += '.';
                    validCharacter = true;
                }

                if (validCharacter == false)
                    break;
            }

            return version;
        }

        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }

            string MACwithColons = "";
            for (int i = 0; i < sMacAddress.Length; i++)
            {
                MACwithColons = MACwithColons + sMacAddress.Substring(i, 2) + ":";
                i++;
            }
            MACwithColons = MACwithColons.Substring(0, MACwithColons.Length - 1);

            return MACwithColons;
        }
    }
}