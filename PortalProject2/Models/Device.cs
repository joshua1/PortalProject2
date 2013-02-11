using System;
using System.Collections.Generic;

namespace PortalProject2.Models
{
    public class Device
    {
        public long Id { get; set; }
        public string DeviceToken { get; set; }
        public string DevicePhoneNumber { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceVersion { get; set; }
        public string DevicePlatform { get; set; }
    }
}
