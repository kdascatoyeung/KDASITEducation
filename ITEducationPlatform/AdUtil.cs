using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.DirectoryServices;

namespace ITEducationPlatform
{
    public class AdUtil
    {
        public static string GetStaffName()
        {
            //string domain = IPGlobalProperties.GetIPGlobalProperties().DomainName;
            string domain = Environment.UserDomainName;

            string loginName = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).Identity.Name;

            string username = loginName.Substring(loginName.IndexOf('\\') + 1);
            DirectoryEntry domainEntry = new DirectoryEntry("LDAP://" + domain);

            DirectorySearcher searcher = new DirectorySearcher(domainEntry);
            searcher.Filter = "(&(objectClass=user)(sAMAccountName=" + username + "))";
            SearchResult result = searcher.FindOne();
            DirectoryEntry entry = result.GetDirectoryEntry();

            string currentUser = entry.Properties["displayName"].Value.ToString();

            return currentUser;
        }

        public static string GetStaffId()
        {
            string domain = Environment.UserDomainName;
            //string domain = IPGlobalProperties.GetIPGlobalProperties().DomainName;

            string loginName = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).Identity.Name;

            string username = loginName.Substring(loginName.IndexOf('\\') + 1);
            DirectoryEntry domainEntry = new DirectoryEntry("LDAP://" + domain);

            DirectorySearcher searcher = new DirectorySearcher(domainEntry);
            searcher.Filter = "(&(objectClass=user)(sAMAccountName=" + username + "))";
            SearchResult result = searcher.FindOne();
            DirectoryEntry entry = result.GetDirectoryEntry();

            string account = entry.Properties["sAMAccountName"].Value.ToString();

            return account;
        }
    }
}
