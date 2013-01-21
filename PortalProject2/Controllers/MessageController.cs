using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using PortalProject2.Logic;
using PortalProject2.Models;
using PortalProject2.ViewModels;

namespace PortalProject2.Controllers
{
    [Authorize]
    public class MessageController : BootstrapBaseController
    {
        private VansoPushPortalContext db = new VansoPushPortalContext();
        //
        // GET: /Message/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Message/Details/5

        public ActionResult Details(long id = 0)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        //
        // GET: /Message/Create

        public ActionResult Create()
        {
            return View(new ViewMessage());
        }

        //
        // POST: /Message/Create

        [HttpPost]
        public ActionResult Create(ViewMessage message)
        {
            var ret = string.Empty;
            if (ModelState.IsValid)
            {
               var resp= MessageLogic.SendMessage(message);
               switch (resp.status_code)
               {
                   case "200":
                       Success(@"Message Sent Successfully");
                       break;
                   case "210":
                       Error(@"Argument Error, " + resp.status_message);
                       break;
                   case "N/A":
                       Error(@"Error!, Malformed request string");
                       break;
                   case "500":
                       Error( @"Internal Error Occured");
                       break;
               }
               // return RedirectToAction("Index");
            }else
            Error("there were some errors in your form.");
            return View(message);
        }

        //
        // GET: /Message/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        //
        // POST: /Message/Edit/5

        [HttpPost]
        public ActionResult Edit(Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        //
        // GET: /Message/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        //
        // POST: /Message/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}