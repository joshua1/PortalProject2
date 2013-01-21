using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalProject2.ViewModels
{
    public class Messages
    {
        public string MessageText { get; set; }
        public string PhoneNumbers { get; set; }
        public bool ToAll { get; set; }
    }
    public class NewDevice
    {
        public string DeviceToken { get; set; }
        public string DevicePhoneNumber { get; set; }
    }

}