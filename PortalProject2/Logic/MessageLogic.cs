using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using PortalProject2.Models;
using PortalProject2.Utils;
using PortalProject2.ViewModels;

namespace PortalProject2.Logic
{
    public class MessageLogic:BaseLogic
    {
         
         internal static MessageResponse SendMessage(ViewMessage msg)
         {
             string apiToken = ConfigurationManager.AppSettings["ApiToken"];
             string applicationCode = ConfigurationManager.AppSettings["ApplicationCode"];
             List<string> deviceTokens = new List<string>();
             List<string> phoneList = new List<string>();

             Message newMessage = new Message();
           
             newMessage.MessageText = msg.MessageText;
            
            
             if (msg.ToAll)
             {
                 var devices = repo.Select<Device>().ToList();
                 foreach (Device dv in devices)
                 {
                    

                     string token = dv.DeviceToken;
                     deviceTokens.Add(token);
                     phoneList.Add(dv.DevicePhoneNumber);
                 }

             }
             else
             {
                 string[] phoneNo = msg.PhoneNumbers.Split(',');
                 foreach (string item in phoneNo)
                 {
                     string item1 = Md5.GetMd5Hash(item);
                     var firstOrDefault = repo.Select<Device>().FirstOrDefault(q => q.DevicePhoneNumber == item1);
                     if (firstOrDefault != null)
                     {
                        

                         string token = firstOrDefault.DeviceToken;
                         deviceTokens.Add(token);
                         phoneList.Add(item);
                     }
                 }
             }
            // req.request.notifications.devices = new List<string>(deviceTokens.ToArray());


             //MessageResponse resp = PushService.DoSendMessage(req);
            
             JObject json = new JObject(
                 new JProperty("application", applicationCode),
                 new JProperty("auth", apiToken),
                 new JProperty("notifications",
                     new JArray(
                         new JObject(
                             new JProperty("send_date", "now"),
                             new JProperty("content", msg.MessageText),
                             new JProperty("wp_type", "Toast"),
                             new JProperty("wp_count", 3),
                             new JProperty("data",
                                 new JObject(
                                     new JProperty("custom", "json data"))),
                                     new JProperty("platforms", new JArray(new List<int> { 1, 2, 3, 4, 5 })),
                            // new JProperty("link", "http://Vanso.com/"),
                            // new JProperty("conditions",
                              //   new JArray(
                                //     (object)new JArray("Color", "EQ", "black")))
                                new JProperty("devices",new JArray(new List<string>(deviceTokens.ToArray())))
                                     ))));

            var resp= PushService.SendPush("createMessage", json);

             
                 newMessage.DateSent = DateTime.Now;
                 newMessage.MessageStatus = resp.status_message;
                 newMessage.StatusCode = int.Parse(resp.status_code);
                 if (resp.status_code != "200")
                 {
                     newMessage.MessageId = "0";
                 }
                 else
                 {
                     newMessage.MessageId = resp.response.messages[0];
                 }
             newMessage.PhoneNumber = JsonConvert.SerializeObject(phoneList);
                repo.Insert(newMessage);
            

             return resp;
         }

         internal static IEnumerable GetAllMessages()
         {
             return repo.Select<Message>().Select(k => new
                                                           {
                                                               k.Id,
                                                               k.MessageId,
                                                               k.MessageStatus,
                                                               k.DateSent,
                                                               k.MessageText,
                                                               k.StatusCode,
                                                               k.PhoneNumber
                                                           }).AsEnumerable();
         }

         internal static Message GetMessage(long id)
         {
             return repo.Select<Message>().FirstOrDefault(k => k.Id==id);
         }

         internal static List<PhoneNumber> GetNumbers(long id)
         {
             List<PhoneNumber> nList = new List<PhoneNumber>();
             var retList = new List<string>();
             var firstOrDefault = repo.Select<Message>().FirstOrDefault(k => k.Id == id);
             if (firstOrDefault != null)
             {
                 var numbs = firstOrDefault.PhoneNumber;
                 var numbers= JsonConvert.DeserializeObject<List<string>>(numbs);
               
                 foreach (var number in numbers)
                 {
                     PhoneNumber n = new PhoneNumber {Number = number};
                     nList.Add(n);
                 }
                 
             }
             return nList;
         }

         internal static PushStats GetPushStats()
         {
             int statusOk=0;
             int statusFail=0;
             var messages = repo.Select<Message>();
             //var messageCount = messages.Count();
             int devicesCount = 0;
             int devicesRegistered = repo2.Select<Device>().Count();
             
             List<PhoneNumber> nList = new List<PhoneNumber>();
             foreach (var message in messages)
             {
                 if (message.StatusCode.ToString() == "200")
                     statusOk += 1;
                 else
                     statusFail += 1;
                 var numbers = JsonConvert.DeserializeObject<List<string>>(message.PhoneNumber);

                 foreach (var number in numbers)
                 {
                     PhoneNumber n = new PhoneNumber {Number = number};
                     nList.Add(n);
                 }
             }
             devicesCount = nList.Count();
             PushStats pn = new PushStats
                                {
                                    PushesSent = statusOk+statusFail,
                                    TotalFail = statusFail,
                                    TotalOk = statusOk,
                                    RegisteredDevices=devicesRegistered
                                };
             return pn;
         }
    }
}