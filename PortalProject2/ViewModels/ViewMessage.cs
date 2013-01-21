using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalProject2.ViewModels
{
    public class ViewMessage
    {
        public bool ToAll { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string MessageText { get; set; }
        public string PhoneNumbers { get; set; }
        
    }
}