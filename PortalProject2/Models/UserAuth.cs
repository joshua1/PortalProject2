using System;
using System.Collections.Generic;

namespace PortalProject2.Models
{
    public class UserAuth
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PrimaryEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string BirthDateRaw { get; set; }
        public string Country { get; set; }
        public string Culture { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Language { get; set; }
        public string MailAddress { get; set; }
        public string Nickname { get; set; }
        public string PostalCode { get; set; }
        public string TimeZone { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
        public string DigestHA1Hash { get; set; }
        public string Roles { get; set; }
        public string Permissions { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> RefId { get; set; }
        public string RefIdStr { get; set; }
        public string Meta { get; set; }
    }
}
