using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PortalProject2.Logic;

using PortalProject2.Models;
using PortalProject2.ViewModels;

namespace PortalProject2.Controllers
{
    public class DevicesController : ApiController
    {
        private VansoPushPortalContext db = new VansoPushPortalContext();

        // GET api/Devices
        public IEnumerable<Device> GetDevices()
        {
            return db.Devices.AsEnumerable();
        }

        // GET api/Devices/5
        public Device GetDevice(long id)
        {
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return device;
        }

        // PUT api/Devices/5
        [AcceptVerbs("PUT")]
        public HttpResponseMessage PutDevice(NewDevice device)
        {
            if (ModelState.IsValid)
            {
               DeviceResponse dr= DeviceLogic.UpdateDevice(device);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created,dr);
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                // response.Headers.Location = new Uri(Url.Link("DefaultApi", new DeviceResponse { Status = true, StatusMessage = "Updated" }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Devices
        public HttpResponseMessage PostDevice(NewDevice device)
        {
            if (ModelState.IsValid)
            {
               DeviceResponse dr= DeviceLogic.SaveDevice(device);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, dr);
                //response.Headers.Location = new Uri(Url.Link("DefaultApi", new DeviceResponse { Status=true,StatusMessage = "Saved"}));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        



        // DELETE api/Devices/5
        public HttpResponseMessage DeleteDevice(long id)
        {
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Devices.Remove(device);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, device);
        }
        [AcceptVerbs("DELETE")]
        public HttpResponseMessage DeletePhone(string phoneNo)
        {
            if (DeviceLogic.DeleteDevice(phoneNo))
            {
              var req=  Request.CreateResponse(HttpStatusCode.OK, phoneNo);
              req.Headers.Add("Access-Control-Allow-Origin", "*");
                return req;
            }
            else
            {

              return  Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}