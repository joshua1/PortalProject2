using System;
using System.Collections.Generic;

namespace PortalProject2.Models
{
    public class MessageNumber
    {
        public int Id { get; set; }
        public long MessageId { get; set; }
        public long DeviceId { get; set; }
        public virtual Device Device { get; set; }
        public virtual Message Message { get; set; }
    }
}
