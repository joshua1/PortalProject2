using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalProject2.Models;

namespace PortalProject2.Logic
{
    public class BaseLogic
    {
        public const string conName = "VansoPushPortalContext";
        public static Repo<VansoPushPortalContext> repo = new Repo<VansoPushPortalContext>(conName);
        public static Repo<VansoPushPortalContext> repo2 = new Repo<VansoPushPortalContext>(conName);
    }
}