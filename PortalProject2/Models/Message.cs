using System;
using System.Collections.Generic;

namespace PortalProject2.Models
{
    public class Message
    {
        public Message()
        {
            this.MessageNumbers = new List<MessageNumber>();
        }

        public long Id { get; set; }
        public string MessageId { get; set; }
        public int StatusCode { get; set; }
        public string MessageStatus { get; set; }
        public System.DateTime DateSent { get; set; }
        public string PhoneNumber { get; set; }
        public string MessageText { get; set; }
        public virtual ICollection<MessageNumber> MessageNumbers { get; set; }
    }
}
