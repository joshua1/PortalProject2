using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using PortalProject2.Models;

namespace PortalProject2.ViewModels
{
    public class PhoneNumber
    {
        [Required]
        public string Number { get; set; }

    }
    public class DeviceResponse
    {
        public bool Status { get; set; }
        public string StatusMessage { get; set; }
        public Device Device { get; set; }
    }

    public class PushStats
    {
        public int PushesSent { get; set; }
        public int TotalOk { get; set; }
        public int TotalFail { get; set; }
        public int TotalDevices { get; set; }
        public int RegisteredDevices { get; set; }
    }
}
