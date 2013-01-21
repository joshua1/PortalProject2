﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using PortalProject2.Models;
using PortalProject2.Utils;
using PortalProject2.ViewModels;

namespace PortalProject2.Logic
{
    public class DeviceLogic:BaseLogic
    {
        internal static DeviceResponse SaveDevice(NewDevice device)
        {
            DeviceResponse dr=new DeviceResponse();
            var Count = repo.Select<Device>().Count(k => k.DevicePhoneNumber == device.DevicePhoneNumber);
            if(Count>0)
            {
                dr.Status = false;
                dr.StatusMessage = "Phone number already exist";
            }
            else
            {
                repo.Insert(new Device {DevicePhoneNumber = device.DevicePhoneNumber, DeviceToken = device.DeviceToken});
                dr.Status = true;
                dr.StatusMessage = "Saved";
            }
            return dr;
        }

        internal static bool DeleteDevice(string phoneNo)
        {
            var dev = repo.Select<Device>().Where(k => k.DevicePhoneNumber == phoneNo);
            var count = dev.Count();
            var item = dev.FirstOrDefault();
            if(count>0)
            {
                repo2.Delete(item);
                return true; 
            }
            else
            {
                return false;
            }
            
            
        }

        internal static void UpdateDevice(NewDevice device)
        {
            var dev = repo.Select<Device>().FirstOrDefault(k => k.DevicePhoneNumber == device.DevicePhoneNumber);
            if (dev != null)
            {
                dev.DeviceToken = device.DeviceToken;
                repo.Update(dev);
            }
        }

        internal static string SendLock(PhoneNumber phone)
        {
            var number = ReturnHash(phone.Number);
            string sURL;
            var serviceId = ConfigurationManager.AppSettings["AppServiceId"];
            sURL = "https://bankingsvc.vanso.com/unregister_user?app_id="+serviceId+"&user_array="+number;

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);


            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

           
                sLine = objReader.ReadLine();
            return sLine;

        }

        private static string ReturnHash(string phone)
        {
            if (phone.Substring(0, 1) == "+")
                phone = phone.Substring(1);
            else if (phone.Substring(0, 2) == "00")
                phone = phone.Substring(2);
            if (phone.Substring(0, 3) == "234")
                return 
                           "['"+Md5.GetMd5Hash(phone)+"','"+ Md5.GetMd5Hash("0" + phone.Substring(3))+"','"+ phone.Substring(3)+"']";
            else if(phone.Substring(0, 1) == "0")
                return "['"+ Md5.GetMd5Hash("234"+phone.Substring(1))+"','"+ Md5.GetMd5Hash(phone)+"','"+phone.Substring(1)+"']";
            else
                return "['"+Md5.GetMd5Hash("234"+phone)+"','"+ Md5.GetMd5Hash("0" + phone)+"','"+phone+"']";
        }
    }
}