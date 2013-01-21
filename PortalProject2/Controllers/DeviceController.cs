using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using PortalProject2.Logic;
using PortalProject2.ViewModels;

namespace PortalProject2.Controllers
{
    [Authorize]
    public class DeviceController : BootstrapBaseController
    {
        //
        // GET: /Device/

        public ActionResult Index()
        {
            return View(new PhoneNumber());
        }

        [HttpPost]
        public ActionResult Lock(PhoneNumber phone)
        {
            var ret = string.Empty;
            if (ModelState.IsValid)
            {
                var resp = DeviceLogic.SendLock(phone);
                switch (resp)
                {
                    case "1":
                        Success(@"Phone number locked Successfully");
                        break;
                    case "0":
                        Error(@"Phone number not found");
                        break;
                }
                // return RedirectToAction("Index");
            }
            else
            Error("An error occured.");
            return View("Index",new PhoneNumber());
        }

    }
}
