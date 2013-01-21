using System;
using System.Collections;
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
    public class MessagesController : ApiController
    {
        private VansoPushPortalContext db = new VansoPushPortalContext();

        // GET api/Messages
        public IEnumerable GetMessages()
        {
            return MessageLogic.GetAllMessages();// db.Messages.AsEnumerable();
        }

        // GET api/Messages/5
        public Message GetMessage(long id)
        {
            Message message = MessageLogic.GetMessage(id);// db.Messages.Find(id);
            if (message == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return message;
        }

        [AcceptVerbs("GET")]
        public List<PhoneNumber> Numbers(long id)
        {
            return MessageLogic.GetNumbers(id);
        } 

        // PUT api/Messages/5
        public HttpResponseMessage PutMessage(long id, Message message)
        {
            if (ModelState.IsValid && id == message.Id)
            {
                db.Entry(message).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Messages
        public HttpResponseMessage PostMessage(ViewMessage message)
        {
            if (ModelState.IsValid)
            {
               var resp= MessageLogic.SendMessage(message);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created,resp);
               
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Messages/5
        public HttpResponseMessage DeleteMessage(long id)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Messages.Remove(message);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, message);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}